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

		internal static IServiceCollection AddDockableWindow<TDockableWindow, TChildWindow>(this IServiceCollection services)
			where TDockableWindow : InventorDockableWindow
			where TChildWindow : UserControl
		{
			services.AddTransient<TChildWindow>();
			services.AddTransient<TDockableWindow>(provider =>
			{
				var dockableWindow = ActivatorUtilities.CreateInstance<TDockableWindow>(provider);
				var childWindow = provider.GetRequiredService<TChildWindow>();
			
				var properties = typeof(TChildWindow).GetProperties()
					.Where(prop => prop.IsDefined(typeof(InjectableAttribute), false));
				
				foreach (var property in properties)
				{
					var value = provider.GetRequiredService(property.PropertyType);
					property.SetValue(childWindow, value);
				}
			
				return dockableWindow;
			
			});
			return services;
		}
	}
}