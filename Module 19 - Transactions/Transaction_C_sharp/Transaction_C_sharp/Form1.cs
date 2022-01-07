using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;

namespace Transaction_C_sharp
{
    
    public partial class Form1 : Form
    {
        public static Inventor.Application _invApp = null;
        public Form1()
        {
            InitializeComponent();

            try
            {
                try
                {
                    _invApp = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");

                }catch
                {
                    Type invType = System.Type.GetTypeFromProgID("Inventor.Application");
                    _invApp= (Inventor.Application)System.Activator.CreateInstance(invType);
                    _invApp.Visible = true;
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Could not create an instance");
            }
        }
        private String OpenFile(String StrFilter)
        {
            String filename = "";
            System.Windows.Forms.OpenFileDialog ofDlg=new OpenFileDialog();
            String user=System.Windows.Forms.SystemInformation.UserName;

            ofDlg.Title = "Open File";
            ofDlg.InitialDirectory = @"C:\Documents and Settings\" + user + @"\Desktop\";
            ofDlg.Filter = StrFilter; //Example: "Inventor files (*.ipt; *.iam; *.idw)|*.ipt;*.iam;*.idw"
            ofDlg.FilterIndex = 1;
            ofDlg.RestoreDirectory = true;

            if(ofDlg.ShowDialog() == DialogResult.OK)
            {
                filename = ofDlg.FileName;
            }
            return filename;
        }

        private void UsingTrans_Click(object sender, EventArgs e)
        {
            PartDocument oDoc = (PartDocument)_invApp.ActiveDocument;
            PartComponentDefinition oCompdef = oDoc.ComponentDefinition;

            WorkPlane oWorkPlane = oDoc.ComponentDefinition.WorkPlanes[2];

            PlanarSketch oSketch = oDoc.ComponentDefinition.Sketches.Add(oWorkPlane, false);

           // PlanarSketch oSketch = oCompdef.Sketches[1];
            TransientGeometry oTG = _invApp.TransientGeometry;

            //get the transaction manager
            TransactionManager oTxnMgr=_invApp.TransactionManager;

            //start a regular transaction
            //Transaction oTxn = oTxnMgr.StartTransaction((_Document)oDoc, "My Rectangular Command");
            Transaction oTxn = oTxnMgr.StartTransactionForDocumentOpen("My Rectangular Command");

            SketchLine oLine;
            oLine = oSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(0, 0), oTG.CreatePoint2d(1, 0));
            oLine = oSketch.SketchLines.AddByTwoPoints(oLine.EndSketchPoint, oTG.CreatePoint2d(1, 2));
            oLine = oSketch.SketchLines.AddByTwoPoints(oLine.EndSketchPoint, oTG.CreatePoint2d(0, 2));
            oLine = oSketch.SketchLines.AddByTwoPoints(oLine.EndSketchPoint, oTG.CreatePoint2d(0, 0));

            oTxn.End();


        }

        private void CreateMyProperty_Click(object sender, EventArgs e)
        {
            PropertySet oPropSet;
            Property oProp;
            TransactionManager oTxnMgr;
            Transaction oTxn;
            oTxnMgr = _invApp.TransactionManager;
            oTxn = oTxnMgr.StartTransactionForDocumentOpen("Stuff");
            oPropSet = _invApp.ActiveDocument.PropertySets.Add("MyPropSet");
            oProp=oPropSet.Add(5.0,"MyProp");
            oTxn.End();




        }

        private void start_end_abort_Click(object sender, EventArgs e)
        {
            Document oDoc=_invApp.ActiveDocument;

            //get a transaction manager
            TransactionManager oTxnMng=_invApp.TransactionManager;

            //start a tranaaction
            Transaction oTxn = oTxnMng.StartTransactionForDocumentOpen("MyTransaction");

            //try to open an invalid document (That doesnt exist)
            try
            {
                Document dummy = _invApp.Documents.Open(@"C:\dummy.ipt");
                oTxn.End();
            }catch
            {
                System.Windows.Forms.MessageBox.Show("Unrecoverable error occurred during the operation");
                oTxn.Abort();
            }
        }

        private void checkPoints_Click(object sender, EventArgs e)
        {
            PartDocument oPdoc= (PartDocument)_invApp.ActiveDocument;
            PartComponentDefinition oCompDef = oPdoc.ComponentDefinition;

            PlanarSketch oSketch = oCompDef.Sketches[1];
            TransactionManager oTxnMgr= _invApp.TransactionManager;

            Transaction oTxn = oTxnMgr.StartTransactionForDocumentOpen("Checkpoint Txn");

            CheckPoint oChkpt=null;
            try
            {
                //
                //Perform the creation of extrude profile
                //

                oSketch.SketchCircles.AddByCenterRadius(_invApp.TransientGeometry.CreatePoint2d(0, 0), 10);
                
                //Create a checkpoint before the seond job
                oChkpt = oTxnMgr.SetCheckPoint();

                ExtrudeDefinition oExDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oSketch.Profiles.AddForSolid(), PartFeatureOperationEnum.kJoinOperation);

                //we intentionally set the extent distance as 0. this will fail extrudefeatures.add
                oExDef.SetDistanceExtent(0, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
                oCompDef.Features.ExtrudeFeatures.Add(oExDef);

                //end transaction
                oTxn.End();


            }catch
            {
                DialogResult oRst = MessageBox.Show("Extrude operation failed. Modify profile ?", "error", MessageBoxButtons.YesNo);
                
                if(oRst == DialogResult.Yes)
                {
                    oTxnMgr.GoToCheckPoint(oChkpt);

                }
                else
                {
                    oTxn.Abort();
                }
            }

        }

        private void Parent_Child_Trasns_Click(object sender, EventArgs e)
        {
            PartDocument oDoc = (PartDocument)_invApp.ActiveDocument;
            PartComponentDefinition oCompdef = oDoc.ComponentDefinition;

            PlanarSketch oSketch = oCompdef.Sketches[1];
            TransientGeometry oTG = _invApp.TransientGeometry;

            //get the transaction manager
            TransactionManager oTxnMgr=_invApp.TransactionManager;

            //start a regular transaction
            Transaction oTxn1 = oTxnMgr.StartTransactionForDocumentOpen("My Txn");

            //draw a line
            SketchLine oLine = oSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(0, 0), oTG.CreatePoint2d(1, 0));

            //start a nested transaction
            Transaction oTxn2 = oTxnMgr.StartTransactionForDocumentOpen("My Child Txn");

            //draw a circle
            SketchCircle oCirc = oSketch.SketchCircles.AddByCenterRadius(oLine.EndSketchPoint, 3);

            oTxn2.End();
            oTxn1.End();
        }
        #region Transaction Events

      
        private void startEvents_Click(object sender, EventArgs e)
        {
            TransactionManager oTxnMgr=_invApp.TransactionManager;
            TransactionEvents oTransEvent = oTxnMgr.TransactionEvents;
            oTransEvent.OnUndo += OTransEvent_OnUndo;
            oTransEvent.OnCommit += OTransEvent_OnCommit;
            oTransEvent.OnDelete += OTransEvent_OnDelete;
            oTransEvent.OnRedo += OTransEvent_OnRedo;
        }
        private void stopEvents_Click(object sender, EventArgs e)
        {
            TransactionManager oTxnMgr = _invApp.TransactionManager;
            TransactionEvents oTransEvent = oTxnMgr.TransactionEvents;
            oTransEvent.OnUndo += OTransEvent_OnUndo;

        }
        //private void onUndo_Click(object sender, EventArgs e)
        //{
        //    TransactionManager oTxnMgr = _invApp.TransactionManager;
        //    TransactionEvents oTransEvent = oTxnMgr.TransactionEvents;
        //    oTransEvent.OnUndo += OTransEvent_OnUndo;
        //}
        //private void onDelete_Click(object sender, EventArgs e)
        //{
        //    TransactionManager oTxnMgr = _invApp.TransactionManager;
        //    TransactionEvents oTransEvent = oTxnMgr.TransactionEvents;
        //    oTransEvent.OnDelete += OTransEvent_OnDelete;
        //}
        //private void onCommit_Click(object sender, EventArgs e)
        //{
        //    TransactionManager oTxnMgr = _invApp.TransactionManager;
        //    TransactionEvents oTransEvent = oTxnMgr.TransactionEvents;
        //    oTransEvent.OnCommit += OTransEvent_OnCommit;
        //}
        //private void onRedo_Click(object sender, EventArgs e)
        //{
        //    TransactionManager oTxnMgr = _invApp.TransactionManager;
        //    TransactionEvents oTransEvent = oTxnMgr.TransactionEvents;
        //    oTransEvent.OnRedo += OTransEvent_OnRedo;


        //}

        private void OTransEvent_OnDelete(Transaction TransactionObject, NameValueMap Context, EventTimingEnum BeforeOrAfter)
        {
            System.Windows.Forms.MessageBox.Show("Delete [" + TransactionObject.DisplayName + "]");
           // HandlingCode = HandlingCodeEnum.kEventHandled;
        }

        private void OTransEvent_OnCommit(Transaction TransactionObject, NameValueMap Context, EventTimingEnum BeforeOrAfter, out HandlingCodeEnum HandlingCode)
        {
            System.Windows.Forms.MessageBox.Show("Commit [" + TransactionObject.DisplayName + "]");
            HandlingCode = HandlingCodeEnum.kEventHandled;
        }

        private void OTransEvent_OnUndo(Transaction TransactionObject, NameValueMap Context, EventTimingEnum BeforeOrAfter, out HandlingCodeEnum HandlingCode)
        {
            System.Windows.Forms.MessageBox.Show("Undo [" + TransactionObject.DisplayName + "]");
            HandlingCode = HandlingCodeEnum.kEventHandled;
        }

        #endregion


        private void OTransEvent_OnRedo(Transaction TransactionObject, NameValueMap Context, EventTimingEnum BeforeOrAfter, out HandlingCodeEnum HandlingCode)
        {
            System.Windows.Forms.MessageBox.Show("Redo [" + TransactionObject.DisplayName + "]");
            HandlingCode = HandlingCodeEnum.kEventHandled;
        }


    }
}
