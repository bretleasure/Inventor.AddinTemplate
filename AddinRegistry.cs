using Inventor.AddinTemplate.Addin;
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
				.AddDockableWindow<DefaultDockableWindow, DefaultUserControl>();

		}
	}
}