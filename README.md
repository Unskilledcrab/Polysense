# Polysense
A multi-platform application that is making the political world easy to understand

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

1) [Codemaid](https://marketplace.visualstudio.com/items?itemName=SteveCadwallader.CodeMaid)
2) [EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)
3) [Github Extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=GitHub.GitHubExtensionforVisualStudio)

## Adding Code Snippets
Import the snippets in the "Tools/Code Snippets" folder into your project following the steps outlined or using the link to this site

[Microsoft Instructions](https://docs.microsoft.com/en-us/visualstudio/ide/walkthrough-creating-a-code-snippet?view=vs-2019#import-a-code-snippet)
* Go to "Tools" in the toolbar
* Click "Code Snippets Manager..."
* Click "Import" in the lower left corner
* Navigate to "PS.Snippets" file in the solution
* Select "My Code Snippets" folder
* Click "Finish"
