using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using BroChatCatch.WebSocketProcessor;
using SpeechLib;

namespace CTS
{
    public class ChatListenner
    {
        BroChatListenner Listenner;

        public ChatListenner()
        {
            Listenner = new BroChatListenner();

            Listenner.OnMessage += ListennerOnMessage;
        }
        
        private void ListennerOnMessage(BCMessage msg)
        {
            UsersController.Control.Add(msg.Name);

            if (msg.Name.ToLower() == msg.Service.ToLower())
            {
                LogController.Add($"[{msg.Service}] [SysMsg] {msg.Name} : {msg.Text}");
                return;
            }

            if (UsersController.Control.IsInored(msg.Name))
            {
                LogController.Add($"[{msg.Service}] [IGONORED] {msg.Name} : {msg.Text}");
                return;
            }

            Rule r = Rules.RuleCollection.FirstOrDefault(x => x.CanReadThis(msg.Text.ToLower()));
            if (r.VoiceID == "null")
            {
                LogController.Add($"[{msg.Service}] [NULL VOICE] {msg.Name} : {msg.Text}");
                return;
            }
            
            if (msg.Text[0] == '+')
            {
                string[] strs = msg.Text.Split(new[] {' '}, 2, StringSplitOptions.RemoveEmptyEntries);

                if (strs.Length == 1)return;

                switch (strs[0])
                {
                    case "+s":
                        {
                            ReplaceFilter rpls = ReplaseFilter.Collection.Where(x => x.Z).FirstOrDefault(x => x.Key == msg.Name);

                            if (rpls == null)
                            {
                                rpls = new ReplaceFilter(msg.Name, true);
                                ReplaseFilter.Collection.Add(rpls);
                            }
                            if (rpls.Contains(r.RuleID)) rpls.Remove(r.RuleID);

                            string txt = strs[1];
                            List<string> tsmiles = ATextClear(ref txt);

                            LogController.Add($"[{msg.Service}] {msg.Name} -> {txt}");

                            ReplaseFilter.FilterThisShit(ref txt, r.RuleID);
                            BTextClear(tsmiles, ref txt);

                            if (!TextCheck(txt, 15, 15))
                            {
                                LogController.Add($"[{msg.Service}] [FILTERED] {msg.Name} : {txt}");
                                return;
                            }

                            rpls.Content.Add(new FilterUnit(r.RuleID, txt));
                            
                            Form1.Speak(r.VoiceID, ConstantsWords.Get("%rename",r.RuleID).Replace("%1", msg.Name).Replace("%2", txt));



                            return;
                        }
                    //case "+n":
                    //{
                    //        Form1.voice.Speak("", SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
                    //        return;
                    //}
                }
            }


            string text = msg.Text;

            List<string> smiles = ATextClear(ref text);

            LogController.Add($"[{msg.Service}] {msg.Name} : {text}");

            ReplaseFilter.FilterThisShit(ref text, r.RuleID);
            BTextClear(smiles,ref text);

            if (!TextCheck(text, 200, 15))
            {
                LogController.Add($"[{msg.Service}] [FILTERED] {msg.Name} : {text}");
                return;
            }

            string name = msg.Name;
            ReplaseFilter.FilterThisShit(ref name, r.RuleID, true);

            Form1.Speak(r.VoiceID, $"{name} {ConstantsWords.Get("%say",r.RuleID)} {text}");
        }

        private bool TextCheck(string text, int allsize, int partialsize)
        {
            if (text.Replace(" ", "").Length < 1) return false;

            if (text.Length >= allsize) return false;

            string[] strs = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Any(s => s.Length > partialsize))
                return false;

            return true;
        }

        private List<string> ATextClear(ref string text)
        {
            List<string> list = new List<string>();

            Regex r = new Regex("<a.*?a>");
            foreach (Match match in r.Matches(text))
            {
                text = text.Replace(match.Value, "");
            }
            r = new Regex("<img class = \"smile\" alt=\"(.*?)\" src.*?/img>");
            foreach (Match match in r.Matches(text))
            {
                if(!list.Contains(match.Groups[1].Value))
                    list.Add(match.Groups[1].Value);
                text = text.Replace(match.Groups[0].Value,match.Groups[1].Value);
            }

            return list;
        }

        private void BTextClear(List<string> list, ref string text)
        {
            text = list.Aggregate(text, (current, s) => current.Replace(s, ""));
        }
    }
}
