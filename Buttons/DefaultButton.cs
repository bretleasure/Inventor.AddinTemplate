using System.Windows.Forms;
using Inventor;
using InventorAddinTemplate.Constants;

namespace InventorAddinTemplate.Buttons
{
    public class DefaultButton : InventorButton
    {
        public override void Execute(NameValueMap context)
        {
            MessageBox.Show("Hello World");
        }

        public override string GetButtonName() => "Default Button";

        public override string GetDescriptionText() => "This is the Default Button";

        public override string GetToolTipText() => "Click this to execute the Default Button";

        public override string GetLargeIconResourceName()
        {
            throw new NotImplementedException();
        }

        public override string GetDarkThemeLargeIconResourceName()
        {
            throw new NotImplementedException();
        }

        public override string GetSmallIconResourceName()
        {
            throw new NotImplementedException();
        }

        public override string GetDarkThemeSmallIconResourceName()
        {
            throw new NotImplementedException();
        }
    }
}