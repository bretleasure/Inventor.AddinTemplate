using System.Windows;
using System.Windows.Forms.Integration;
using Inventor;
using Microsoft.Extensions.DependencyInjection;

namespace Inventor.AddinTemplate.Addin
{
	public abstract class InventorDockableWindow
	{
		protected abstract string WindowTitle { get; }
		public abstract bool ShowTitleBar { get; }
		public abstract int Height { get; }
		public abstract int Width { get; }
		protected abstract bool ShowVisibilityCheckbox { get; }
		public abstract bool DisableCloseButton { get; }
		public abstract DockingStateEnum DockingState { get; }

		

		

		
		
		
		private UserControl _childWindow;
		
		[Injectable]
		public UserControl ChildWindow
		{
			get
			{
				// if (_childWindow == null)
				// {
				// 	_childWindow = GetChildWindowInstance.Invoke();
				// }

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
					// if (ChildWindow is Window wpfWindow)
					// {
					// 	// Wpf Windows must be hosted in an ElementHost because they dont have a Handle
					// 	var host = new ElementHost
					// 	{
					// 		Dock = DockStyle.Fill
					// 	};
					//
					// 	// Attach the WPF Window
					// 	host.Child = wpfWindow;
					//
					// 	_handle = host.Handle;
					// }
					// else if (ChildWindow is UserControl userControl)
					// {
					// 	_handle = userControl.Handle;
					// }
					// else
					// {
					// 	throw new InvalidOperationException("ChildWindow must be a Window or UserControl");
					// }
					_handle = ChildWindow.Handle;
				}

				return _handle;
			}
		}
		
		/// <summary>
		/// The Inventor.DockableWindow object that represents this dockable window.
		/// </summary>
		public DockableWindow Window;

		internal void Initialize()
		{
			Window = AddinServer.InventorApp.UserInterfaceManager.DockableWindows.Add(Guid.NewGuid().ToString(), 
				$"Inventor.AddinTemplate_{WindowTitle}", WindowTitle);
			Window.AddChild(Handle);
			Window.ShowVisibilityCheckBox = ShowVisibilityCheckbox;
			Window.Height = Height;
			Window.Width = Width;
			Window.DockingState = DockingState;
			Window.DisableCloseButton = DisableCloseButton;
			Window.ShowTitleBar = ShowTitleBar;
			
			// ChildWindow.Disposed += (sender, args) =>
			// {
			// 	Dispose();
			// };
		}

		internal void Dispose()
		{
			Window.Delete();
			_childWindow.Dispose();
		}
		
		internal void Show()
		{
			Window.Visible = true;
		}
	}
}