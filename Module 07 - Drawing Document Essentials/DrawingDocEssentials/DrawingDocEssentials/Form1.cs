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

namespace DrawingDocEssentials
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
                    Type InvType = System.Type.GetTypeFromProgID("Inventor.Application");
                    _invApp=(Inventor.Application)System.Activator.CreateInstance(InvType);   
                    _invApp.Visible = true;
                }
            }catch
            {
                System.Windows.Forms.MessageBox.Show("Error! Could not create instance");
            }
        }

        private void AddTitleBlock_Click(object sender, EventArgs e)
        {
            if(_invApp.ActiveDocument.DocumentType != DocumentTypeEnum.kDrawingDocumentObject)
            {
                System.Windows.Forms.MessageBox.Show("Please Open a drawing Document");
            }

            DrawingDocument oDoc = (DrawingDocument)_invApp.ActiveDocument;
            TitleBlockDefinitions oTitle = oDoc.TitleBlockDefinitions;
            TitleBlockDefinition oTitleBlock = oTitle.Add("My Title Block");


        }

        private void CreateBorderDefination_Click(object sender, EventArgs e)
        {
            DrawingDocument oDraw = (DrawingDocument)_invApp.ActiveDocument;
            //creae the new border defination
            BorderDefinition oBorderDef = oDraw.BorderDefinitions.Add("My Border");

            //open the border defination sketch for edit
            DrawingSketch oSketch = null;
            oBorderDef.Edit(out oSketch);

            TransientGeometry oTG = _invApp.TransientGeometry;

            //add geometry using the sketch

            oSketch.SketchLines.AddAsTwoPointRectangle(oTG.CreatePoint2d(2, 2), oTG.CreatePoint2d(53.88, 41.18));

            oBorderDef.ExitEdit(true);

        }

        private void CreateViews_Click(object sender, EventArgs e)
        {
            DrawingDocument oDoc= (DrawingDocument)_invApp.ActiveDocument;
            PartDocument oPartDoc = (PartDocument)_invApp.Documents.Open(@"C:\Temp\NewPart.ipt", false);
            TransientGeometry oTG=_invApp.TransientGeometry;

            //create a base view
            DrawingView oFrontView = oDoc.ActiveSheet.DrawingViews.AddBaseView(
                                                                   (_Document)oPartDoc, oTG.CreatePoint2d(35, 20), 1, ViewOrientationTypeEnum.kFrontViewOrientation, DrawingViewStyleEnum.kHiddenLineDrawingViewStyle);
            //add projected view
            DrawingView oRightView = oDoc.ActiveSheet.DrawingViews.AddProjectedView(oFrontView, oTG.CreatePoint2d(15, 20), DrawingViewStyleEnum.kFromBaseDrawingViewStyle);

            //add Isometric view
            DrawingView oIso = oDoc.ActiveSheet.DrawingViews.AddProjectedView(oFrontView, oTG.CreatePoint2d(15, 35), DrawingViewStyleEnum.kHiddenLineDrawingViewStyle);
            Sheet oSheet = oDoc.Sheets.Add(DrawingSheetSizeEnum.kBDrawingSheetSize);
            oSheet.AddDefaultBorder();
            Inventor.TitleBlock ottle = oSheet.AddTitleBlock(oDoc.TitleBlockDefinitions["ANSI A"]);

            oFrontView.CopyTo(oSheet);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawingDocument oDoc = (DrawingDocument)_invApp.ActiveDocument;
            DrawingView oView = oDoc.ActiveSheet.DrawingViews[1];
            GeneralDimensionsEnumerator oDimColl=oDoc.ActiveSheet.DrawingDimensions.GeneralDimensions.Retrieve(oView);    
        }
    }
}
