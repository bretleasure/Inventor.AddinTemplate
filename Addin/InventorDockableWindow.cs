using System.Windows;
using System.Windows.Forms.Integration;
using Inventor;

namespace Inventor.AddinTemplate.Addin
{
	public class InventorDockableWindow<TWindowChild> where TWindowChild : IDockableWindowChild
	{
		public InventorDockableWindow(string title)
		{
			WindowTitle = title;
		}
		
		private TWindowChild _childWindow;
		private TWindowChild ChildWindow
		{
			get
			{
				if (_childWindow == null)
				{
					_childWindow = Activator.CreateInstance<TWindowChild>();
					_childWindow.InventorApp = AddinServer.InventorApp;
				}

				return _childWindow;
			}
			set => _childWindow = value;
		}

		private IntPtr _handle;
		private IntPtr Handle
		{
			get
			{
				if (_handle == default(IntPtr))
				{
					if (ChildWindow is Window wpfWindow)
					{
						// Wpf Windows must be hosted in an ElementHost because they dont have a Handle
						var host = new ElementHost
						{
							Dock = DockStyle.Fill
						};

						// Attach the WPF Window
						host.Child = wpfWindow;
					
						_handle = host.Handle;
					}
					else if (ChildWindow is UserControl userControl)
					{
						_handle = userControl.Handle;
					}
					else
					{
						throw new InvalidOperationException("ChildWindow must be a Window or UserControl");
					}
				}

				return _handle;
			}
		}

		private string WindowTitle { get; }
		
		private DockableWindow _window;

		internal void Initialize()
		{
			_window = AddinServer.InventorApp.UserInterfaceManager.DockableWindows.Add(Guid.NewGuid().ToString(), 
				$"Inventor.AddinTemplate_{WindowTitle}", WindowTitle);
			_window.AddChild(Handle);
		}
		
		internal void Dispose()
		{
			_window.Delete();
			ChildWindow = default(TWindowChild);
		}
		
		internal void Show()
		{
			_window.Visible = true;
		}
	}
}