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
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Xml.Linq;

namespace SheetMetal
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
            }catch
            {
                System.Windows.Forms.MessageBox.Show("Error: Unable to create Instance");
            }
        }

		//Small helper function that prompts user for a file selection

		private string OpenFile(string StrFilter)
		{
			string returnValue;

			string filename = "";

			System.Windows.Forms.OpenFileDialog ofDlg = new System.Windows.Forms.OpenFileDialog();

			string user = System.Windows.Forms.SystemInformation.UserName;

			ofDlg.Title = "Open File";
			ofDlg.InitialDirectory = "C:\\Documents and Settings\\" + user + "\\Desktop\\";

			ofDlg.Filter = StrFilter; //Example: "Inventor files (*.ipt; *.iam; *.idw)|*.ipt;*.iam;*.idw"
			ofDlg.FilterIndex = 1;
			ofDlg.RestoreDirectory = true;

			if (ofDlg.ShowDialog() == DialogResult.OK)
			{
				filename = ofDlg.FileName;
			}

			returnValue = filename;

			return returnValue;
		}
		private void btn1_Click(object sender, EventArgs e)
        {
			// Set a reference to the sheet metal document.
			// This assumes a part document is active.
			PartDocument oPartDoc;
			oPartDoc = (Inventor.PartDocument)(_invApp.ActiveDocument);

			// Make sure the document is a sheet metal document.
			if (oPartDoc.SubType != "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}")
			{
				MessageBox.Show("A sheet metal document must be open.");
				return;
			}

			// Get the sheet metal component definition. Because this is a part document whose
			// sub type is sheet metal, the document will return a SheetMetalComponentDefinition
			// instead of a PartComponentDefinition.
			SheetMetalComponentDefinition oSheetMetalCompDef;
			oSheetMetalCompDef = (Inventor.SheetMetalComponentDefinition)(oPartDoc.ComponentDefinition);

			// Copy a sheet metal style to create a new one. There will always be at least
			// one style in a document. This sample uses the first style, which is the default.
			SheetMetalStyle oStyle;

			try
			{
				oStyle = oSheetMetalCompDef.SheetMetalStyles.Copy(
				oSheetMetalCompDef.SheetMetalStyles[1],
				"Custom Style");
			}
			catch
			{
				MessageBox.Show("Custom Style already exists :(");
				return;
			}

			// Get the name of the parameter used for the thickness. We need the actual name
			// to use in expressions to set the other values. It's best to get the name rather
			// than hard code it because the name changes with various languages and the user
			// can change the name in the Parameters dialog.

			// This gets the name of the thickness from the component definition.
			string sThicknessName;
			sThicknessName = oSheetMetalCompDef.Thickness.Name;

			// Set the various values associated with the style.
			oStyle.BendRadius = sThicknessName + " * 1.5";
			oStyle.BendReliefWidth = sThicknessName + " / 2";
			oStyle.BendReliefDepth = sThicknessName + " * 1.5";
			oStyle.CornerReliefSize = sThicknessName + " * 2.0";
			oStyle.MinimumRemnant = sThicknessName + " * 2.0";

			oStyle.BendReliefShape = BendReliefShapeEnum.kRoundBendReliefShape;
			oStyle.BendTransition = BendTransitionEnum.kArcBendTransition;
			oStyle.CornerReliefShape = CornerReliefShapeEnum.kRoundCornerReliefShape;

			// Add a linear unfold method.  Unfold methods are now separate
			// from sheet metal styles.
			try
			{
				oSheetMetalCompDef.UnfoldMethods.AddLinearUnfoldMethod(
				"Linear Sample",
				"0.43");
			}
			catch
			{
				MessageBox.Show("Linear Sample UnfoldMethod already exists :(");
				return;
			}

			// Add a bend table fold method. This uses error trapping to catch if an
			// invalid bend table file was specified.
			try
			{
				oSheetMetalCompDef.UnfoldMethods.AddBendTableFromFile(
				"Table Sample",
                OpenFile("Bend Table (*.txt)|*.txt"));
			}
			catch
			{
				MessageBox.Show("Unable to load bend table");
			}

			// Make the new linear method the active unfold method for the document.
			UnfoldMethod oUnfoldMethod;
			oUnfoldMethod = oSheetMetalCompDef.UnfoldMethods["Linear Sample"];
			oStyle.UnfoldMethod = oUnfoldMethod;

			// Activate this style, which will also update the part.
			oStyle.Activate();
		}
    }
}
