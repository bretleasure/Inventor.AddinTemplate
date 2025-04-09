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