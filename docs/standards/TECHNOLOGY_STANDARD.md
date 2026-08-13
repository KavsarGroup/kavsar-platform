# Technology Standard

Version: 1.0.0  
Status: Approved  
Created on: 13 August 2026

---

# 1. Purpose

This Engineering Standard defines the approved technology baseline for the implementation of the Kavsar Platform.

Its purpose is to ensure consistency, maintainability, long-term support, portability, security, and institutional stability across all engineering work.

Technology shall support the Platform architecture.

Technology shall not define the Platform architecture.

---

# 2. Scope

This standard applies to:

- all Platform capabilities;
- all engineering work;
- all repositories belonging to the Kavsar Platform;
- all contributors.

---

# 3. Engineering Principles

Technology selection shall follow these principles:

- Architecture before technology.
- Simplicity before complexity.
- Proven technologies before experimental technologies.
- Long-Term Support (LTS) before short-term releases.
- Open standards before vendor lock-in.
- Platform capabilities before third-party dependencies.
- Institutional longevity before technological trends.

Implementation may evolve.

Architectural principles shall not.

---

# 4. Approved Technology Stack

## Backend

Language:

- C#

Runtime:

- .NET LTS

Framework:

- ASP.NET Core

---

## Persistence

Database:

- PostgreSQL

ORM:

- Entity Framework Core

Database Provider:

- Npgsql

---

## Architecture

- Capability-oriented Modular Monolith

---

## Frontend

- React
- TypeScript

Frontend tooling may evolve while preserving compatibility with approved Platform Services.

---

## Testing

Unit Testing:

- xUnit

Integration Testing:

- Testcontainers

Testing shall utilize the approved persistence technology wherever practical.

---

## Build & Delivery

Version Control:

- Git

Repository Hosting:

- GitHub

Continuous Integration:

- GitHub Actions

---

## Containers

Development and deployment shall utilize OCI-compatible container technology.

---

## Observability

The Platform shall provide standardized:

- Logging
- Tracing
- Metrics

using an implementation consistent with OpenTelemetry principles.

---

# 5. Approved Versions

The approved technology families are defined by this Engineering Standard.

The authoritative machine-readable record of the currently approved implementation versions is maintained in the repository root as:

`technology-manifest.json`

This Engineering Standard governs the approved technology stack and its evolution.

The Technology Manifest records the exact implementation versions approved for engineering use.

At the time of approval this standard adopts:

| Technology | Approved Version |
|------------|------------------|
| .NET | 10 LTS |
| C# | 14 |
| ASP.NET Core | 10 |
| PostgreSQL | 18 |
| Entity Framework Core | 10 |
| Npgsql | 10 |
| React | Current stable approved by Engineering Governance |
| TypeScript | Current stable approved by Engineering Governance |
| Vite | Current stable approved by Engineering Governance |
| xUnit | v3 |
| Testcontainers | Current stable |
| GitHub Actions | Current stable |
| OpenTelemetry | Current stable |

Future approved versions shall supersede this table through Engineering Governance.

---

# 6. Upgrade Policy

Technology upgrades shall follow these principles.

Major version upgrades:

- require engineering review;
- require compatibility validation;
- require formal approval.

Minor version upgrades:

- should be applied after validation.

Security updates:

- shall be applied promptly following successful validation.

Technology shall never be upgraded solely because a newer version exists.

---

# 7. Dependency Policy

Engineering shall minimize external dependencies.

Every dependency shall possess a clear engineering justification.

Preference shall always be given to:

- Platform capabilities;
- framework capabilities;
- standard libraries;

before introducing third-party packages.

Commercial dependencies require explicit approval.

Unused dependencies shall be removed.

---

# 8. Development Environment

The approved engineering environment shall support:

- Windows
- Linux
- macOS

Engineering work shall remain platform-independent wherever practical.

---

# 9. Continuous Integration

Every Pull Request shall successfully complete the approved engineering pipeline before merging.

The pipeline shall include, where applicable:

- Build
- Static Analysis
- Unit Tests
- Integration Tests
- Migration Validation
- Dependency Audit

Engineering Governance may extend the pipeline over time.

---

# 10. Governance

Changes to the approved technology stack shall occur only through approved Engineering Governance.

Technology standards may evolve.

Platform architecture shall remain governed by the Constitution, Charter, Platform Blueprint, Capability Specifications, and approved Architectural Decision Records.

---

# 11. References

- Constitution
- Charter
- Platform Blueprint
- ADR-001 through ADR-009

---

# 12. Closing Principle

Technology serves the Platform.

The Platform does not serve technology.