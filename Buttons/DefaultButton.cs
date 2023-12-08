using System.Windows.Forms;
using Inventor;
using InventorAddinTemplate.Constants;
using Ribbons = Inventor.Ribbons;

namespace InventorAddinTemplate.Buttons
{
    public class DefaultButton : InventorButton
    {
        protected override void Execute(NameValueMap context)
        {
            MessageBox.Show($"Current document name: {AddinServer.InventorApp.ActiveDocument.DisplayName}");
        }

        protected override string GetRibbonName() => Constants.Ribbons.Drawing;

        protected override string GetRibbonTabName() => Constants.RibbonTabs.PlaceViews;

        protected override string GetRibbonPanelName() => "InventorAddinTemplate";

        protected override string GetButtonName() => "DefaultButton";

        protected override string GetDescriptionText() => "Default Button Description";

        protected override string GetToolTipText() => "Click the Default Button";

        protected override string GetLargeIconResourceName() => "InventorAddinTemplate.Buttons.Assets.Default-Light.png";

        protected override string GetDarkThemeLargeIconResourceName() => "InventorAddinTemplate.Buttons.Assets.Default-Dark.png";

        protected override string GetSmallIconResourceName() => GetLargeIconResourceName();

        protected override string GetDarkThemeSmallIconResourceName() => GetDarkThemeLargeIconResourceName();
    }
}