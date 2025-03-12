using Inventor.AddinTemplate.Addin;
using Inventor.AddinTemplate.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Inventor.AddinTemplate
{
	/// <summary>
	/// Register UI components of the addin.
	/// </summary>
	public static class AddinRegistry
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddRibbonButton<DefaultButton>()
				.AddDockableUserControl<UserControl1>("My Dockable Window")
				.AddDockableWpf<Window1>("My Dockable WPF Window");
			
		}
	}
}