using Inventor.AddinTemplate.Addin;

namespace Inventor.AddinTemplate
{
	public partial class UserControl1 : UserControl
	{
		private readonly Application _inventor;

		public UserControl1()
		{
			// _inventor = inventor;
			InitializeComponent();

			// label1.Text = inventor.ActiveDocument.DisplayName;
		}
	}
}