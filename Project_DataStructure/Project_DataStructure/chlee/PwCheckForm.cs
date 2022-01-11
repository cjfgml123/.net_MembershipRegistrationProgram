using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DataStructure.chlee
{   
    
    public partial class PwCheckForm : Form
    {   
        public string _result { get; set;}

        public PwCheckForm()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            _result = txt_oldPw.Text;
            Close();
        }
    }
}
