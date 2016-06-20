using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTS
{
    public static class ReplaseFilter
    {
        public static List<ReplaceFilter> Collection;

        private static string FileName = "filters";
        
        public static void Resort()
        {
            Collection = Collection.OrderBy(x => !x.Z).ToList();
        }

        public static void Load()
        {
            Collection = DataLoad.Load<List<ReplaceFilter>>(FileName);
            
            Resort();
        }

        public static void Save()
        {
            DataLoad.Save(Collection,FileName);
        }

        public static void FilterThisShit(ref string text, string rule, bool z = false)
        {
            foreach (ReplaceFilter filter in z? Collection.Where(zz=> zz.Z) : Collection)
            {
                filter.Get(rule, ref text);
            }
        }
    }

    public class FilterUnit
    {
        public FilterUnit(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public FilterUnit()
        {
            
        }
        public string Key;
        public string Value;
    }

    public class ReplaceFilter
    {
        public List<FilterUnit> Content;
        public string Key;

        public bool Z;

        public ReplaceFilter()
        {
            
        }

        public ReplaceFilter(string key, bool z=false)
        {
            Content = new List<FilterUnit>();
            Key = key;
            Z = z;
        }

        public bool Contains(string rule)
        {
            return Content.FirstOrDefault(x => x.Key == rule) != null;
        }
        public void Remove(string rule)
        {
            if (Contains(rule))
                Content.Remove(Content.FirstOrDefault(x => x.Key == rule));
        }

        public string this[string index]
        {
            get
            {
                if (Contains(index))
                    return Content.FirstOrDefault(x => x.Key == index).Value;
                else return "";
            }
            set
            {
                if(Contains(index))
                Content.FirstOrDefault(x => x.Key == index).Value = value;
                else Content.Add(new FilterUnit(index,value));
            }
        }

        public void Get(string rule, ref string text)
        {
            FilterUnit fu = Content.FirstOrDefault(x => x.Key == rule);
            if (fu!=null)
            {
                if (fu.Value.Replace(" ", "") != "")
                    text = text.Replace(Key, fu.Value);
            }
        }

        public string Get(string rule)
        {
            FilterUnit fu = Content.FirstOrDefault(x => x.Key == rule);
            if (fu != null)
            {
                if (fu.Value.Replace(" ", "") != "")
                    return fu.Value;
            }

            return "";
        }

        public override string ToString()
        {
            return Key;
        }

        public static void Swap(ReplaceFilter pi, ReplaceFilter pj)
        {
            dynamic tmp = pi.Content;
            pi.Content = pj.Content;
            pj.Content = tmp;

            tmp = pi.Key;
            pi.Key = pj.Key;
            pj.Key = tmp;
        }
    }
}
