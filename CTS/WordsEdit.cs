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
    public partial class WordsEdit : Form
    {
        public WordsEdit()
        {
            InitializeComponent();

            //foreach (string s in ConstantsWords.Collection.Keys)
            //{
            //    listBox1.Items.Add(s);
            //}
            
            //foreach (Rule rule in Rules.RuleCollection)
            //{
            //    comboBox1.Items.Add($"[{rule.RuleID}] - {rule.VoiceID}");
            //}

            comboBox1.SelectedIndex = 0;
        }

        //private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    comboBox1.SelectedIndex = 0;
        //    textBox1.Text = ConstantsWords.Collection[(string)listBox1.SelectedItem].Contains((string)comboBox1.SelectedItem) ? ConstantsWords.Collection[(string)listBox1.SelectedItem][(string)comboBox1.SelectedItem] : "";

        //    switch ((string)listBox1.SelectedItem)
        //    {
        //        //TODO: придумать описания
        //        case "%say":
        //        {
        //            richTextBox1.Text = "";
        //                return;
        //        }
                    
        //    }
            
        //}
    }
}
