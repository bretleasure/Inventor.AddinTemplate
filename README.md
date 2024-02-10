# Inventor Addin Template

[![NuGet version (Inventor.AddinTemplate)](https://buildstats.info/nuget/Inventor.AddinTemplate)](https://www.nuget.org/packages/Inventor.AddinTemplate)

## Overview

The goal of this template is to make creating an Inventor addin as easy as possible. All that is required is to create the buttons and write the code that will be executed when the buttons are clicked. The template will handle adding the buttons to the ribbon.


The template is designed to be used with Visual Studio or JetBrains Rider.

## Installation

The latest version of template can be installed from [NuGet.org](https://www.nuget.org/packages/Inventor.AddinTemplate/) using the dotnet CLI.

```powershell
dotnet new install Inventor.AddinTemplate
```

## Usage

Once the template is installed it will appear in the list of available project templates in Visual Studio and Rider.

### Addin Components

These are the components that will be created when the addin is compiled.

#### .addin File

The .addin file is the entry point for the addin. It is an XML file that describes the addin and its components. The file is used by the Inventor Addin Manager to load the addin.

The .addin file is a pointer to the .dll file. It is also used to specify the addin's name, description, and other properties.

#### .dll File

The .dll file is the compiled addin. It is a dynamic link library that contains the addin's code.

### Adding Buttons

Adding buttons is very simple. Just create a class that inherits the `InventorButton` class and implement the abstract methods. The `InventorButton` class will handle adding the button to the ribbon and executing the code when the button is clicked.

All button classes in this project will be loaded into the Inventor UI. A button can be prevented from being loaded by overriding the `Enabled` property to return `false`.

#### Execute Method

This method is called when the button is clicked. This is where the code that the button will execute should be placed.

#### Ribbon

The `GetRibbonName()` method is used to specify the Ribbon the button will be added to. The available ribbon names can be found using the [Inventor.InternalNames](https://www.nuget.org/packages/Inventor.InternalNames) package that is included with this template.

#### Ribbon Tabs and Panels

The `GetRibbonTabName()` and `GetRibbonPanelName()` methods are used to specify the tab and panel that the button will be added to. If the name matches the `InternalName` of an existing tab or panel the button will be added to that tab or panel. Otherwise a new tab or panel will be created.

Internal names can be found using the [Inventor.InternalNames](https://www.nuget.org/packages/Inventor.InternalNames) package that is included with this template.

#### Icon Resources

Button icons need to be .PNG files and be added to the project as embedded resources.

Check out the `DefaultButton` class for an example of how to add a button to the ribbon.
