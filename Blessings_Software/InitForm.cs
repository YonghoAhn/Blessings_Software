using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blessings_Software
{
    public partial class InitForm : Form
    {
        string version = Application.ProductVersion;
        string appname = Application.ProductName;
        string company = Application.CompanyName;
        string startup = Application.StartupPath;
        string culturename = Application.CurrentCulture.NativeName;
        public InitForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
