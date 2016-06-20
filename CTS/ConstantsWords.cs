using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTS
{
    public static class ConstantsWords
    {
        public static List<ReplaceFilter> Collection;


        public static bool Contains(string rule)
        {
            return Collection.FirstOrDefault(x => x.Key == rule) != null;
        }

        public static string Get(string key, string rule)
        {
            if (Contains(key))
            {
                return Collection.FirstOrDefault(x => x.Key == key)[rule];
            }
            return "";
        }

        private static void Init()
        {
            Collection = new List<ReplaceFilter>();

            ReplaceFilter say = new ReplaceFilter("%say");
            say.Content.Add(new FilterUnit("Default", " say, "));
            say.Content.Add(new FilterUnit("Russian", " говорит, "));

            //ReplaceFilter me = new ReplaceFilter("%me");
            //me.Content.Add("Default", " dreaming, ");
            //me.Content.Add("Russian", " думает, ");
            
            ReplaceFilter rename = new ReplaceFilter("%rename");
            rename.Content.Add(new FilterUnit("Default", "%1 now sound as %2"));
            rename.Content.Add(new FilterUnit("Russian", "%1 теперь звучит как %2"));

            Collection.Add(say);
            //Collection.Add(me.Key, me);
            Collection.Add(rename);
        }
        
        private static string FileName = "consts";

        public static void Load()
        {
            Collection = DataLoad.Load<List<ReplaceFilter>>(FileName);
            if(Collection.Count==0)
                Init();
        }

        public static void Save()
        {
            DataLoad.Save(Collection,FileName);
        }
    }
}
