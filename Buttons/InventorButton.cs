﻿using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Inventor;
using IPictureDisp = Inventor.IPictureDisp;

namespace InventorAddinTemplate.Buttons
{
    public abstract class InventorButton
    {
        internal InventorButton()
        {
            Definition = AddinServer.InventorApp.CommandManager.ControlDefinitions.AddButtonDefinition(GetButtonName(), GetInternalName(),
                CommandType, null, GetDescriptionText(), GetToolTipText(), SmallIcon, LargeIcon);
            Definition.Enabled = true;
            Definition.OnExecute += OnExecute;
        }

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
        protected virtual string GetInternalName() => Guid.NewGuid().ToString();

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
        private bool AddToRibbon => true;
        private ButtonDefinition Definition { get; }
        
        /// <summary>
        /// Controls whether the button is enabled or not. If false, the button will be disabled (greyed out) and cannot be clicked.
        /// </summary>
        public bool Enabled
        {
            get => Definition.Enabled;
            set => Definition.Enabled = value;
        }

        public void Initialize()
        {
            if (AddToRibbon)
            {
                var uiMan = AddinServer.InventorApp.UserInterfaceManager;
                var ribbon = uiMan.Ribbons.Cast<Ribbon>()
                    .FirstOrDefault(r => r.InternalName == GetRibbonName());

                if (ribbon == null)
                {
                    throw new Exception($"Ribbon {GetRibbonName()} not found");
                }

                var tab = ribbon.RibbonTabs.Cast<RibbonTab>()
                    .FirstOrDefault(t => t.InternalName == GetRibbonTabName());

                if (tab == null)
                {
                    tab = ribbon.RibbonTabs.Add(GetRibbonTabName(), GetRibbonTabName(), Guid.NewGuid().ToString());
                }

                var existingPanel = tab.RibbonPanels.Cast<RibbonPanel>()
                    .FirstOrDefault(p => p.InternalName == GetRibbonPanelName());

                if (existingPanel != null)
                {
                    existingPanel.Delete();
                }
                
                var panel = tab.RibbonPanels.Add(GetRibbonPanelName(), GetRibbonPanelName(), Guid.NewGuid().ToString());
                
                panel.CommandControls.AddButton(Definition, UseLargeIcon, ShowText);
            }
            else
            {
                throw new NotImplementedException("AddToRibbon is false");
            }
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
        public ImageConverter() : base(string.Empty) { }

        public static IPictureDisp BitmapToPicture(Bitmap bitmap)
        {
            return (IPictureDisp)GetIPictureDispFromPicture(bitmap);
        }
    }
}