using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTS
{
    public static class Rules
    {
        public static List<Rule> RuleCollection = new List<Rule>();

        private static void Init()
        {
            RuleCollection.Add(new Rule("Default", "Default", "null", RuleType.ByDefault));
            RuleCollection.Add(new Rule("йцукенгшщзхъфывапролджэячсмитьбю", "Russian", "null", RuleType.ByLanguage));
            Resort();
        }

        private static string FileName = "rules";

        public static void Resort()
        {
            RuleCollection = RuleCollection.OrderBy(x => x.Type).ToList();
        }

        public static void Load()
        {
            RuleCollection = DataLoad.Load<List<Rule>>(FileName);

            if(RuleCollection.Count==0)
                Init();
        }

        public static void Save()
        {
            DataLoad.Save(RuleCollection,FileName);
        }
    }

    public enum RuleType
    {
        ByDefault=99,
        ByLanguage=0,
        ByName=1
    }

    public class Rule
    {
        public Rule()
        {
            
        }
        public string Content;
        public string RuleID;

        public string VoiceID;

        public RuleType Type;

        public Rule(string content, string ruleId, string voiceId, RuleType type = RuleType.ByLanguage)
        {
            Content = content;
            RuleID = ruleId;
            VoiceID = voiceId;
            Type = type;
        }

        public bool CanReadThis(string text)
        {
            if (Type == RuleType.ByDefault) return true;

            switch (Type)
            {
                case RuleType.ByLanguage:
                {
                    return Content.Any(c => text.Contains(c));
                }
            }

            return false;
        }
    }
}
