using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTS
{
    public partial class FilterEdit : Form
    {
        public FilterEdit(ReplaceFilter unit)
        {
            InitializeComponent();

            filter = unit;

            textBox1.Text = unit.Key;

            foreach (Rule rule in Rules.RuleCollection)
            {
                comboBox1.Items.Add(rule.RuleID);
            }
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
        }

        ReplaceFilter filter;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = filter.Contains((string) comboBox1.SelectedItem) ? filter[(string) comboBox1.SelectedItem] : "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReplaseFilter.Collection.Remove(filter);
            FilterManager.Reupdate = true;

            Close();
        }
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            filter[(string) comboBox1.SelectedItem] = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filter.Key = textBox1.Text.Trim();
            FilterManager.Reupdate = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Speak(Rules.RuleCollection.FirstOrDefault(x=> x.RuleID == (string)comboBox1.SelectedItem).VoiceID, textBox2.Text);
        }

        private void FilterEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("");
                e.Cancel = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
