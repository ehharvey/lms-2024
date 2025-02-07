This page describes notes related to managing the database.

# Overview
1. We use EF Core for DB management. This includes making migrations
2. Because of some quirkiness in our test code that Emil couldn't figure out, we need to add exclusions to generated DB migrations, This looks like:
```
using System.Diagnostics.CodeAnalysis; # <---- add this for the later annotation

namespace lms.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage] # <----- this is what to add
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
...
```