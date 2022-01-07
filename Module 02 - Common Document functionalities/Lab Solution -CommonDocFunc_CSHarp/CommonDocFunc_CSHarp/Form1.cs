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

namespace Lab_Solution__CommonDocFunc_CSHarp
{
    
    public partial class Form1 : Form
    {
        public static Inventor.Application m_InvApp = null;
        public static UnitsOfMeasure mUOM;
        public Form1()
        {
            InitializeComponent();
            try
            {
                try
                {
                    m_InvApp = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Applicayion");
                }catch
                {
                    Type invAppType = System.Type.GetTypeFromProgID("Inventor.Application");
                    m_InvApp= (Inventor.Application)System.Activator.CreateInstance(invAppType);  
                    m_InvApp.Visible = true;    

                }
            }catch
            {
                System.Windows.Forms.MessageBox.Show("Error Opening Inventor Application!");
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if(tbx1.Text.Length > 0)
            {
                PartDocument pDoc = (PartDocument)m_InvApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject,m_InvApp.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject), true); //create a part document

                //Access Inventor Summary Information
                Inventor.PropertySet oPropSet = (PropertySet)pDoc.PropertySets["{F29F85E0-4FF9-1068-AB91-08002B27B3D9}"];
                //us
                Inventor.Property oProp = (Property)oPropSet["Author"];

                oProp.Value = tbx1.Text;
                //get user defined property sets
                oPropSet = (PropertySet)pDoc.PropertySets["{D5CDD505-2E9C-101B-9397-08002B2CF9AE}"];
               oProp = (Property)oPropSet.Add("MypropVal", "MyProp");

                //save the document
                Inventor.FileDialog oDLG = null;
                m_InvApp.CreateFileDialog(out oDLG);
                oDLG.FileName = @"C:\Temp\NewPart.ipt";
                oDLG.Filter = "Inventor Files (*.ipt)|*.ipt";
                oDLG.DialogTitle = "Save Part";
                oDLG.ShowSave();
                if(oDLG.FileName != "")
                {
                    pDoc.SaveAs(oDLG.FileName, false);

                }

            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            try
            {
                PartDocument oPdoc = (PartDocument)m_InvApp.ActiveDocument;
                Parameter Oparam = oPdoc.ComponentDefinition.Parameters["Length"];

                Double value = mUOM.GetValueFromExpression(tbx2.Text, UnitsTypeEnum.kDefaultDisplayLengthUnits);
                Oparam.Value = value;
                oPdoc.Update();
                m_InvApp.ActiveView.Fit();

            }catch (Exception ex)
            {
                MessageBox.Show("Error: Parameter \"Length\" does not exist in active document");
            }
        }

        private void tbx2_TextChanged(object sender, EventArgs e)
        {
            mUOM = m_InvApp.ActiveDocument.UnitsOfMeasure;
            if(tbx2.Text.Length > 0)
            {
                if(mUOM.IsExpressionValid(tbx2.Text, UnitsTypeEnum.kDefaultDisplayLengthUnits))
                {
                    tbx2.ForeColor = System.Drawing.Color.Black;
                    btn2.Enabled = true;
                }
                else
                {
                    tbx2.ForeColor = System.Drawing.Color.Red;
                    btn2.Enabled = false;

                }
            }
        }
    }
}
