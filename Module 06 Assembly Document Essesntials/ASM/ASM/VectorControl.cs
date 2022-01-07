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
    public partial class VectorControl : UserControl
    {

        private Inventor.Application _invApp = null;
        public VectorControl()
        {
            InitializeComponent();
        }
        public Inventor.Application SetApp
        {

            set { _invApp = value; }
        }
        public string VectorName
        {

            get { return label1.Text; }

            set { label1.Text = value; }
        }
        public Vector Vector
        {
            get
            {
                double x = 0;
                double y = 0;
                double z = 0;

                if ((textBoxx.TextLength == 0))
                {
                    x = 0;
                }
                else
                {
                    x = System.Double.Parse(textBoxx.Text);
                }

                if ((textBoxy.TextLength == 0))
                {
                    y = 0;
                }
                else
                {
                    y = System.Double.Parse(textBoxy.Text);
                }

                if ((textBoxz.TextLength == 0))
                {
                    z = 0;
                }
                else
                {
                    z = System.Double.Parse(textBoxz.Text);
                }


                return _invApp.TransientGeometry.CreateVector(x, y, z);
            }
            set
            {

                textBoxx.Text = value.X.ToString("F2");
                textBoxy.Text = value.Y.ToString("F2");

                textBoxz.Text = value.Z.ToString("F2");
            }

        }
    }
}
