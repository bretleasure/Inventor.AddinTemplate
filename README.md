# Inventor Addin Template

![GitHub Release](https://img.shields.io/github/v/release/bretleasure/inventor.addintemplate?logo=github)
![GitHub Release](https://img.shields.io/github/v/release/bretleasure/inventor.addintemplate?include_prereleases&logo=github&label=latest%20build)
![NuGet Downloads](https://img.shields.io/nuget/dt/inventor.addintemplate?logo=nuget&color=9932CC&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FInventor.AddinTemplate)
![GitHub License](https://img.shields.io/github/license/bretleasure/inventor.addintemplate?color=salmon)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/bretleasure/inventor.addintemplate/build-deploy.yml?logo=github%20actions&logoColor=white&label=Build%20and%20Deploy)

## Overview

The goal of this template is to make creating an Inventor addin as easy as possible. All that is required is to create the buttons and write the code that will be executed when the buttons are clicked. The template will handle adding the buttons to the ribbon.

## Features

- Simplifies the creation of Inventor addins
- Automatically adds buttons to the ribbon
- Supports light and dark themes
- Easy to extend and customize

## Getting Started
   
1. [Creating Buttons](#creating-buttons)
   
2. [Deploying Your Addin](#deploying-your-addin)
   
3. [Referencing the Inventor Interop](#referencing-the-inventor-interop)

4. [Installing and Using this Template](#installing-and-using-this-template)

---

## Creating Buttons

All buttons need to inherit the `InventorButton` class, which contains abstract methods for all of the required information for creating a button. There are also some optional properties and methods that can be used.

### Adding button to the Ribbon

The `InventorButton` class handles adding the button to the ribbon and executing the code when the button is clicked.

All button classes in this project will be loaded into the Inventor UI. A button can be prevented from being loaded by overriding the `Enabled` property to return false.

### Command Execution

The code that should run when the button is clicked should be placed in the `Execute()` method.

### Required Methods


| Name                                                             | Description                                                                                                                                                                                                                                |
|------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Execute(`NameValueMap` context, `Inventor.Application` inventor) | This is the code that will execute when the button is clicked by the user. The `Inventor.Application` argument is the active instance of Inventor                                                                                          |
| GetRibbonName()                                                  | The internal name of the Ribbon the button will be added to. See [Internal Naming](#internal-naming)                                                                                                                                   |
| GetRibbonTabName() | The name of the Ribbon Tab the button will be added to. If an Internal name of an existing Ribbon Tab is used, the button will be added to that Tab. Otherwise, a new tab will be created. See [Internal Naming](#internal-naming)         |
| GetRibbonPanelName() | The name of the Ribbon Panel the button will be added to. If an Internal name of an existing Ribbon Panel is used, the button will be added to that Panel. Otherwise, a new panel will be created. See [Internal Naming](#internal-naming) |
| GetButtonName() | The name of the button. This value will be shown as the text for the button if `ShowLabel` is set to true                                                                                                                                  |
| GetDescriptionText() | The description text for the button                                                                                                                                                                                                        |
| GetToolTipText() | the tool tip text for the button                                                                                                                                                                                                           |
| GetLargeIconResourceName() | The resource to be used as the Large Icon for the button. See [Icon Resources](#icon-resources)                                                                                                                    |
| GetDarkThemeLargeIconResourceName() | The resource to be used as the Large Icon when Inventor's Dark Theme is used. See [Icon Resources](#icon-resources)                                                                                              |
| GetSmallIconResourceName() | The resource to be used as the Small Icon for the button. See [Icon Resources](#icon-resources)                                                                                                                    |
| GetDarkThemeSmallIconResourceName() | The resource to be used as the Small Icon when Inventor's Dark Theme is used. See [Icon Resources](#icon-resources)                                                                                                                                                                |                                                                                                                                                                                                        

#### Internal Naming

The NuGet package [Inventor.InternalNames](https://github.com/bretleasure/Inventor.InternalNames) is referenced in this template and contains all of the Internal Names for the Inventor Ribbon, RibbonTabs, and RibbonPanels. 

#### Icon Resources
Button icons need to be .PNG files and be added to the project as embedded resources. Large icons should be 32px by 32px and small icons should be 16px by 16px.

### Optional Properties

| Name         | Default Value | Description |
|--------------| - | --- |
| UseLargeIcon | true | Whether the button will be displayed with a Large Icon |
| ShowText     | true | Whether the button's label will be displayed |
| Enabled      | true | Whether the button will be displayed |
| SequenceNumber | 0 | Used to control the order that the buttons are added (if creating multiple buttons). Buttons are ordered lowest to highest from Left to Right / Top to Bottom |

### Example

```csharp
internal class DefaultButton : InventorButton
{
    protected override void Execute(NameValueMap context, Inventor.Application inventor)
    {
        MessageBox.Show($"Current document name: {inventor.ActiveDocument.DisplayName}");
    }

    protected override string GetRibbonName() => InventorRibbons.Drawing;

    protected override string GetRibbonTabName() => DrawingRibbonTabs.PlaceViews;

    protected override string GetRibbonPanelName() => "AutodeskInventorAddin1";

    protected override string GetButtonName() => "DefaultButton";

    protected override string GetDescriptionText() => "Default Button Description";

    protected override string GetToolTipText() => "Click the Default Button";

    protected override string GetLargeIconResourceName() => "AutodeskInventorAddin1.Buttons.Assets.Default-Light.png";

    protected override string GetDarkThemeLargeIconResourceName() => "AutodeskInventorAddin1.Buttons.Assets.Default-Dark.png";

    protected override string GetSmallIconResourceName() => GetLargeIconResourceName();

    protected override string GetDarkThemeSmallIconResourceName() => GetDarkThemeLargeIconResourceName();
}
```

---

## Deploying Your Addin

Deploying your addin is fairly simple. Just copy the build contents of your project and put them in the correct folder so that Inventor can find them.

The only catch is there are multiple locations that Inventor looks for addins to load. Deciding on which location to use will depend on the following factors:

1. Should it load for all users of the computer or only a specific user?
2. Should it load for all versions of Inventor or only a specific version?

### Load for All Users and all Versions of Inventor

```
%AllUsersProfile%\Autodesk\ApplicationPlugins
```

### Load for All Users and a Specific Version of Inventor

```
%AllUsersProfile%\Autodesk\Inventor 2024\Addins
```

### Load for a Specific User and all Versions of Inventor

```
%AppData%\Autodesk\ApplicationPlugins
```

### Load for a Specific User and a Specific Version of Inventor

```
%AppData%\Autodesk\Inventor 2024\Addins
```

---

## Referencing the Inventor Interop

Inventor Addins must reference the Inventer Interop library (`Autodesk.Inventor.Interop.dll`) in order to access Inventor's API. This template includes a copy of the Inventor 2023 Interop in the `lib` folder. If needed, you can change the reference to a different version. The `Autodesk.Inventor.Interop.dll` can be found in the following location:

```
C:\Program Files\Autodesk\Inventor 20xx\Bin\Public Assemblies
```

`xx` should be set to the version of Inventor you want to reference.

### Keeping the Interop Up to Date

[This article on Mod the Machine](https://modthemachine.typepad.com/my_weblog/2015/01/keep-your-interop-up-to-date.html) explains how the different versions of the Interop work and how you can be sure that your app continues to work even with new versions of Inventor.

---

## Installing and Using this Template

The easiest way to install this template is by using the [dotnet CLI](https://dotnet.microsoft.com/en-us/download/dotnet). 

Simply run the following command and the project will then show in Visual Studio and JetBrains Rider

```powershell
dotnet new install Inventor.AddinTemplate
```

### Visual Studio

![image](https://github.com/bretleasure/Inventor.AddinTemplate/assets/30269827/60114aed-dd7d-4b99-b0ef-5dbfbe8f1b6a)

### JetBrains Rider

![image](https://github.com/bretleasure/Inventor.AddinTemplate/assets/30269827/f7305db2-4f18-4a09-ada2-61a6608c5463)

---
