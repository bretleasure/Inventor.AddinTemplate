using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Inventor;
using IPictureDisp = Inventor.IPictureDisp;

namespace Inventor.AddinTemplate.Buttons
{
    internal abstract class InventorButton
    {
        private void OnExecute(NameValueMap context) => Execute(context, AddinServer.InventorApp);

        protected abstract void Execute(NameValueMap context, Inventor.Application inventor);
        protected abstract string GetRibbonName();

        /// <summary>
        /// Name of the ribbon tab where the button will be placed. If the tab does not exist, it will be created.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetRibbonTabName();

        /// <summary>
        /// Name of the ribbon panel where the button will be placed. If the panel does not exist, it will be created.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetRibbonPanelName();

        /// <summary>
        /// Name of the button. This is the name that will be displayed in the UI.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetButtonName();

        /// <summary>
        /// Internal name of the button. This is the name that will be used to identify the button in the code. By default, it is a random GUID.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetInternalName() => string.Concat(AddinServer.AddinGuid.ToString(), "-",GetButtonName());

        protected abstract string GetDescriptionText();

        /// <summary>
        /// Tool tip text that will be displayed when the user hovers over the button.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetToolTipText();

        protected abstract string GetLargeIconResourceName();
        protected abstract string GetDarkThemeLargeIconResourceName();
        protected abstract string GetSmallIconResourceName();
        protected abstract string GetDarkThemeSmallIconResourceName();
        protected virtual CommandTypesEnum CommandType => CommandTypesEnum.kEditMaskCmdType;
        protected virtual bool UseLargeIcon => true;
        protected virtual bool ShowText => true;
        internal virtual int SequenceNumber => 0;
        private bool AddToRibbon => true;

        private Ribbon _cachedRibbon;
        private Ribbon Ribbon
        {
            get
            {
                if (_cachedRibbon == null)
                {
                    _cachedRibbon = AddinServer.InventorApp.UserInterfaceManager.Ribbons
                        .Cast<Ribbon>()
                        .FirstOrDefault(r => r.InternalName == GetRibbonName());
                    
                    if (_cachedRibbon == null)
                    {
                        throw new Exception($"Ribbon {GetRibbonName()} not found");
                    }
                }

                return _cachedRibbon;
            }
        }

        private RibbonTab _cachedRibbonTab;
        private RibbonTab RibbonTab
        {
            get
            {
                if (_cachedRibbonTab == null)
                {
                    _cachedRibbonTab = Ribbon.RibbonTabs
                        .Cast<RibbonTab>()
                        .FirstOrDefault(t => t.InternalName == GetRibbonTabName());
                    
                    if (_cachedRibbonTab == null)
                    {
                        _cachedRibbonTab = Ribbon.RibbonTabs.Add(GetRibbonTabName(), GetRibbonTabName(), Guid.NewGuid().ToString());
                    }
                }

                return _cachedRibbonTab;
            }
        }

        private RibbonPanel _cachedRibbonPanel;
        private RibbonPanel RibbonPanel
        {
            get
            {
                if (_cachedRibbonPanel == null)
                {
                    _cachedRibbonPanel = RibbonTab.RibbonPanels
                        .Cast<RibbonPanel>()
                        .FirstOrDefault(p => p.InternalName == GetRibbonPanelName());
                    
                    if (_cachedRibbonPanel == null)
                    {
                        _cachedRibbonPanel = RibbonTab.RibbonPanels.Add(GetRibbonPanelName(), GetRibbonPanelName(), Guid.NewGuid().ToString());
                    }
                }

                return _cachedRibbonPanel;
            }
        }

        private ButtonDefinition _buttonDefinition;
        private ButtonDefinition Definition
        {
            get
            {
                if (_buttonDefinition == null)
                {
                    _buttonDefinition = AddinServer.InventorApp.CommandManager.ControlDefinitions.AddButtonDefinition(GetButtonName(), GetInternalName(),
                        CommandType, null, GetDescriptionText(), GetToolTipText(), SmallIcon, LargeIcon);
                    
                    _buttonDefinition.Enabled = true;
                    _buttonDefinition.OnExecute += OnExecute;
                }
                
                return _buttonDefinition;
            }
        }

        private CommandControl _button;

        /// <summary>
        /// Controls whether the button is enabled or not. If false, the button will be disabled (greyed out) and cannot be clicked.
        /// </summary>
        internal virtual bool Enabled
        {
            get => Definition.Enabled;
            set => Definition.Enabled = value;
        }

        internal void Initialize()
        {
            if (AddToRibbon)
            {
                _button = RibbonPanel.CommandControls.AddButton(Definition, UseLargeIcon, ShowText);
            }
            else
            {
                throw new NotImplementedException("AddToRibbon is false");
            }
        }

        internal void Dispose()
        {
            Definition.Delete();
            _button.Delete();
        }

        private IPictureDisp LightThemeLargeIcon => CreateIcon(GetLargeIconResourceName());
        private IPictureDisp DarkThemeLargeIcon => CreateIcon(GetDarkThemeLargeIconResourceName());
        private IPictureDisp LightThemeSmallIcon => CreateIcon(GetSmallIconResourceName());
        private IPictureDisp DarkThemeSmallIcon => CreateIcon(GetDarkThemeSmallIconResourceName());

        private IPictureDisp LargeIcon
        {
            get
            {
                if (AddinServer.InventorApp.ThemeManager.ActiveTheme.Name == "LightTheme")
                {
                    return LightThemeLargeIcon;
                }
                else
                {
                    return DarkThemeLargeIcon;
                }
            }
        }

        private IPictureDisp SmallIcon
        {
            get
            {
                if (AddinServer.InventorApp.ThemeManager.ActiveTheme.Name == "LightTheme")
                {
                    return LightThemeSmallIcon;
                }
                else
                {
                    return DarkThemeSmallIcon;
                }
            }
        }

        private IPictureDisp CreateIcon(string resourceName)
        {
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            if (resourceStream == null)
                throw new Exception($"Resource {resourceName} not found in assembly {Assembly.GetExecutingAssembly().FullName}");

            var bitmap = new Bitmap(resourceStream);

            return ImageConverter.BitmapToPicture(bitmap);
        }
    }

    internal class ImageConverter : AxHost
    {
        public ImageConverter() : base(string.Empty)
        {
        }

        public static IPictureDisp BitmapToPicture(Bitmap bitmap)
        {
            return (IPictureDisp)GetIPictureDispFromPicture(bitmap);
        }
    }
}