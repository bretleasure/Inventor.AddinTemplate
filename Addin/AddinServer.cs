using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Inventor;
using Microsoft.Extensions.DependencyInjection;

namespace Inventor.AddinTemplate.Addin
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [Guid("25E7585D-00D9-47C6-8E1B-734A2186383A")]
    public class AddinServer : Inventor.ApplicationAddInServer
    {
        // The Inventor application instance
        public static Inventor.Application InventorApp;

        public static ServiceProvider Services;
        
        public static Guid AddinGuid = new("25E7585D-00D9-47C6-8E1B-734A2186383A");
        
        List<InventorButton> _buttons;
        List<InventorDockableWindow> _dockableWindows;

        #region ApplicationAddInServer Members

        /// <summary>
        /// This method is called by Inventor when it loads the addin.
        /// The AddInSiteObject provides access to the Inventor Application object.
        /// The FirstTime flag indicates if the addin is loaded for the first time.
        /// </summary>
        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            InventorApp = addInSiteObject.Application;
            InventorApp.ApplicationEvents.OnApplicationOptionChange += UpdateButtons;
            InventorApp.ApplicationEvents.OnReady += OnReady;
        }

        private void OnReady(EventTimingEnum beforeorafter, NameValueMap context, out HandlingCodeEnum handlingcode)
        {
            if (beforeorafter == EventTimingEnum.kAfter)
            {
                SetupDependencyInjection();
                    
                InitializeUIComponents();
                
                handlingcode = HandlingCodeEnum.kEventHandled;
                return;
            }
            
            
            handlingcode = HandlingCodeEnum.kEventNotHandled;
        }

        /// <summary>
        /// Initializes the UI components of the addin.
        /// </summary>
        private void InitializeUIComponents()
        {
            _buttons = Services.GetServices<InventorButton>()
                .Where(button => button.Enabled)
                .OrderBy(button => button.SequenceNumber)
                .ToList();

            _buttons.ForEach(b => b.Initialize());

            // _dockableWindows = Services.GetServices<InventorDockableWindow>().ToList();
            // _dockableWindows.ForEach(d => d.Initialize());
        }

        /// <summary>
        /// Updates the buttons when the application options change.
        /// </summary>
        private void UpdateButtons(EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
        {
            if (beforeOrAfter == EventTimingEnum.kAfter)
            {
                _buttons.ForEach(b => b.Dispose());
                
                InitializeUIComponents();

                handlingCode = HandlingCodeEnum.kEventHandled;
            }

            handlingCode = HandlingCodeEnum.kEventNotHandled;
        }

        /// <summary>
        /// This method is called by Inventor when the AddIn is unloaded.
        /// The AddIn will be unloaded either manually by the user or
        /// when the Inventor session is terminated.
        /// </summary>
        public void Deactivate()
        {
            InventorApp = null;
            _buttons.ForEach(b => b.Dispose());
            _dockableWindows.ForEach(d => ((dynamic)d).Dispose());

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// This method is now obsolete, you should use the
        /// ControlDefinition functionality for implementing commands.
        /// </summary>
        public void ExecuteCommand(int commandID)
        {
        }

        /// <summary>
        /// This property is provided to allow the AddIn to expose an API
        /// of its own to other programs. Typically, this  would be done by
        /// implementing the AddIn's API interface in a class and returning
        /// that class object through this property.
        /// </summary>
        public object Automation
        {
            get
            {
                return null;
            }
        }

        #endregion

        private void SetupDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<Inventor.Application>(InventorApp);
            AddinRegistry.RegisterServices(serviceCollection);
            

            Services = serviceCollection.BuildServiceProvider();
        }

    }
}