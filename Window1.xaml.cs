using System.Windows;
using Inventor.AddinTemplate.Addin;

namespace Inventor.AddinTemplate.Windows
{
	public partial class Window1 : Window, IDockableWindowChild
	{
		public Window1()
		{
			InitializeComponent();
		}

		public Application InventorApp { get; set; }
	}
}