![open-graph-preview-img](https://raw.githubusercontent.com/bretleasure/Inventor.AddinTemplate/refs/heads/main/Assets/open-graph-preview-img.webp)

![GitHub Release](https://img.shields.io/github/v/release/bretleasure/inventor.addintemplate?logo=github)
![GitHub Release](https://img.shields.io/github/v/release/bretleasure/inventor.addintemplate?include_prereleases&logo=github&label=latest%20build)
![NuGet Downloads](https://img.shields.io/nuget/dt/inventor.addintemplate?logo=nuget&color=9932CC&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FInventor.AddinTemplate)
![GitHub License](https://img.shields.io/github/license/bretleasure/inventor.addintemplate?color=salmon)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/bretleasure/inventor.addintemplate/build-deploy.yml?logo=github%20actions&logoColor=white&label=Build%20and%20Deploy)

## Inventor Add-in Template

The goal of this template is to make creating an Inventor addin as easy as
possible. All that is required is to create the buttons and write the code that
will be executed when the buttons are clicked. The template will handle adding
the buttons to the ribbon.

## Features

- Simplifies the creation of Inventor addins
- Automatically adds buttons to the ribbon
- Supports light and dark themes
- Easy to extend and customize

## Getting Started

1️⃣ [Creating Buttons](#1️⃣-creating-buttons)

2️⃣ [Deploying Your Addin](#2️⃣-deploying-your-addin)

3️⃣ [Referencing the Inventor Interop](#3️⃣-referencing-the-inventor-interop)

4️⃣ [Installing and Using this Template](#4️⃣-installing-and-using-this-template)

---

## 1️⃣ Creating Buttons

All buttons need to inherit the `InventorButton` class, which contains abstract
properties for all of the required information for creating a button. There are
also some optional properties and methods that can be used.

### Adding button to the Ribbon

The `InventorButton` class handles adding the button to the ribbon and executing
the code when the button is clicked. All subclasses (i.e., user defined buttons)
in the project are automatically loaded into the Inventor UI. A button can be
prevented from being loaded by overriding the `Enabled` property to return
false.

### Command Execution

The code that should run when the button is clicked should be placed in the
`Execute()` method.

### Required Properties/Methods

| Name                                                             | Description                                                                                                                                                                                                                                |
| ---------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Execute(`NameValueMap` context, `Inventor.Application` inventor) | This is the code that will execute when the button is clicked by the user. The `Inventor.Application` argument is the active instance of Inventor                                                                                          |
| RibbonName                                                       | The internal name of the Ribbon the button will be added to. See [Internal Naming](#internal-naming)                                                                                                                                       |
| RibbonTabName                                                    | The name of the Ribbon Tab the button will be added to. If an Internal name of an existing Ribbon Tab is used, the button will be added to that Tab. Otherwise, a new tab will be created. See [Internal Naming](#internal-naming)         |
| RibbonPanelName                                                  | The name of the Ribbon Panel the button will be added to. If an Internal name of an existing Ribbon Panel is used, the button will be added to that Panel. Otherwise, a new panel will be created. See [Internal Naming](#internal-naming) |
| Label                                                            | The display name of the button. This value will be shown as the text for the button if `ShowLabel` is set to true                                                                                                                          |
| Description                                                      | The description text for the button                                                                                                                                                                                                        |
| Tooltip                                                          | the tool tip text for the button                                                                                                                                                                                                           |
| LargeIconResourceName                                            | The resource to be used as the Large Icon for the button. See [Icon Resources](#icon-resources)                                                                                                                                            |
| DarkThemeLargeIconResourceName                                   | The resource to be used as the Large Icon when Inventor's Dark Theme is used. See [Icon Resources](#icon-resources)                                                                                                                        |
| SmallIconResourceName                                            | The resource to be used as the Small Icon for the button. See [Icon Resources](#icon-resources)                                                                                                                                            |
| DarkThemeSmallIconResourceName                                   | The resource to be used as the Small Icon when Inventor's Dark Theme is used. See [Icon Resources](#icon-resources)                                                                                                                        |

#### Internal Naming

The NuGet package [Inventor.InternalNames](https://github.com/bretleasure/Inventor.InternalNames)
is referenced in this template and contains all of the Internal Names for the
Inventor Ribbon, RibbonTabs, and RibbonPanels.

#### Icon Resources

Button icons need to be .PNG files and be added to the project as embedded
resources. Large icons should be 32px by 32px and small icons should be 16px by
16px.

### Optional Properties/Methods

| Name           | Default Value | Description                                                                                                                                                   |
| --------------------------------------------------------------------- | ------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| UseLargeIcon                                                          | true          | Whether the button will be displayed with a Large Icon                                                                                                        |
| ShowText                                                              | true          | Whether the button's label will be displayed                                                                                                                  |
| Enabled                                                               | true          | Whether the button will be displayed                                                                                                                          |
| SequenceNumber                                                        | 0             | Used to control the order that the buttons are added (if creating multiple buttons). Buttons are ordered lowest to highest from Left to Right / Top to Bottom |
| ConfigureProgressiveToolTip(`ProgressiveToolTip` toolTip)             |               | Allows configuring a progressive tooltip. The `Label` and `Tooltip` properties will be ignored if this is used. Refer to Autodesk's [ProgressiveToolTip Documentation](https://help.autodesk.com/view/INVNTOR/2025/ENU/?guid=GUID-ProgressiveToolTip) for the available properties for `ProgressiveToolTip`|
| OnHelp(`NameValueMap` context, out `HandlingCodeEnum` handlingcode)   |               | **Only Available if using a Progressive Tool Tip.** Allows overriding what happens when a user selects F1 while the progressive tool tip is being displayed. Refer to Autodesk's [ButtonDefinition.OnHelp Event Documentation](https://help.autodesk.com/view/INVNTOR/2025/ENU/?guid=ButtonDefinition_OnHelp)|

### Example

```csharp
using System.Windows.Forms;
using Inventor;
using Inventor.InternalNames.Ribbon;

namespace Inventor.AddinTemplate.Buttons
{
    public class DefaultButton : InventorButton
    {
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            MessageBox.Show($"Current document name: {inventor.ActiveDocument.DisplayName}");
        }

        protected override string RibbonName => InventorRibbons.Drawing;

        protected override string RibbonTabName => DrawingRibbonTabs.PlaceViews;

        protected override string RibbonPanelName => "Inventor.AddinTemplate";

        protected override string Label => "DefaultButton";

        protected override string Description => "Default Button Description";

        protected override string Tooltip => "Click the Default Button";

        protected override string LargeIconResourceName => "Inventor.AddinTemplate.Buttons.Assets.Default-Light.png";

        protected override string DarkThemeLargeIconResourceName => "Inventor.AddinTemplate.Buttons.Assets.Default-Dark.png";

        protected override string SmallIconResourceName => LargeIconResourceName;

        protected override string DarkThemeSmallIconResourceName => DarkThemeLargeIconResourceName;
    }
}
```

---

## 2️⃣ Deploying Your Addin

Deploying your addin is fairly simple. Just copy the build contents of your
project and put them in the correct folder so that Inventor can find them.

The only catch is there are multiple locations that Inventor looks for addins to
load. Deciding on which location to use will depend on the following factors:

1. Should it load for all users of the computer or only a specific user?
2. Should it load for all versions of Inventor or only a specific version?

### Load for All Users and all Versions of Inventor

```text
%AllUsersProfile%\Autodesk\ApplicationPlugins
```

### Load for All Users and a Specific Version of Inventor

```text
%AllUsersProfile%\Autodesk\Inventor 2024\Addins
```

### Load for a Specific User and all Versions of Inventor

> [!IMPORTANT]
> This is the recommended installation location by Autodesk

```text
%AppData%\Autodesk\ApplicationPlugins
```

### Load for a Specific User and a Specific Version of Inventor

```text
%AppData%\Autodesk\Inventor 2024\Addins
```

---

## 3️⃣ Referencing the Inventor Interop

Inventor Addins must reference the Inventor Interop library
(`Autodesk.Inventor.Interop.dll`) in order to access Inventor's API. This
template includes a copy of the Inventor 2023 Interop in the `lib` folder. If
needed, you can change the reference to a different version. The
`Autodesk.Inventor.Interop.dll` can be found in the following location:

```text
C:\Program Files\Autodesk\Inventor 20xx\Bin\Public Assemblies
```

`xx` should be set to the version of Inventor you want to reference.

### Keeping the Interop Up to Date

[This article on Mod the Machine](https://modthemachine.typepad.com/my_weblog/2015/01/keep-your-interop-up-to-date.html)
explains how the different versions of the Interop work and how you can be sure
that your app continues to work even with new versions of Inventor.

---

## 4️⃣ Installing and Using this Template

The easiest way to install this template is by using the
[dotnet CLI](https://dotnet.microsoft.com/en-us/download/dotnet). Simply run the
following command and the project will then show in Visual Studio and JetBrains
Rider:

```powershell
dotnet new install Inventor.AddinTemplate
```

![A screenshot of the Visual Studio New Project Window.](https://raw.githubusercontent.com/bretleasure/Inventor.AddinTemplate/main/Assets/vs-new-solution.png)

![A screenshot of the Rider New Solution Window.](https://raw.githubusercontent.com/bretleasure/Inventor.AddinTemplate/main/Assets/Rider-new-solution.png)
