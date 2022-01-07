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

namespace InventorBasics
{
    public partial class Form1 : Form
    {   
        public static Inventor.Application m_invApp=null; //declare a variable of type inventor
        public Form1()
        {
            InitializeComponent();
            try
            {
                try
                {
                    m_invApp = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");

                }
                catch
                {
                    Type InvType = System.Type.GetTypeFromProgID("Inventor.Application");
                    //remember to explicitly cast from type to application
                    m_invApp = (Inventor.Application)System.Activator.CreateInstance(InvType);
                    //set visibility to true
                    m_invApp.Visible = true;
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error! Could not create an instance");
            }

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Inventor.View oView = m_invApp.ActiveView;
            if (oView != null)
            {
                if(tbx1.Text.Length > 0 && tbx2.Text.Length>0)
                {
                    oView.Width = System.Int16.Parse(tbx1.Text);
                    oView.Height= System.Int16.Parse(tbx2.Text);
                }
            }

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if(tbx3.Text.Length>0)
            {
                m_invApp.Caption=tbx3.Text;
            }
        }
    }
}
