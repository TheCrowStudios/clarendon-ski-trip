using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace ClarendonSkiTrip
{
    class FileEdit
    {
        public static void WriteArray(string[] _array, string _path)
        {
            using (StreamWriter sw = new StreamWriter(_path))
            {
                for (var i = 0; i < _array.Length; i++)
                {
                    sw.WriteLine(_array[i]);
                }
            }
        }

        public static void EditUser(User _user, Program.UserFormatEnum _index, string _value, string _path)
        {
            int userIndex = User.FindUserIndex(_user.Username, _path);

            using (StreamReader sr = new StreamReader(Program.PathUsers))
            {
                string[] users = sr.ReadToEnd().Split("\n");

                string[] currentUserInfo = users[userIndex].Split(",");

                currentUserInfo[(int)_index] = _value;

                string userConcat = "";

                for (var i = 0; i < currentUserInfo.Length; i++)
                {
                    if (i < currentUserInfo.Length - 1) userConcat += $"{currentUserInfo[i]},";
                    else userConcat += $"{currentUserInfo[i]}";
                }

                users[userIndex] = userConcat;

                sr.Close();

                FileEdit.WriteArray(users, Program.PathUsers);
            }
        }

        public static void EditUser(string _username, Program.UserFormatEnum _index, string _value, string _path)
        {
            int userIndex = User.FindUserIndex(_username, _path);

            using (StreamReader sr = new StreamReader(Program.PathUsers))
            {
                string[] users = sr.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);

                string[] currentUserInfo = users[userIndex].Split(",");

                currentUserInfo[(int)_index] = _value;
                currentUserInfo[(int)Program.UserFormatEnum.dateEdited] = DateTime.Now.ToString();

                string userConcat = "";

                for (var i = 0; i < currentUserInfo.Length; i++)
                {
                    if (i < currentUserInfo.Length - 1) userConcat += $"{currentUserInfo[i]},";
                    else userConcat += $"{currentUserInfo[i]}";
                }

                users[userIndex] = userConcat;

                sr.Close();

                FileEdit.WriteArray(users, Program.PathUsers);
            }
        }

        public static void SetUserGroups(string _path)
        {
            string[] users;

            using (StreamReader sr = new StreamReader(_path))
            {
                users = sr.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
            }

            for (var i = 0; i < users.Length; i++)
            {
                string[] user = users[i].Split(",");

                int averageTime = (Convert.ToInt16(user[(int)Program.UserFormatEnum.time]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time2]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time3]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time4])) / 4;

                char group;

                if (averageTime <= 20) group = 'A';
                else if (averageTime > 20 && averageTime <= 30) group = 'B';
                else group = 'C';

                FileEdit.EditUser(user[(int)Program.UserFormatEnum.username], Program.UserFormatEnum.group, Convert.ToString(group), _path);
            }
        }

        public static string Sha256Hash(string _text)
        {
            byte[] hash;

            using (SHA256 sha256 = SHA256.Create())
            {
                hash = sha256.ComputeHash(UTF8Encoding.UTF8.GetBytes(_text));
            }

            StringBuilder stringBuilder = new StringBuilder();

            for (var i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
