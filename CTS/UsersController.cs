using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTS
{
    public class UsersController
    {
        public static UsersController Control = new UsersController();

        public List<string> Users = new List<string>();
        List<string> IgnoredUsers = new List<string>();

        public delegate void SomeChanged();

        public event SomeChanged OnChange;

        public void Add(string str)
        {
            if (!Users.Contains(str))
            {
                Users.Add(str);
                OnChange?.Invoke();
            }
        }

        private static string FileName = "ignore";

        public void Contains(string str) => Users.Add(str);

        public void IgnoreToggle(string str)
        {
            string tmp = str;

            if (str[0] == '[')
            {
                tmp = str.Replace("[i] ", "");
            }

            if (IgnoredUsers.Contains(tmp))
                IgnoredUsers.Remove(tmp);
            else IgnoredUsers.Add(tmp);

            OnChange?.Invoke();
        }

        public bool IsInored(string name) => IgnoredUsers.Contains(name);

        public void Load()
        {
            IgnoredUsers = DataLoad.Load<List<string>>(FileName);
        }
        public void Save()
        {
            DataLoad.Save(IgnoredUsers,FileName);
        }

    }
}
