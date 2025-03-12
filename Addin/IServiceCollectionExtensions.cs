using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Inventor.AddinTemplate.Addin
{
	internal static class IServiceCollectionExtensions
	{
		internal static IServiceCollection AddRibbonButton<TButton>(this IServiceCollection services) where TButton : InventorButton
		{
			services.AddSingleton<InventorButton, TButton>();
			return services;
		}
		
		internal static IServiceCollection AddDockableUserControl<TChildWindow>(this IServiceCollection services, string windowTitle) where TChildWindow : UserControl, IDockableWindowChild
		{
			services.AddSingleton(new InventorDockableWindow<TChildWindow>(windowTitle));
			return services;
		}
		
		internal static IServiceCollection AddDockableWpf<TChildWindow>(this IServiceCollection services, string windowTitle) where TChildWindow : Window, IDockableWindowChild
		{
			services.AddSingleton(new InventorDockableWindow<TChildWindow>(windowTitle));
			return services;
		}
	}
}