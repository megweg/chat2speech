using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpeechLib;

namespace CTS
{
    public partial class VoiceCorretions : Form
    {
        public VoiceCorretions()
        {
            InitializeComponent();

            comboBox2.Items.Add("null");
            foreach (SpObjectToken o in Form1.voice.GetVoices("",""))
            {
                comboBox2.Items.Add(o.GetDescription(0));
            }

            foreach (Rule rule in Rules.RuleCollection)
            {
                comboBox1.Items.Add(rule.RuleID);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Rules.RuleCollection.FirstOrDefault(x => x.RuleID == (string) comboBox1.SelectedItem).VoiceID =
                (string)comboBox2.SelectedItem;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedItem =
                Rules.RuleCollection.FirstOrDefault(x => x.RuleID == (string) comboBox1.SelectedItem).VoiceID;
        }
    }
}
