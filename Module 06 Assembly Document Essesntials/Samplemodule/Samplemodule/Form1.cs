using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;

namespace Samplemodule
{

    public partial class Form1 : Form
    {

       // public Macros macro;
        public static Inventor.Application _invApp = null;


        public Form1()//Inventor.Application oApp)
        {
            InitializeComponent();

            //macro = new Macros(oApp);

            //System.Reflection.MemberInfo[] methods = macro.GetType().GetMembers();

            //foreach (var member in methods)
            //{
            //    if(member.DeclaringType.Name =="Macros" & member.MemberType== System.Reflection.MemberTypes.Method)
            //    {
            //        ComboBoxMacros.Items.Add(member.Name);
            //    }
            //    if(ComboBoxMacros.Items.Count >= 0)
            //    {
            //        ComboBoxMacros.SelectedIndex = 0;
            //        button1.Enabled = true;
            //    }
            //}

            try
            {
                try
                {
                    _invApp = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");

                }
                catch
                {
                    Type InvType = System.Type.GetTypeFromProgID("Inventor.Application");
                    //remember to explicitly cast from type to application
                    _invApp = (Inventor.Application)System.Activator.CreateInstance(InvType);
                    //set visibility to true
                    _invApp.Visible = true;
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error! Could not create an instance");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string memberName = ComboBoxMacros.SelectedItem.ToString();
            //    object[] @params = null;
            //}
            //catch (Exception ex)
            //{
            //    string message = ex.Message;
            //    MessageBoxButtons buttons = MessageBoxButtons.OK;
            //    DialogResult result = MessageBox.Show(ex.StackTrace,message,buttons,MessageBoxIcon.Exclamation);
            //}
            AssemblyDocument oDoc = (AssemblyDocument)_invApp.ActiveDocument;
            //Call the recursive function to iterate through the assembly tree
            AssemblyTraversalRec((ComponentOccurrences)oDoc.ComponentDefinition.Occurrences, 0);

        }
        private void AssemblyTraversalRec(ComponentOccurrences inCollection, long level)
        {
            //ComponentOccurrence oCompOccurence;
            foreach (ComponentOccurrence oCompOccurence in inCollection)
            {
                if (oCompOccurence.DefinitionDocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    //Debug.Print(Strings.Space((int)(3L * level)) + " - [Asm]  " + oCompOccurrence.Name);
                }
                else
                {
                    // Debug.Print(Strings.Space((int)(3L * level)) + " - [Part] " + oCompOccurrence.Name);
                }
                AssemblyTraversalRec((ComponentOccurrences)oCompOccurence.SubOccurrences, level + 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            AssemblyDocument oDoc = (AssemblyDocument)_invApp.ActiveDocument;
            Matrix oMatrix = _invApp.TransientGeometry.CreateMatrix();
            ComponentOccurrence oOcc = oDoc.ComponentDefinition.Occurrences.Add(@"C:\Temp\Part1.ipt", oMatrix);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AssemblyDocument oDoc = (AssemblyDocument)_invApp.ActiveDocument;
            PartDocument oPdoc = (PartDocument)_invApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, _invApp.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject), false);
            Matrix oMatrix = _invApp.TransientGeometry.CreateMatrix();
            ComponentOccurrence oOcc = oDoc.ComponentDefinition.Occurrences.AddByComponentDefinition((ComponentDefinition)oPdoc.ComponentDefinition, oMatrix);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AssemblyDocument oAsm = (AssemblyDocument)_invApp.ActiveDocument;
            TransientGeometry oTG = _invApp.TransientGeometry;
            ComponentOccurrence oOcc1 = oAsm.ComponentDefinition.Occurrences[1];

            if (oOcc1.Grounded)
            {
                //oOcc1.Grounded=false;
            }
            Matrix oNewMatrix = oOcc1.Transformation;
            oNewMatrix.SetTranslation(oTG.CreateVector(15, 5, 5), false);
            oOcc1.Transformation = oNewMatrix;
            oAsm.Update();

        }



        private class Timer
        {
            DateTime _previous;
            public Timer()
            {
                _previous = DateTime.Now;

            }
            public double GetElapsedSeconds()
            {
                DateTime Now = DateTime.Now;
                TimeSpan Elasped = Now.Subtract(_previous);
                _previous = Now;
                return Elasped.TotalSeconds;


            }
        }

        private void RotateOccurrence(ComponentOccurrence oOccurence, double oAngleDeg)
        {
            TransientGeometry oTG = _invApp.TransientGeometry;
            Matrix oRotMatrix = oTG.CreateMatrix();
            oRotMatrix.SetToRotation(oAngleDeg * (System.Math.PI / 180), oTG.CreateVector(0, 0, 1), oTG.CreatePoint(0, 0, 0));
            Matrix oNewMatrix = oOccurence.Transformation;
            oNewMatrix.PreMultiplyBy(oRotMatrix);
            oOccurence.Transformation = oNewMatrix;
        }

        private void RotateOccurenceTest_Click(object sender, EventArgs e)
        {
            AssemblyDocument oAsm = (AssemblyDocument)_invApp.ActiveDocument;
            ComponentOccurrence oOcc1 = oAsm.ComponentDefinition.Occurrences[1];

            Timer timer = new Timer();
            Double dt = 0;
            while (dt < 10)
            {
                RotateOccurrence(oOcc1, 5);
                System.Windows.Forms.Application.DoEvents();
                dt = dt + timer.GetElapsedSeconds();
            }
        }

        private void Attachattribute_Click(object sender, EventArgs e)
        {
            Document oDoc = _invApp.ActiveDocument;
            if (oDoc.SelectSet.Count != 1)
            {
                System.Windows.Forms.MessageBox.Show("A single Edge must be selected...");
                return;
            }

            if (!(oDoc.SelectSet[1] is Edge))
            {
                System.Windows.Forms.MessageBox.Show("Not an edge...");
                return;
            }
            try
            {
                Edge oEdge = oDoc.SelectSet[1];
                //create a new custom set
                AttributeSet oAttSet = oEdge.AttributeSets.Add("CustomSet");
                //Create Attribute (Notice the syntax "Inventor.Attribute" is REQUIRED)
                Inventor.Attribute oAttribute;
                oAttribute = oAttSet.Add("Insert", ValueTypeEnum.kStringType, "Insert edge");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Attribute or Set already exists...");
                return;
            }
        }
    }
}

//    public class Macros
//    {
//        public static Inventor.Application _invApp = null;

//        public Macros(Inventor.Application oApp)
//        {
//            _invApp = oApp;
//        }

//        public void AssemblyTraversal()
//        {
//            AssemblyDocument oDoc = (AssemblyDocument)_invApp.ActiveDocument;
//            Call the recursive function to iterate through the assembly tree
//            AssemblyTraversalRec((ComponentOccurrences)(ComponentOccurrence)oDoc.ComponentDefinition.Occurrences, 0);

//        }

//        private void AssemblyTraversalRec(ComponentOccurrences inCollection, long level)
//        {
//            ComponentOccurrence oCompOccurence;
//            foreach (ComponentOccurrence oCompOccurence in inCollection)
//            {
//                if (oCompOccurence.DefinitionDocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
//                {
//                    Debug.Print(Strings.Space((int)(3L * level)) + " - [Asm]  " + oCompOccurrence.Name);
//                }
//                else
//                {
//                    Debug.Print(Strings.Space((int)(3L * level)) + " - [Part] " + oCompOccurrence.Name);
//                }
//                AssemblyTraversalRec((ComponentOccurrences)oCompOccurence.SubOccurrences, level + 1);
//            }
//        }
//        public void AddFromFile()
//        {
//            AssemblyDocument oDoc = (AssemblyDocument)_invApp.ActiveDocument;
//            Matrix oMatrix = _invApp.TransientGeometry.CreateMatrix();
//            ComponentOccurrence oOcc = oDoc.ComponentDefinition.Occurrences.Add(@"C:\Temp\Part1.ipt", oMatrix);

//        }

//        public void MoveOccurence()
//        {
//            AssemblyDocument oAsm = (AssemblyDocument)_invApp.ActiveDocument;
//            TransientGeometry oTG = _invApp.TransientGeometry;
//            ComponentOccurrence oOcc1 = oAsm.ComponentDefinition.Occurrences[1];

//            if (oOcc1.Grounded)
//            {
//                oOcc1.Grounded = false;
//            }
//            Matrix oNewMatrix = oOcc1.Transformation;
//            oNewMatrix.SetTranslation(oTG.CreateVector(15, 5, 5), false);
//            oOcc1.Transformation = oNewMatrix;
//            oAsm.Update();


//        }

//        private class Timer
//        {
//            DateTime _previous;
//            public Timer()
//            {
//                _previous = DateTime.Now;

//            }
//            public double GetElapsedSeconds()
//            {
//                DateTime Now = DateTime.Now;
//                TimeSpan Elasped = Now.Subtract(_previous);
//                _previous = Now;
//                return Elasped.TotalSeconds;


//            }
//        }

//        private void RotateOccurrence(ComponentOccurrence oOccurence, double oAngleDeg)
//        {
//            TransientGeometry oTG = _invApp.TransientGeometry;
//            Matrix oRotMatrix = oTG.CreateMatrix();
//            oRotMatrix.SetToRotation(oAngleDeg * (System.Math.PI / 180), oTG.CreateVector(0, 0, 1), (Inventor.Point)oTG.CreateVector(0, 0, 0));
//            Matrix oNewMatrix = oOccurence.Transformation;
//            oNewMatrix.PreMultiplyBy(oRotMatrix);
//            oOccurence.Transformation = oNewMatrix;
//        }

//        public void RotateOccurrenceTest()
//        {
//            AssemblyDocument oAsm = (AssemblyDocument)_invApp.ActiveDocument;
//            ComponentOccurrence oOcc1 = oAsm.ComponentDefinition.Occurrences[1];

//            Timer timer = new Timer();
//            Double dt = 0;
//            while (dt < 10)
//            {
//                RotateOccurrence(oOcc1, 5);
//                System.Windows.Forms.Application.DoEvents();
//                dt = dt + timer.GetElapsedSeconds();
//            }
//        }

//        public void AttachAttribute()
//        {
//            Document oDoc = _invApp.ActiveDocument;
//            if (oDoc.SelectSet.Count != 1)
//            {
//                System.Windows.Forms.MessageBox.Show("A single Edge must be selected...");
//                return;
//            }

//            if (!(oDoc.SelectSet[1] is Edge))
//            {
//                System.Windows.Forms.MessageBox.Show("Not an edge...");
//                return;
//            }
//            try
//            {
//                Edge oEdge = oDoc.SelectSet[1];
//                create a new custom set
//                AttributeSet oAttSet = oEdge.AttributeSets.Add("CustomSet");
//                Create Attribute(Notice the syntax "Inventor.Attribute" is REQUIRED)
//                Inventor.Attribute oAttribute;
//                oAttribute = oAttSet.Add("Insert", ValueTypeEnum.kStringType, "Insert edge");
//            }
//            catch
//            {
//                System.Windows.Forms.MessageBox.Show("Attribute or Set already exists...");
//                return;
//            }
//        }
//    }
//}
