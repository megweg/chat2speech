using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotSpeaker;
using SpeechLib;

namespace CTS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Rules.Load();
            ReplaseFilter.Load();
            ConstantsWords.Load();
            UsersController.Control.Load();

            CheckForIllegalCrossThreadCalls = false;

            foreach (SpObjectToken token in voice.GetVoices("", ""))
            {
                comboBox1.Items.Add(token.GetDescription(0));
            }
            comboBox1.SelectedIndex = 0;

            ChatListenner c = new ChatListenner();

            UsersController.Control.OnChange += () =>
            {
                listBox1.Items.Clear();

                listBox1.BeginUpdate();
                foreach (string s in UsersController.Control.Users)
                {
                    listBox1.Items.Add((UsersController.Control.IsInored(s) ? "[i] " : "") + s);
                }
                listBox1.EndUpdate();
            };
        }


        public static void Speak(string v, string text)
        {
            foreach (SpObjectToken token in voice.GetVoices("", ""))
            {
                if (v == token.GetDescription(0))
                {
                    voice.Voice = token;
                    break;
                }
            }
            voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }

        public static SpVoice voice = new SpVoice();

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (SpObjectToken token in voice.GetVoices("", ""))
            {
                if ((string)comboBox1.SelectedItem == token.GetDescription(0))
                {
                    voice.Voice = token;
                    break;
                }
            }
            voice.Speak(richTextBox1.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (SpObjectToken token in voice.GetVoices("", ""))
            {
                if ((string)comboBox1.SelectedItem == token.GetDescription(0))
                {
                    voice.Voice = token;
                    break;
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listBox1.SelectedIndex==-1) return;

            ReplaceFilter tmp =
                ReplaseFilter.Collection.Where(x => x.Z).FirstOrDefault(x => x.Key == (string) listBox1.SelectedItem);
            if (tmp == null)
            {
                tmp = new ReplaceFilter((string)listBox1.SelectedItem, true);
                ReplaseFilter.Collection.Add(tmp);
            }

            FilterEdit o = new FilterEdit(tmp);
            o.ShowDialog();
        }
        
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            if (e.Button == MouseButtons.Right) UsersController.Control.IgnoreToggle((string)listBox1.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            voice.Speak("", SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Rules.Save();
            ReplaseFilter.Save();
            ConstantsWords.Save();
            UsersController.Control.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VoiceCorretions vc = new VoiceCorretions();
            vc.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WordsEdit we = new WordsEdit();
            we.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FilterManager fm= new FilterManager();
            fm.ShowDialog();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            voice.Rate = trackBar1.Value - 10;
            label1.Text = $"Speed rate {trackBar1.Value - 10}";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Log log = new Log();
            log.ShowDialog();
        }
    }
}
