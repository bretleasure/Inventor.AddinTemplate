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

        protected override string GetRibbonName() => InventorRibbons.Drawing;

        protected override string GetRibbonTabName() => DrawingRibbonTabs.PlaceViews;

        protected override string GetRibbonPanelName() => "Inventor.AddinTemplate";

        protected override string GetButtonName() => "DefaultButton";

        protected override string GetDescriptionText() => "Default Button Description";

        protected override string GetToolTipText() => "Click the Default Button";

        protected override string GetLargeIconResourceName() => "Inventor.AddinTemplate.Buttons.Assets.Default-Light.png";

        protected override string GetDarkThemeLargeIconResourceName() => "Inventor.AddinTemplate.Buttons.Assets.Default-Dark.png";

        protected override string GetSmallIconResourceName() => GetLargeIconResourceName();

        protected override string GetDarkThemeSmallIconResourceName() => GetDarkThemeLargeIconResourceName();

        protected override void ConfigureProgressiveToolTip(ProgressiveToolTip toolTip)
        {
            toolTip.Title = "Default Button";
            toolTip.Description = "Default Button Description";
            toolTip.ExpandedDescription = "This is the expanded description of the default button.";
            toolTip.Image = null;
            toolTip.IsProgressive = false;
            toolTip.Video = null;
        }

        protected override void OnHelp(NameValueMap context, out HandlingCodeEnum handlingcode)
        {
            
        }
    }
}