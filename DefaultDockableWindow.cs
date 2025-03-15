using Inventor.AddinTemplate.Addin;

namespace Inventor.AddinTemplate
{
	public class DefaultDockableWindow : InventorDockableWindow
	{
		protected override string WindowTitle => "Default Dockable Window";
		public override bool ShowTitleBar => false;
		public override int Height => 400;
		public override int Width => 600;
		protected override bool ShowVisibilityCheckbox => false;
		public override bool DisableCloseButton => false;
		public override DockingStateEnum DockingState => DockingStateEnum.kFloat;
	}
}