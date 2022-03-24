using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarendonSkiTrip
{
    class User
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool Operator { get; private set; } = false;
        public int HighScore { get; private set; } = 0;

        string[] Values { get; }

        public User(string[] _values)
        {
            int numberToSet;

            Username = _values[(int)Program.UserFormatEnum.username];
            Password = _values[(int)Program.UserFormatEnum.password];
            if (int.TryParse(_values[(int)Program.UserFormatEnum.op], out numberToSet)) Operator = Convert.ToBoolean(numberToSet);
            if (int.TryParse(_values[(int)Program.UserFormatEnum.highscore], out numberToSet)) HighScore = numberToSet;
            Values = _values;
        }

        public int FindCurrentUserIndex(string _path)
        {
            using (StreamReader sr = new StreamReader(_path))
            {
                string[] users = sr.ReadToEnd().Split("\n");

                for (var i = 0; i < users.Length; i++)
                {
                    string[] userInfo = users[i].Split(",");

                    if (userInfo[(int)Program.UserFormatEnum.username] == Username)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public static int FindUserIndex(string _username, string _path)
        {
            using (StreamReader sr = new StreamReader(_path))
            {
                string[] users = sr.ReadToEnd().Split("\n");

                for (var i = 0; i < users.Length; i++)
                {
                    string[] userInfo = users[i].Split(",");

                    if (userInfo[(int)Program.UserFormatEnum.username] == _username)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}
