using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Inventor;
using InventorAddinTemplate.Buttons;

namespace InventorAddinTemplate
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [Guid("35678d1f-aeb7-4a22-9fef-624c83e66007")]
    public class AddinServer : Inventor.ApplicationAddInServer
    {
        public static Inventor.Application InventorApp;

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.            

            // Initialize AddIn members.
            InventorApp = addInSiteObject.Application;

            InventorApp.ApplicationEvents.OnApplicationOptionChange += UpdateButtons;

            try
            {
                if (firstTime)
                {
                    InitializeUIComponents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load the View Reference Addin.");
            }

        }

        private void InitializeUIComponents()
        {
            var buttons = Assembly.GetExecutingAssembly().GetTypes()
                .OfType<InventorButton>()
                .Select(button => Activator.CreateInstance(button.GetType()));
            
            
            
            var createButton = new CreateReferencesButton();
            var removeButton = new RemoveReferencesButton();
            var configButton = new ConfigureButton();

            UserInterfaceManager uiMan = InventorApp.UserInterfaceManager;

            if (uiMan.InterfaceStyle == InterfaceStyleEnum.kRibbonInterface)
            {
                Ribbon ribbon = uiMan.Ribbons["Drawing"];
                RibbonTab tab = ribbon.RibbonTabs["id_TabPlaceViews"];
                
                var existingPanel = tab.RibbonPanels.Cast<RibbonPanel>().FirstOrDefault(p => p.InternalName == AppConstants.UIPanelId);
                if (existingPanel != null)
                {
                    existingPanel.Delete();
                }

                RibbonPanel panel = tab.RibbonPanels.Add("View Reference", "vr_Panel", Guid.NewGuid().ToString());
                CommandControls controls = panel.CommandControls;

                controls.AddButton(createButton.Definition, true, true);
                controls.AddButton(removeButton.Definition, true, true);
                controls.AddButton(configButton.Definition, false, true);
            }
        }
        
        private void UpdateButtons(EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
        {
            if (beforeOrAfter == EventTimingEnum.kAfter)
            {
                InitializeUIComponents();
                
                handlingCode = HandlingCodeEnum.kEventHandled;
            }
            
            handlingCode = HandlingCodeEnum.kEventNotHandled;
        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated


            // Release objects.
            InventorApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                return null;
            }
        }

        #endregion

    }
}
