using System.Xml.Linq;

namespace Kavsar.ArchitectureTests;

public sealed class ProductionProjectDependencyTests
{
    private static readonly IReadOnlyDictionary<string, IReadOnlySet<string>> ApprovedDirectDependencies =
        new Dictionary<string, IReadOnlySet<string>>(StringComparer.Ordinal)
        {
            ["Kavsar.Platform"] = SetOf(),
            ["Kavsar.Modules.Organization"] = SetOf("Kavsar.Platform"),
            ["Kavsar.Modules.Audit"] = SetOf("Kavsar.Platform"),
            ["Kavsar.Modules.Membership"] = SetOf(
                "Kavsar.Platform",
                "Kavsar.Modules.Organization"),
            ["Kavsar.Host"] = SetOf(
                "Kavsar.Platform",
                "Kavsar.Modules.Organization",
                "Kavsar.Modules.Membership",
                "Kavsar.Modules.Audit"),
        };

    [Fact]
    public void Production_projects_follow_the_approved_dependency_graph()
    {
        var repositoryRoot = FindRepositoryRoot();
        var actualDependencies = LoadProductionProjectDependencies(repositoryRoot);

        Assert.Equal(
            ApprovedDirectDependencies.Keys.Order(StringComparer.Ordinal),
            actualDependencies.Keys.Order(StringComparer.Ordinal));

        foreach (var project in ApprovedDirectDependencies.Keys)
        {
            Assert.Equal(
                ApprovedDirectDependencies[project].Order(StringComparer.Ordinal),
                actualDependencies[project].Order(StringComparer.Ordinal));
        }

        AssertNoCycles(actualDependencies);
        AssertNoUnapprovedTransitiveDependencies(actualDependencies);
    }

    private static Dictionary<string, IReadOnlySet<string>> LoadProductionProjectDependencies(
        string repositoryRoot)
    {
        var sourceDirectory = Path.Combine(repositoryRoot, "src");
        var projectFiles = Directory.GetFiles(
            sourceDirectory,
            "*.csproj",
            SearchOption.AllDirectories);
        var projectNamesByPath = projectFiles.ToDictionary(
            Path.GetFullPath,
            path => Path.GetFileNameWithoutExtension(path),
            StringComparer.Ordinal);

        return projectFiles.ToDictionary(
            path => projectNamesByPath[Path.GetFullPath(path)],
            path => (IReadOnlySet<string>)LoadProjectReferences(path, projectNamesByPath),
            StringComparer.Ordinal);
    }

    private static HashSet<string> LoadProjectReferences(
        string projectFile,
        IReadOnlyDictionary<string, string> projectNamesByPath)
    {
        var document = XDocument.Load(projectFile);
        var projectDirectory = Path.GetDirectoryName(projectFile)
            ?? throw new InvalidOperationException($"Project directory was not found for '{projectFile}'.");

        return document
            .Descendants("ProjectReference")
            .Select(element => element.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => Path.GetFullPath(Path.Combine(projectDirectory, include!)))
            .Where(projectNamesByPath.ContainsKey)
            .Select(path => projectNamesByPath[path])
            .ToHashSet(StringComparer.Ordinal);
    }

    private static void AssertNoCycles(
        IReadOnlyDictionary<string, IReadOnlySet<string>> dependencies)
    {
        foreach (var project in dependencies.Keys)
        {
            Assert.DoesNotContain(project, GetTransitiveDependencies(project, dependencies));
        }
    }

    private static void AssertNoUnapprovedTransitiveDependencies(
        IReadOnlyDictionary<string, IReadOnlySet<string>> actualDependencies)
    {
        foreach (var project in actualDependencies.Keys)
        {
            var actualTransitiveDependencies = GetTransitiveDependencies(project, actualDependencies);
            var approvedTransitiveDependencies = GetTransitiveDependencies(
                project,
                ApprovedDirectDependencies);

            Assert.Equal(
                approvedTransitiveDependencies.Order(StringComparer.Ordinal),
                actualTransitiveDependencies.Order(StringComparer.Ordinal));
        }
    }

    private static HashSet<string> GetTransitiveDependencies(
        string project,
        IReadOnlyDictionary<string, IReadOnlySet<string>> dependencies)
    {
        var visited = new HashSet<string>(StringComparer.Ordinal);
        var pending = new Stack<string>(dependencies[project]);

        while (pending.TryPop(out var dependency))
        {
            if (!visited.Add(dependency) || !dependencies.TryGetValue(dependency, out var nestedDependencies))
            {
                continue;
            }

            foreach (var nestedDependency in nestedDependencies)
            {
                pending.Push(nestedDependency);
            }
        }

        return visited;
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "Kavsar.slnx")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("The repository root containing Kavsar.slnx was not found.");
    }

    private static IReadOnlySet<string> SetOf(params string[] projectNames) =>
        projectNames.ToHashSet(StringComparer.Ordinal);
}
