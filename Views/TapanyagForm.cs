using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tapanyagook.Models;
using Tapanyagook.Presenters;
using Tapanyagook.ViewInterfaces;

namespace Tapanyagook.Views
{
    public partial class TapanyagForm : Form
    {
        internal tapanyag to;
        private int id;
        private TablazatPresenter tp;
        public TapanyagForm()
        {
            InitializeComponent();
        }

        public tapanyag tapanyag
        {
            get
            {
                var energia = numericUpDown4.Value;
                var nev = textBox1.Text;
                var feherje = numericUpDown1.Value;
                var zsir = numericUpDown2.Value;
                var szenhidrat = numericUpDown3.Value;
                var asd = new tapanyag(nev, energia, feherje, zsir, szenhidrat);
                return asd;
            }
            set
            {

            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
  
        }
    }
}
