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

namespace BRep_
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
                }
                catch
                {
                    Type invType = System.Type.GetTypeFromProgID("Inventor.Application");
                    _invApp = (Inventor.Application)System.Activator.CreateInstance(invType);
                    _invApp.Visible = true;

                }
            } catch
            {
                System.Windows.Forms.MessageBox.Show("Error: Could not create an Instance");
            }


        }

        private void btn1_Click(object sender, EventArgs e)
        {
           if(_invApp.ActiveDocument != null)
            {
                if(_invApp.ActiveDocument.DocumentType == DocumentTypeEnum.kPartDocumentObject)
                {
                    PartDocument oDoc = (PartDocument)_invApp.ActiveDocument;
                    if (oDoc.SelectSet.Count == 2)
                    {
                        if(((oDoc.SelectSet[1]) is SketchLine &  (oDoc.SelectSet[2]) is SketchCircle))
                        {
                            SketchLine oSketchLine = oDoc.SelectSet[1] as SketchLine;
                            SketchCircle oSketchCircle_ = oDoc.SelectSet[2] as SketchCircle;

                            LineSegment2d oLineSeg2d_ = oSketchLine.Geometry;
                            Circle2d oCircle2d_ = oSketchCircle_.Geometry;

                            ObjectsEnumerator objectsEnum_ = oLineSeg2d_.IntersectWithCurve(oCircle2d_, 0.0001);

                            if ((objectsEnum_ == null))
                            {
                                System.Windows.Forms.MessageBox.Show("No physical intersection between Line and Circle");
                                return;
                            }

                            string strResult_ = "Intersection point(s): \n";

                            var i = 0;
                            
                            for (i = 1; i <= objectsEnum_.Count; i++)
                            {
                                Point2d oPoint = objectsEnum_[i] as Point2d;
                                strResult_ += "[" + oPoint.X.ToString("F2") + ", " + oPoint.Y.ToString("F2") + "] \n";
                            }

                            System.Windows.Forms.MessageBox.Show(strResult_, "BRep Sample");
                            return;
                        }
                    }

                }
            }
            
            
            
        


        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (((_invApp.ActiveDocument != null)))
            {
                if ((_invApp.ActiveDocument.DocumentType == DocumentTypeEnum.kPartDocumentObject))
                {
                    PartDocument oDoc = _invApp.ActiveDocument as PartDocument;

                    if ((oDoc.SelectSet.Count == 2))
                    {
                        if (((oDoc.SelectSet[1]) is SketchLine & (oDoc.SelectSet[2]) is SketchCircle))
                        {

                            SketchLine oSketchLine = oDoc.SelectSet[1] as SketchLine;
                            SketchCircle oSketchCircle = oDoc.SelectSet[2] as SketchCircle;

                            Line2d oLine2d = _invApp.TransientGeometry.CreateLine2d(oSketchLine.Geometry.StartPoint, oSketchLine.Geometry.Direction);
                            Circle2d oCircle2d = oSketchCircle.Geometry;

                            ObjectsEnumerator objectsEnum = oLine2d.IntersectWithCurve(oCircle2d, 0.0001);

                            if ((objectsEnum == null))
                            {
                                System.Windows.Forms.MessageBox.Show("No intersection between extended Line and Circle", "BRep Sample");
                                return;
                            }

                            string strResult = "Intersection point(s): \n";

                            int i = 0;
                            for (i = 1; i <= objectsEnum.Count; i++)
                            {
                                Point2d oPoint = objectsEnum[i] as Point2d;
                                strResult += "[" + oPoint.X.ToString("F2") + ", " + oPoint.Y.ToString("F2") + "]" + "\n";
                            }

                            System.Windows.Forms.MessageBox.Show(strResult, "BRep Sample");
                            return;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Entity 1 must be a SketchLine, Entity 2 must be a SketchCircle", "BRep Sample");
                            return;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Incorrect selection of sketch entities", "BRep Sample");
                        return;
                    }
                }
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (((_invApp.ActiveDocument != null)))
            {
                if ((_invApp.ActiveDocument.DocumentType == DocumentTypeEnum.kPartDocumentObject))
                {
                    PartDocument oDoc = _invApp.ActiveDocument as PartDocument;

                    if ((oDoc.SelectSet.Count == 1))
                    {
                        if (((oDoc.SelectSet[1]) is Edge))
                        {
                            Edge oEdge = oDoc.SelectSet[1] as Edge;

                            CurveEvaluator oCurveEval = oEdge.Evaluator;

                            double MinParam = 0;
                            double MaxParam = 0;

                            oCurveEval.GetParamExtents(out MinParam, out MaxParam);

                            double length = 0;
                            oCurveEval.GetLengthAtParam(MinParam, MaxParam, out length);

                            double MidParam = 0;
                            oCurveEval.GetParamAtLength(MinParam, length * 0.5, out MidParam);

                            double[] Params = { MidParam };

                            double[] Points = new double[3 * Params.Length];
                            oCurveEval.GetPointAtParam(ref Params, ref Points);

                            double[] Directions = new double[3 * Params.Length];
                            double[] Curvatures = new double[Params.Length];
                            oCurveEval.GetCurvature(ref Params, ref Directions, ref Curvatures);

                            double[] Tangents = new double[3 * Params.Length];
                            oCurveEval.GetTangent(ref Params, ref Tangents);

                            double[] FirstDeriv = new double[3 * Params.Length];
                            oCurveEval.GetFirstDerivatives(ref Params, ref FirstDeriv);


                            string strResult = "Curve Properties: \n\n";

                            strResult += " - Length: " + length.ToString("F2") + "\n\n";

                            strResult += " - Middle point: [" + Points[0].ToString("F2") + ", " + Points[1].ToString("F2") + ", " + Points[2].ToString("F2") + "]" + "\n\n";

                            strResult += " - Curvature: " + Curvatures[0].ToString("F2") + "\n\n";

                            strResult += " - Tangent: [" + Tangents[0].ToString("F2") + ", " + Tangents[1].ToString("F2") + ", " + Tangents[2].ToString("F2") + "]" + "\n\n";

                            strResult += " - First derivative: [" + FirstDeriv[0].ToString("F2") + ", " + FirstDeriv[1].ToString("F2") + ", " + FirstDeriv[2].ToString("F2") + "]" + "\n\n";

                            System.Windows.Forms.MessageBox.Show(strResult, "Curve Evaluator");

                            return;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Selected entity must be an Edge", "Curve Evaluator");
                            return;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("A single Edge must be selected first", "Curve Evaluator");
                        return;
                    }
                }
            }
        }
        // create a feature
        private void createFeature()
        {
            PartDocument oDoc = _invApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject,
                  _invApp.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject,
                                                   SystemOfMeasureEnum.kDefaultSystemOfMeasure,
                                                   DraftingStandardEnum.kDefault_DraftingStandard, null),
                                                   true) as PartDocument;

            // Get the XZ Plane
            WorkPlane oWorkPlane = oDoc.ComponentDefinition.WorkPlanes[2];

            PlanarSketch oSketch = oDoc.ComponentDefinition.Sketches.Add(oWorkPlane, false);

            TransientGeometry oTG = _invApp.TransientGeometry;

            //Create some transient points used for defining the lines (see BRep Module)
            Point2d[] oPoints = new Point2d[5];

            oPoints[0] = oTG.CreatePoint2d(0, 0);
            oPoints[1] = oTG.CreatePoint2d(-10, 0);
            oPoints[2] = oTG.CreatePoint2d(-10, -10);
            oPoints[3] = oTG.CreatePoint2d(5, -10);
            oPoints[4] = oTG.CreatePoint2d(5, -5);

            //Add the sketchlines, coincident constraints will be created automatically 
            //since the "Line.EndSketchPoint" are provided each time we create a new line
            SketchLine[] oLines = new SketchLine[5];

            oLines[0] = oSketch.SketchLines.AddByTwoPoints(oPoints[0], oPoints[1]);
            oLines[1] = oSketch.SketchLines.AddByTwoPoints(oLines[0].EndSketchPoint, oPoints[2]);
            oLines[2] = oSketch.SketchLines.AddByTwoPoints(oLines[1].EndSketchPoint, oPoints[3]);
            oLines[3] = oSketch.SketchLines.AddByTwoPoints(oLines[2].EndSketchPoint, oPoints[4]);

            oSketch.SketchArcs.AddByCenterStartEndPoint(oTG.CreatePoint2d(0, -5), oLines[3].EndSketchPoint, oLines[0].StartSketchPoint, true);

            //Create a profile for the extrusion, here no need to worry since there is only 
            //a single profile that is possible
            Profile oProfile = oSketch.Profiles.AddForSolid(true, null, null);



            // Definition Way:
            PartComponentDefinition oPartDocDef = oDoc.ComponentDefinition;

            // get ExtrudeFeatures collection
            ExtrudeFeatures extrudes = oPartDocDef.Features.ExtrudeFeatures;

            // Create an extrude definition in the new surface body
            ExtrudeDefinition extrudeDef = extrudes.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);

            // Modify the extent
            extrudeDef.SetDistanceExtent(1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);


            // Create the extrusion.
            ExtrudeFeature extrude = extrudes.Add(extrudeDef);

        }
        private void btn4_Click(object sender, EventArgs e)
        {
            // create a new document with an extrude feature.
            createFeature();

            PartDocument oPartDoc = _invApp.ActiveDocument as PartDocument;

            // get a start face of the extrude feature
            ExtrudeFeature oExtrudeF = oPartDoc.ComponentDefinition.Features.ExtrudeFeatures[1];
            Face oFirstFace = oExtrudeF.StartFaces[1];

            // add a new sketch on the basis of the start face
            PlanarSketch oSketch = oPartDoc.ComponentDefinition.Sketches.Add(oFirstFace, false);

            TransientGeometry oTG = _invApp.TransientGeometry;

            // create a circle and make a profile from the sketch
            oSketch.SketchCircles.AddByCenterRadius(oTG.CreatePoint2d(0, -5), 1);

            Profile oProfile = oSketch.Profiles.AddForSolid(true, null, null);

            // get ExtrudeFeatures collection
            ExtrudeFeatures extrudes = oPartDoc.ComponentDefinition.Features.ExtrudeFeatures;

            // Create an extrude definition in the new surface body
            ExtrudeDefinition extrudeDef = extrudes.CreateExtrudeDefinition(oProfile, PartFeatureOperationEnum.kJoinOperation);

            // Modify the extent
            extrudeDef.SetDistanceExtent(2, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);

            // Create the extrusion.
            ExtrudeFeature extrude = extrudes.Add(extrudeDef);

            //
            FilletFeatures oFilletFs = oPartDoc.ComponentDefinition.Features.FilletFeatures;

            //create fillet definition
            FilletDefinition oFilletDef = oFilletFs.CreateFilletDefinition();

            // FaceCollection
            FaceCollection oFacesCollOne = _invApp.TransientObjects.CreateFaceCollection();
            oFacesCollOne.Add(oFirstFace);

            FaceCollection oFacesCollTwo = _invApp.TransientObjects.CreateFaceCollection();
            oFacesCollTwo.Add(extrude.SideFaces[1]);//cylinder face

            oFilletDef.AddFaceSet(oFacesCollOne, oFacesCollTwo, 0.1);

            oFilletFs.Add(oFilletDef);


            //Fit the view programmatically
            Camera oCamera = _invApp.ActiveView.Camera;

            oCamera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopRightViewOrientation;
            oCamera.Apply();

            _invApp.ActiveView.Fit(true);
        }
    }
}
