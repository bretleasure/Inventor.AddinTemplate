using Inventor.AddinTemplate.Addin;

namespace Inventor.AddinTemplate
{
	public partial class UserControl1 : UserControl, IDockableWindowChild
	{
		public UserControl1()
		{
			InitializeComponent();
		}

		public Application InventorApp { get; set; }
	}
}