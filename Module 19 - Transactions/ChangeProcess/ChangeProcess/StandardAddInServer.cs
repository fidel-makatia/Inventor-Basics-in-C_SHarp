using Inventor;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace ChangeProcess
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("1bafcd77-292f-4c41-8f9e-2bb53466cf45")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;
        private string addin_guid = "{1bafcd77-292f-4c41-8f9e-2bb53466cf45}";
        private string ChangeDefName = "CreateLine";

        //button
        private Inventor.ButtonDefinition m_oButtonDef1;

        //declaration for the change process
        private Inventor.ChangeDefinition m_oChangeDefinition;
        private Inventor.ChangeManager m_oChangeManager;
        private Inventor.ChangeProcessor m_oMyCommandProcessor;

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

            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.

            //add a button to part ribbon
            m_oButtonDef1 = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("Create My Line", "TestTriangleButton", CommandTypesEnum.kNonShapeEditCmdType, addin_guid, "Test of button 1", "Test Button 1");

            //register event handlers
            m_oButtonDef1.OnExecute += M_oButtonDef1_OnExecute;
            m_oMyCommandProcessor.OnExecute += M_oMyCommandProcessor_OnExecute;
            m_oMyCommandProcessor.OnWriteToScript += M_oMyCommandProcessor_OnWriteToScript;
            m_oMyCommandProcessor.OnReadFromScript += M_oMyCommandProcessor_OnReadFromScript;
            m_oChangeDefinition.OnReplay += M_oChangeDefinition_OnReplay;

            //add a button to the first part of the sketch
            var oSketchTab = m_inventorApplication.UserInterfaceManager.Ribbons["Part"].RibbonTabs["id_TabSketch"];
            var oFirstPanel = oSketchTab.RibbonPanels[1];

            oFirstPanel.CommandControls.AddButton(m_oButtonDef1);

            //Create the ChangeDefinitions collection for this Add-In.

            m_oChangeManager = m_inventorApplication.ChangeManager;

            ChangeDefinitions oChangeDefinitions = m_oChangeManager.Add(addin_guid);

            //Create a ChangeDefinition for the command.
            m_oChangeDefinition = oChangeDefinitions.Add(ChangeDefName, "Create My Line");
        }



        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
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
        //button execute event
        private void M_oButtonDef1_OnExecute(NameValueMap Context)
        {
            if (m_oChangeDefinition != null)
            {
                // use the ChangeDefinition we have defined
                // execute the operations 

                ChangeDefinitions colChangeDefs = m_oChangeManager[addin_guid];
                ChangeDefinition mobjChangeDef = colChangeDefs[ChangeDefName];
                m_oMyCommandProcessor = mobjChangeDef.CreateChangeProcessor();

                m_oMyCommandProcessor.Execute(m_inventorApplication.ActiveDocument);
            }
            else
            {  // MsgBox("change definition is null!");
                MessageBox.Show("change definition is null!");
            }
        }
        //Event that's called to execute the change.This is where the actual work
        // within Inventor is done.  Anything done within this event is automatically
        //wrapped in a transaction.   

        private void M_oMyCommandProcessor_OnExecute(_Document Document, NameValueMap Context, ref bool Succeeded)
        {
            PartDocument oPdoc = (PartDocument)m_inventorApplication.ActiveDocument;
            PartComponentDefinition oCompdef=oPdoc.ComponentDefinition;

            TransientGeometry oTG = m_inventorApplication.TransientGeometry;

            SketchLine colLine = oCompdef.Sketches[1].SketchLines.AddByTwoPoints(oTG.CreatePoint2d(0, 0), oTG.CreatePoint2d(4, 0));

        }

        double m_dSize;

        //This event is fired by Inventor when a transcript is being replayed.  
        //Inventor provides the original input that was used.  The global variables 
        //containing the command input should be initialized here.


        private void M_oMyCommandProcessor_OnWriteToScript(_Document Document, NameValueMap Context, out string ResultInputs)
        {
            //Set the return string to contain the triangle size.
            ResultInputs = String.Format( "0.0000000", m_dSize);
        }


        //This event is fired by Inventor when the change processor is running.  You provide
        //a string that encapsulates all of the input required by your command.  If the 
        //transcript is played back this string is passed back to you through the
        //OnReadFromScript event.

        private void M_oMyCommandProcessor_OnReadFromScript(_Document Document, string Inputs, NameValueMap Context)
        {
            m_dSize=Convert.ToDouble(Inputs);
        }

        // Event that 's called by Inventor when a transcript containing 
        //calls to this change processor is being played back.

        private void M_oChangeDefinition_OnReplay(NameValueMap Context, out ChangeProcessor ResultProcessor)
        {
            
            //create a new change processor
            m_oMyCommandProcessor=m_oChangeDefinition.CreateChangeProcessor();

            //set the return argument
            ResultProcessor = m_oMyCommandProcessor;
        }

    }
}
