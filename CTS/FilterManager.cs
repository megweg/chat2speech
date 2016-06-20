using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CTS
{
    public partial class FilterManager : Form
    {
        public FilterManager()
        {
            InitializeComponent();

            Reupdate = true;
            UpdList();
        }

        public static bool Reupdate = false;

        public void UpdList()
        {
            if (!Reupdate) return;


            listBox1.BeginUpdate();

            listBox1.Items.Clear();
            foreach (ReplaceFilter filter in ReplaseFilter.Collection.Where(x => !x.Z))
            {
                listBox1.Items.Add(filter);
            }
            listBox1.EndUpdate();

            Reupdate = false;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                FilterEdit o = new FilterEdit((ReplaceFilter)listBox1.SelectedItem);
                o.ShowDialog();
            }
            UpdList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender.Equals(button1))
            {
                if (listBox1.SelectedIndex <= 0) return;
            }
            else
            {
                if (listBox1.SelectedIndex == listBox1.Items.Count-1) return;
            }
            
            ReplaceFilter a1 = (ReplaceFilter)listBox1.SelectedItem;
            listBox1.SelectedIndex += sender.Equals(button1) ? -1 : 1;
            ReplaceFilter a2 = (ReplaceFilter)listBox1.SelectedItem;
            
            ReplaceFilter.Swap(a1,a2);

            Reupdate = true;
            UpdList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReplaceFilter tmp = new ReplaceFilter("");
            
            FilterEdit o = new FilterEdit(tmp);
            o.ShowDialog();

            ReplaseFilter.Collection.Add(tmp);

            Reupdate = true;

            UpdList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            ReplaseFilter.Collection.Remove((ReplaceFilter) listBox1.SelectedItem);

            Reupdate = true;
            UpdList();
        }
    }
}
