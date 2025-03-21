using Microsoft.Extensions.DependencyInjection;

namespace Inventor.AddinTemplate.Addin
{
	public static class Extensions
	{
		public static void CloseWindow(this UserControl userControl)
		{
			//get parent dockable window
			var windows = AddinServer.Services.GetServices<InventorDockableWindow>();
				
				var dockableWindow = windows.FirstOrDefault(x => x.ChildWindow == userControl);
			if (dockableWindow != null)
			{
				dockableWindow.Window.Visible = false;
			}
		}
	}
}