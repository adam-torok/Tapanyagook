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
    public partial class TablazatForm : Form, ITablazatView
    {
        TablazatPresenter tp;
        private int pageCount;
        private int sortIndex;
        public TablazatForm()
        {
            InitializeComponent();
            tp = new TablazatPresenter(this);
            init();
        }

        public void init()
        {
            pageNumber = 1;
            itemsPerPage = 25;
            sortBy = "Id";
            sortIndex = 0;
            ascending = true;
        }

      
        public BindingList<tapanyag> bindingList
        {
            get => (BindingList<tapanyag>)dataGridView1.DataSource;
            set => dataGridView1.DataSource = value;
        }

        public int pageNumber { get; set; }
        public int itemsPerPage { get; set; }
        public string sortBy { get; set; }
        public bool ascending { get; set; }
        public string search => toolStripTextBox1.Text;

        public int totalItems
        {
            set
            {
                pageCount = (value - 1) / itemsPerPage + 1;
                label1.Text = pageNumber.ToString() + "/" + pageCount.ToString();
            }
        }
        private void TablazatForm_Load(object sender, EventArgs e)
        {
            tp.LoadData();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tp.Save();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using(var edit = new TapanyagForm())
            {
                DialogResult dr = edit.ShowDialog(this);
                if(dr == DialogResult.OK)
                {
                    tp.Add(edit.to);
                    edit.Close();
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            deleteRow();
        }
        private void deleteRow()
        {
            while (dataGridView1.SelectedRows.Count > 0)
            {
                tp.Remove(dataGridView1.SelectedRows[0].Index);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            tp.LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pageNumber >= 2)
            {
                pageNumber--;
                tp.LoadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pageNumber != pageCount)
            {
                pageNumber++;
                tp.LoadData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pageNumber = pageCount;
            tp.LoadData();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            tp.LoadData();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sortIndex == e.ColumnIndex)
            {
                ascending = !ascending;
            }
            switch (e.ColumnIndex)
            {
                default:
                    sortBy = "Id";
                    break;
                case 1:
                    sortBy = "nev";
                    break;
                case 2:
                    sortBy = "energia";
                    break;
                case 3:
                    sortBy = "feherje";
                    break;
                case 4:
                    sortBy = "zsir";
                    break;
                case 5:
                    sortBy = "szenhidrat";
                    break;

            }

            sortIndex = e.ColumnIndex;
            tp.LoadData();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
