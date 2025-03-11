using Inventor;

namespace Inventor.AddinTemplate.Windows
{
	public abstract class InventorDockableWindow
	{
		public abstract string GetWindowTitle();
		
		private DockableWindow _window;
		public DockableWindow Window
		{
			get
			{
				if (_window == null)
				{
					_window = AddinServer.InventorApp.UserInterfaceManager.DockableWindows.Add(Guid.NewGuid().ToString(), $"Inventor.AddinTemplate_{GetWindowTitle()}", GetWindowTitle());					
				}

				return _window;
			}
		}
	}
}