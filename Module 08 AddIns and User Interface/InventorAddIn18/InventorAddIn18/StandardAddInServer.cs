using Inventor;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Windows;

namespace InventorAddIn18
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("24d598a2-e5d5-45e4-bd26-127c995c242f"), ProgIdAttribute("InventorAddIn18.StandardAddInServer")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;
        private UserInterfaceEvents m_uiEvents;
        private ButtonDefinition m_sampleButton;
        public static string clientID = "24d598a2-e5d5-45e4-bd26-127c995c242f";

        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;
            m_uiEvents = m_inventorApplication.UserInterfaceManager.UserInterfaceEvents;
            
            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.

            Inventor.ControlDefinitions controlDefs=m_inventorApplication.CommandManager.ControlDefinitions;
            m_sampleButton = controlDefs.AddButtonDefinition("Sample", "My Sample",CommandTypesEnum.kShapeEditCmdType,clientID);

            //register event handlers
            m_sampleButton.OnExecute += m_sampleButton_OnExecute;
            m_uiEvents.OnResetRibbonInterface+= m_uiEvents_OnResetRibbonInterface;

            if (firstTime)
            {
                AddToUserInterface();
            }
        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            m_uiEvents = null;
            m_inventorApplication = null;

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
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #endregion

        private void  AddToUserInterface()
        {
           //get the part ribbon 
            Ribbon partRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Part"];
           //Get the "Tools" tab
            RibbonTab toolstab = partRibbon.RibbonTabs["id_TabTools"];

            //crete a new panel
            RibbonPanel custompanel = toolstab.RibbonPanels.Add("Sample", "My Sample", clientID);

            //add button
            custompanel.CommandControls.AddButton(m_sampleButton);
        }
        private void m_uiEvents_OnResetRibbonInterface(NameValueMap Context)
        {
            // The ribbon was reset, so add back the add-ins user-interface.
            AddToUserInterface();
        }

        private void m_sampleButton_OnExecute(NameValueMap Context) 
        {
            //MsgBox("Button was clicked.");
            MessageBox.Show("I was clicked");
            
        }


    }
}
