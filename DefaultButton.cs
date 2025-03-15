using System.Windows.Forms;
using Inventor;
using Inventor.AddinTemplate.Addin;
using Inventor.InternalNames.Ribbon;

namespace Inventor.AddinTemplate
{
    internal class DefaultButton : InventorButton
    {
        private readonly DefaultDockableWindow _dockableWindow;
        
        public DefaultButton(DefaultDockableWindow dockableWindow)
        {
            _dockableWindow = dockableWindow;
        }
        protected override void Execute(NameValueMap context, Inventor.Application inventor)
        {
            // MessageBox.Show($"Current document name: {inventor.ActiveDocument.DisplayName}");
            _dockableWindow.Show();
        }

        protected override string GetRibbonName() => InventorRibbons.Assembly;

        protected override string GetRibbonTabName() => "AddinTemplate";

        protected override string GetRibbonPanelName() => "Inventor.AddinTemplate";

        protected override string GetButtonName() => "DefaultButton";

        protected override string GetDescriptionText() => "Default Button Description";

        protected override string GetToolTipText() => "Click the Default Button";

        protected override string GetLargeIconResourceName() => "Inventor.AddinTemplate.Assets.Default-Light.png";

        protected override string GetDarkThemeLargeIconResourceName() => "Inventor.AddinTemplate.Assets.Default-Dark.png";

        protected override string GetSmallIconResourceName() => GetLargeIconResourceName();

        protected override string GetDarkThemeSmallIconResourceName() => GetDarkThemeLargeIconResourceName();
    }
}