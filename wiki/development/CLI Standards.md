This page lists general standards to adhere to when developing the **CLI** of our application.
# Overview
Our CLI should adopt a "noun" "verb" pattern. This means that the first argument the CLI receives should refer to a noun (essentially a 'model' from the MVC world) and the second argument should be a "verb" (which is something to do with the noun).
> This is a common pattern used in many modern CLIs!
> - [GitHub CLI](https://cli.github.com/)
> - [Terraform](https://www.terraform.io/)
> - [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/)
> - etc
# Architecture
The `CommandLineParser` class handles command line parsing. The `parse` function receives command line arguments (via the magic `args` variable). It has several functions that help you along.
- `Parse` is a simple function that receives command line `args` and returns both the `Noun` and the `Verb`
	- E.g., `credit list` -> `(Credit, List)`
- `GetCommandLineArgs` returns the arguments *after* the `Noun` and `Verb`
	- E.g., `credit list` -> `[]`
	- E.g., `credit list abc` -> `["abc"]`
- `ParseWithArgs` is a combination of both `Parse` and `GetCommandLineArgs`
	- E.g., `credit list abc` -> `((Credit, List), ["abc"]`