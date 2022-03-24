using System;
using System.IO;
using System.Text;

namespace ClarendonSkiTrip
{
    class Program
    {
        public static readonly string PathData = $"{Directory.GetCurrentDirectory()}/data";
        public static readonly string PathUsers = $"{Directory.GetCurrentDirectory()}/data/users.csv";

        public static readonly string UserFormat = "username,password,name,lastName,gender,operator,highscore,quizHighscore,time,time2,time3,time4,group,dateAdded,dateEdited";
        public enum UserFormatEnum
        {
            username,
            password,
            name,
            lastName,
            gender,
            op,
            highscore,
            quizHighscore,
            time,
            time2,
            time3,
            time4,
            group,
            dateAdded,
            dateEdited
        }

        public static User CurrentUser { get; set; }

        public static readonly Menu[] Menus = { new Menu(MenuIDs.main),
            new Menu(MenuIDs.login),
            new Menu(MenuIDs.leaderboard),
            new Menu(MenuIDs.dashboard),
            new Menu(MenuIDs.quiz),
            new Menu(MenuIDs.information),
            new Menu(MenuIDs.admin)
        };

        public enum MenuIDs
        {
            main,
            login,
            leaderboard,
            dashboard,
            quiz,
            information,
            admin
        }

        public static User[] Users;

        public static Menu CurrentMenu { get; private set; }

        public static Menu PreviousMenu { get; private set; }

        static void Main(string[] args)
        {
            MenuMain.Menus = new Menu[]{ Menus[(int)MenuIDs.login],
                Menus[(int)MenuIDs.leaderboard]};

            MenuDashboard.Menus = new Menu[]{ Menus[(int)MenuIDs.leaderboard],
                Menus[(int)MenuIDs.quiz],
                Menus[(int)MenuIDs.information],
                Menus[(int)MenuIDs.main]};

            MenuDashboard.AdminMenus = new Menu[] { Menus[(int)MenuIDs.admin] };

            Console.WriteLine("Hello World!");

            //menus = { new Menu(new MenuLogin())};

            CurrentMenu = Menus[(int)MenuIDs.main];

            //CurrentMenu = new Menu([MenuLogin]);

            CheckFiles();

            while (true)
            {
                try
                {
                    //if (CurrentUser == null) CurrentMenu = Menus[(int)MenuIDs.main];
                    Menu.Title(CurrentMenu.MenuName);
                    CurrentMenu.Run();
                } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Input.End();
                    Logout();
                }
                //Input.End();
            }
        }

        public static void ChangeMenu(Menu _menu)
        {
            PreviousMenu = CurrentMenu;
            CurrentMenu = _menu;
        }

        public static void Back()
        {
            CurrentMenu = PreviousMenu;
        }

        public static void Logout()
        {
            CurrentUser = null;
            CurrentMenu = Menus[(int)MenuIDs.main];
        }

        public static Array BubbleSort(Array _values)
        {
            bool edited = true;
            Array valuesSorted = _values;

            while (edited)
            {
                edited = false;

                for (var i = 0; i < valuesSorted.Length; i++)
                {
                    object firstValue = valuesSorted.GetValue(i);

                    if (i != valuesSorted.Length) {
                        if (Convert.ToInt32(firstValue) < Convert.ToInt32(valuesSorted.GetValue(i+1))) {
                            valuesSorted.SetValue(valuesSorted.GetValue(i + 1), i);
                            valuesSorted.SetValue(firstValue, i+1);

                            edited = true;
                        }
                    }
                }
            }

            return valuesSorted;
        }

        public static Array BubbleSort(Array _values, int _index)
        {
            bool edited = true;
            Array valuesSorted = _values;

            while (edited)
            {
                edited = false;

                for (var i = 0; i < valuesSorted.GetLength(0)-1; i++)
                {
                    int[] firstIndex = new int[] { i, _index };
                    int[] secondIndex = new int[] { i + 1, _index };

                    object firstValue = valuesSorted.GetValue(firstIndex);

                    //Console.WriteLine($"{i} / {valuesSorted.GetLength(0) - 1}");
                    if (Convert.ToInt32(valuesSorted.GetValue(firstIndex)) < Convert.ToInt32(valuesSorted.GetValue(secondIndex)))
                    {
                        for (var j = 0; j < valuesSorted.GetLength(1); j++)
                        {
                            object firstCurrentValue = valuesSorted.GetValue(i, j);
                            object secondCurrentValue = valuesSorted.GetValue(i+1, j);

                            valuesSorted.SetValue(secondCurrentValue, new int[] { i, j });
                            valuesSorted.SetValue(firstCurrentValue, new int[] { i+1, j });
                        }

                        edited = true;
                    }
                }
            }

            return valuesSorted;
        }

        static void CheckFiles()
        {
            //Directory.Delete(PathData);
            if (!Directory.Exists(PathData))
            {
                string path = Directory.CreateDirectory(PathData).Name;
                Console.WriteLine($"Data path created at {path}");
            }

            if (!File.Exists(PathUsers)) using (FileStream fs = File.Create(PathUsers)) {
                    //fs.Write(UTF8Encoding.UTF8.GetBytes(UserFormat));
            } /*else
            {
                using (StreamReader sr = new StreamReader(PathUsers)) {
                    string[] allLines = sr.ReadToEnd().Split("\n");
                    sr.Close();
                    if (allLines[0] == null)
                    {
                        using (StreamWriter sw = new StreamWriter(PathUsers)) { sw.WriteLine(UserFormat); };
                        Console.WriteLine("Users file is empty");
                    }
                    else if (allLines[0] != UserFormat)
                    {
                        using (StreamWriter sw = new StreamWriter(PathUsers))
                        {
                            string[] newLines = { UserFormat };
                            Array.Resize(ref newLines, allLines.Length + 1);
                            allLines.CopyTo(newLines, 1);
                            Console.WriteLine("No csv format defined");
                        }
                    }
                }
            }*/

            Console.WriteLine("Paths created");

            string[] allLines;
            int emptyLines = 0;

            bool invalidEntries = false;

            using (StreamReader sr = new StreamReader(PathUsers))
            {
                allLines = sr.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
            }

                if (allLines.Length == 0)
                {
                    Console.WriteLine("There are no users in the file");
                    Console.WriteLine("Add an admin user");

                    MenuAdmin.AddUser(true);
                }

                Users = new User[allLines.Length];

                for (var i = 0; i < allLines.Length; i ++)
                {
                    if (String.IsNullOrWhiteSpace(allLines[i]) && i < allLines.Length - 1) {
                        emptyLines += 1;
                        invalidEntries = true;
                    }

                    string[] currentValues = allLines[i].Split(",");

                    if (currentValues.Length != UserFormat.Split(",").Length && i < allLines.Length - 1)
                    {
                        invalidEntries = true;
                        Console.WriteLine($"User at index {i} is an invalid entry");
                    }
                }

                /*if (invalidEntries)
                {
                    if (!Input.GetYesNo("Do you want to repair the users")) return;
                }*/

            /*for (var i = 0; i < newLines.Length; i++)
            {
                string[] currentValues = newLines[i].Split(",");

                if (currentValues.Length != UserFormat.Split(",").Length && currentValues.Length > 1)
                {
                    int valuesToAdd = UserFormat.Split(",").Length - currentValues.Length;

                    if (valuesToAdd > 0)
                    {
                        newLines[i].Replace("\n", "");

                        if (newLines[i].Substring(newLines[i].Length - 1) == ",") newLines[i] = newLines[i].Remove(newLines[i].Length - 1);

                        for (var j = 0; j < valuesToAdd; j++)
                        {
                            newLines[i] += ",0";
                            //FileEdit.EditUser(currentValues[i], (UserFormatEnum)currentValues.Length + j, "0", PathUsers);
                        }

                        newLines[i] += "\n";
                    }
                }

                
            }*/

            //FileEdit.WriteArray(newLines, PathUsers);

            Console.WriteLine("Users repaired");
        }

        //static bool Login(string )
    }
}
