# Polysense
![.NET Core](https://github.com/Unskilledcrab/Polysense/workflows/.NET%20Core/badge.svg?branch=Development)

A multi-platform application that is making the political world easy to understand. Join us on [Discord](https://discord.gg/5HFjaeEysx)


## Table of contents
* [Project Setup](#project-setup)
* [Visual Studio Extensions](#visual-studio-extensions)
* [Adding Code Snippets](#adding-code-snippets) 

## Project Setup
* Right Click Solution (Polysense)
* Click "Set Startup Projects..."
* Select "Multiple startup projects:" and use the settings below (In the correct order)

format the projects as follows:

	PS.Shared    - None
	PS.UI.Shared - None
	PS.Web.API   - Start
	PS.UI.WPF    - Start

Next update the database context locally. Go to the Package Manager Console and type in the following command

	update-database

## Visual Studio Extensions
Add the extensions below either by clicking on the link and downloading them or following the instructions below

	To add these in visual studio go to "Extensions" -> "Manage Extensions" in the toolbar
	Click "Online" on the left tab
	Type the names below into the search bar on the top right

1) [Polysense Extensions](https://marketplace.visualstudio.com/items?itemName=Polysense.PolysenseExtensions)
2) [Codemaid](https://marketplace.visualstudio.com/items?itemName=SteveCadwallader.CodeMaid)
3) [EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)
4) [Github Extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=GitHub.GitHubExtensionforVisualStudio)
