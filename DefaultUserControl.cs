using Inventor.AddinTemplate.Addin;

namespace Inventor.AddinTemplate
{
	public partial class DefaultUserControl : UserControl
	{
		private readonly Application _inventor;

		public DefaultUserControl(Inventor.Application inventor)
		{
			_inventor = inventor;
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			label1.Text = _inventor.ActiveDocument.FullDocumentName;
		}

		private void btn_Close_Click(object sender, EventArgs e)
		{
			
		}
	}
}