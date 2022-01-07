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

namespace PartDocEssentials
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
                }
                catch
                {
                    Type invType = System.Type.GetTypeFromProgID("Inventor.Application");
                    _invApp= (Inventor.Application)System.Activator.CreateInstance(invType);
                    _invApp.Visible = true; 
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error: Could not create Instance");
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            PartDocument pDoc = (PartDocument)_invApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, _invApp.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject), true);
            
            //get XZ plane 
            WorkPlane owkPlane = pDoc.ComponentDefinition.WorkPlanes[2];
            
            PlanarSketch oSketch = (PlanarSketch)pDoc.ComponentDefinition.Sketches.Add(owkPlane);
            
            TransientGeometry oTG = _invApp.TransientGeometry;

            //create transient points used for lines

            Point2d[] oPoint = new Point2d[5];

            oPoint[0] = oTG.CreatePoint2d(0, 0);
            oPoint[1] = oTG.CreatePoint2d(-10, 0);
            oPoint[2] = oTG.CreatePoint2d(-10, -10);
            oPoint[3] = oTG.CreatePoint2d(5, -10);
            oPoint[4] = oTG.CreatePoint2d(5, -5);

         //Add the sketchlines, coincident constraints will be created automatically 
        //since the "Line.EndSketchPoint" are provided each time we create a new line
      SketchLine[] oLines= new SketchLine[4];

            oLines[0] = oSketch.SketchLines.AddByTwoPoints(oPoint[0], oPoint[1]);
            oLines[1] = oSketch.SketchLines.AddByTwoPoints(oLines[0].EndSketchPoint, oPoint[2]);
            oLines[2] = oSketch.SketchLines.AddByTwoPoints(oLines[1].EndSketchPoint, oPoint[3]);
            oLines[3] = oSketch.SketchLines.AddByTwoPoints(oLines[2].EndSketchPoint, oPoint[4]);

            oSketch.SketchArcs.AddByCenterStartEndPoint(oTG.CreatePoint2d(0, -5), oLines[3].EndSketchPoint, oLines[0].StartSketchPoint);

        //Create a profile for the extrusion, here no need to worry since there is only 
        //a single profile that is possible
       // Dim oProfile As Profile = oSketch.Profiles.AddForSolid
            Profile oProfile = oSketch.Profiles.AddForSolid();

            //Definition way: create an extrude definition
            ExtrudeDefinition extrudeDef = pDoc.ComponentDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kNewBodyOperation);

            // Modify the extent and taper angles.
            extrudeDef.SetDistanceExtent(8, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            extrudeDef.SetDistanceExtentTwo(20);
            extrudeDef.TaperAngle = "-2 deg";
            extrudeDef.TaperAngleTwo = "-10 deg";

            //create an extrusion

            ExtrudeFeature extrude = pDoc.ComponentDefinition.Features.ExtrudeFeatures.Add(extrudeDef);

            //fit to view
            Camera oCam = _invApp.ActiveView.Camera;
            oCam.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopRightViewOrientation;
            oCam.Apply();

            _invApp.ActiveView.Fit();




        }
    }
}
