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

namespace ASM
{
    public partial class Form1 : Form
    {
        public static Inventor.Application _invApp=null;

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
                    Type InvType = System.Type.GetTypeFromProgID("Inventor.Application");
                    _invApp=(Inventor.Application)System.Activator.CreateInstance(InvType);
                    _invApp.Visible = true;
                }
            }catch
            {
                System.Windows.Forms.MessageBox.Show("Error ! Could not create an Inventor Instance");
            }


            //VectorControl ts = new VectorControl();
            //this.Controls.Add(ts);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            VectorControl1.SetApp = _invApp;
            VectorControl2.SetApp = _invApp;
        }

        private void Assembly_Click(object sender, EventArgs e)
        {
            AssemblyDocument oAsm = (AssemblyDocument)_invApp.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, _invApp.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject, SystemOfMeasureEnum.kDefaultSystemOfMeasure, DraftingStandardEnum.kDefault_DraftingStandard, null), true);
        }

        private void Occurence_Click(object sender, EventArgs e)
        {
            if(_invApp.ActiveDocument != null)
            {
                if(_invApp.ActiveDocument.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    AssemblyDocument oAsm=_invApp.ActiveDocument as AssemblyDocument;
                    Inventor.FileDialog oDLG = null;
                    _invApp.CreateFileDialog(out oDLG);

                    //oDLG.FileName = "C:\Temp\"
                    oDLG.Filter = "Inventor Files (*.iam;*.ipt)|*.iam;*.ipt";
                    oDLG.DialogTitle = "Insert occurrence";

                    oDLG.ShowOpen();

                    if ((!string.IsNullOrEmpty(oDLG.FileName)))
                    {

                        Matrix pos = _invApp.TransientGeometry.CreateMatrix();
                        var oNewOcc = oAsm.ComponentDefinition.Occurrences.Add(oDLG.FileName, pos);


                        _invApp.ActiveView.Update();

                    }


                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("An Assembly document must be active...", "Error");
                    return;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("An Assembly document must be active...", "Error");
                return;

            }
        }
        private void Transform_Click(object sender, EventArgs e)
        { 


            {
                if (_invApp.ActiveDocument != null)
            {
                if (_invApp.ActiveDocument.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    if ((_invApp.ActiveDocument.SelectSet.Count == 1))
                    {
                        if (((_invApp.ActiveDocument.SelectSet[1]) is ComponentOccurrence))
                        {
                            ComponentOccurrence oCompOccurrence = _invApp.ActiveDocument.SelectSet[1] as ComponentOccurrence;

                            Matrix oTransfo = _invApp.TransientGeometry.CreateMatrix();
                            if ((Angle.Text.Length == 0))
                            {
                                Angle.Text = "0";
                            }
                            double angle = System.Double.Parse(Angle.Text);
                            Vector trans =   VectorControl1.Vector;
                            Vector axis = VectorControl2.Vector;

                            
                            if ((axis.Length == 0))
                            {
                                System.Windows.Forms.MessageBox.Show("Rotation Axis cannot be null", "Error");
                                return;
                            }
                            oTransfo.SetToRotation(angle * Math.Atan(1) * 4 / 180.0, axis, oCompOccurrence.MassProperties.CenterOfMass);

                            Vector oFinalTx = oTransfo.Translation;
                            oFinalTx.AddVector(trans);

                            oTransfo.SetTranslation(oFinalTx, false);

                            Matrix oNewTransfo = oCompOccurrence.Transformation;

                            oNewTransfo.TransformBy(oTransfo);

                            oCompOccurrence.Transformation = oNewTransfo;

                            _invApp.ActiveView.Update();
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Not an occurrence...", "Error");
                            return;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("A single occurrence must be selected...", "Error");
                        return;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("An Assembly document must be active...", "Error");
                    return;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("An Assembly document must be active...", "Error");
                return;
            }

        }
        }

        #region Lab_Demo_COnstraints

        void createPart1()
        {
            // create a new part

            PartDocument oDoc = (PartDocument)_invApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject);
            PartComponentDefinition oDef = oDoc.ComponentDefinition;

            TransientGeometry oTG = _invApp.TransientGeometry;

            // create sketch elements
            PlanarSketch oSketch = oDef.Sketches.Add(oDef.WorkPlanes[3]);
            SketchCircle oCircle = oSketch.SketchCircles.AddByCenterRadius(oTG.CreatePoint2d(0, 0), 1);

            Profile oProfile = oSketch.Profiles.AddForSolid();

            // create a cylinder feature
            ExtrudeDefinition oExtrudDef = oDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudDef.SetDistanceExtent(5, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature oExtrudeF = oDef.Features.ExtrudeFeatures.Add(oExtrudDef);

            //add an attribute to cylinder face         
            Face oFace = oExtrudeF.SideFaces[1];

            AttributeSet oAttSet = default(AttributeSet);
            Inventor.Attribute oAtt = null;
            oAttSet = oFace.AttributeSets.Add("demoAttset");
            oAtt = oAttSet.Add("demoAtt", ValueTypeEnum.kStringType, "namedEdge");
            if (System.IO.File.Exists("c:\temp\test1.ipt"))
            {
                System.IO.File.Delete("c:\temp\test1.ipt");
            }

            oDoc.SaveAs("c:\\temp\\test1.ipt", false);

        }

        private void createPart2()
        {
            // create a new part
            PartDocument oDoc = (PartDocument)_invApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject);
            PartComponentDefinition oDef = oDoc.ComponentDefinition;

            TransientGeometry oTG = _invApp.TransientGeometry;

            // create sketch elements
            PlanarSketch oSketch = oDef.Sketches.Add(oDef.WorkPlanes[3]);
            oSketch.SketchLines.AddAsTwoPointRectangle(oTG.CreatePoint2d(-5, -5), oTG.CreatePoint2d(5, 5));

            SketchPoint oSketchPt = oSketch.SketchPoints.Add(oTG.CreatePoint2d(0, 0));

            Profile oProfile = oSketch.Profiles.AddForSolid();
            // create a plate with a hole feature
            ExtrudeDefinition oExtrudDef = oDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);
            oExtrudDef.SetDistanceExtent(1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature oExtrudeF = oDef.Features.ExtrudeFeatures.Add(oExtrudDef);

            // Create an object collection for the hole center points. 
            ObjectCollection oHoleCenters = default(ObjectCollection);
            oHoleCenters = _invApp.TransientObjects.CreateObjectCollection();

            oHoleCenters.Add(oSketchPt);

            // create hole feature
            HolePlacementDefinition oHPdef = (HolePlacementDefinition)oDef.Features.HoleFeatures.CreateSketchPlacementDefinition(oHoleCenters);

            HoleFeature oHoleF = oDef.Features.HoleFeatures.AddDrilledByThroughAllExtent(oHPdef, "2", PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

            Face oFace = oHoleF.SideFaces[1];
            AttributeSet oAttSet = default(AttributeSet);
            Inventor.Attribute oAtt = null;
            oAttSet = oFace.AttributeSets.Add("demoAttset");
            oAtt = oAttSet.Add("demoAtt", ValueTypeEnum.kStringType, "namedEdge");
            if (System.IO.File.Exists("c:\temp\test2.ipt"))
            {
                System.IO.File.Delete("c:\temp\test2.ipt");
            }


            oDoc.SaveAs("c:\\temp\\test2.ipt", false);

        }

        private void insertPartsAndMateEdges()
        {

            // create an assembly
            AssemblyDocument oAssDoc = (AssemblyDocument)_invApp.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject);
            AssemblyComponentDefinition oAssDef = oAssDoc.ComponentDefinition;

            Matrix oM = _invApp.TransientGeometry.CreateMatrix();

            //place the two parts
            ComponentOccurrence oOcc1 = oAssDef.Occurrences.Add("c:\\temp\\test1.ipt", oM);
            ComponentOccurrence oOcc2 = oAssDef.Occurrences.Add("c:\\temp\\test2.ipt", oM);

            // find the two faces to mate
            PartDocument oDoc1 = (PartDocument)oOcc1.Definition.Document;
            ObjectCollection oObjCollection = oDoc1.AttributeManager.FindObjects("demoAttset", "demoAtt");

            Face oFace1 = null;
            if (oObjCollection[1] is Face)
            {
                oFace1 = (Face)oObjCollection[1];
            }

            PartDocument oDoc2 = (PartDocument)oOcc2.Definition.Document;
            oObjCollection = oDoc2.AttributeManager.FindObjects("demoAttset", "demoAtt");

            Face oFace2 = null;
            if (oObjCollection[1] is Face)
            {
                oFace2 = (Face)oObjCollection[1];
            }

            Object tempObj;
            //create the proxy objects for the two faces

            oOcc1.CreateGeometryProxy(oFace1, out tempObj);
            FaceProxy oAsmProxyFace1 = (FaceProxy)tempObj;

            oOcc2.CreateGeometryProxy(oFace2, out tempObj);
            FaceProxy oAsmProxyFace2 = (FaceProxy)tempObj;

            // add the mate constraint
            oAssDef.Constraints.AddMateConstraint(oAsmProxyFace1, oAsmProxyFace2, 0);

        }
        private void Lab_Click(object sender, EventArgs e)
        {
            // create part1
            createPart1();
            // create part2
            createPart2();
            // create assembly, place the two parts and mate two faces
            insertPartsAndMateEdges();
        }


        #endregion




        //

    }
}
