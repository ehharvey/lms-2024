This page describes how to get a development environment set up.
# Overview
## Requirements
This project uses:
1. [VS Code](https://code.visualstudio.com/)
2. [Obsidian](https://obsidian.md/)
	* This is for this wiki :)
3. [DotNet](https://dotnet.microsoft.com/en-us/download)
	* This is used for the app logic (aka business logic, backend)
## Other helpful tools
* This [extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit&WT.mc_id=dotnet-35129-website) for VS Code for C#
* [GitHub CLI](https://cli.github.com/)
	* See [[GitHub CLI]] for some notes on how to use this tool
# Procedures
## Setting up development environment
1. Install requirements in [[#Overview]]
2. Make sure `git` is also installed
3. Clone this repository
4. Open VS Code to this repository
5. Open a terminal
6. Navigate the terminal to `app`. This is where our current code lives!
	* `cd app`
7. Run `dotnet restore`
## Running project
* You can use VS Code's built-in "Run and Debug" menu.
* You can also use the terminal:
	1. Navigate to `app`
	2. Run `dotnet run`
* If you receive an error about the database, you may need to run `dotnet ef database update` before running the project.
## Wiki
1. Open Obsidian
2. Click 'Open folder as vault'
3. Navigate to the `wiki` folder of this repository
4. You'll see a dialog asking you whether to enable third-party extensions. Allow extensions!
