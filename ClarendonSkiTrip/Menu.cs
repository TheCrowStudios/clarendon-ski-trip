using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClarendonSkiTrip
{
    class Menu
    {

        //public static string[] Names = { "Main", "Login", "Leaderboard" };

        public Program.MenuIDs Name { get; }
        public string MenuName { get; }
        public char Id { get; private set; }
        string Description { get; }
        bool InputMenu { get; }
        public Menu[] Menus { get; set; }


        public Menu(Menu[] _menus)
        {
            for (var i = 0; i < _menus.Length; i++)
            {
                if (Description.Length > 0) Description += $" / {_menus[i].Name} ({_menus[i].Id})";
                if (Description.Length == 0) Description += $"{_menus[i].Name} ({_menus[i].Id})";


            }

            Menus = _menus;
        }

        public Menu(Program.MenuIDs _name)
        {
            switch (_name)
            {
                case Program.MenuIDs.login:
                    Name = _name;
                    MenuName = MenuLogin.Name;
                    Id = MenuLogin.Id;
                    InputMenu = MenuLogin.InputMenu;
                    break;

                case Program.MenuIDs.leaderboard:
                    Name = _name;
                    MenuName = MenuLeaderboard.Name;
                    Id = MenuLeaderboard.Id;
                    InputMenu = MenuLeaderboard.InputMenu;
                    break;

                case Program.MenuIDs.main:
                    Name = _name;
                    MenuName = MenuMain.Name;
                    Id = MenuMain.Id;
                    InputMenu = MenuMain.InputMenu;
                    Menus = MenuMain.Menus;
                    break;

                case Program.MenuIDs.dashboard:
                    Name = _name;
                    MenuName = MenuDashboard.Name;
                    Id = MenuDashboard.Id;
                    InputMenu = MenuDashboard.InputMenu;
                    Menus = MenuDashboard.Menus;

                    //CreateDescription(MenuMain.Menus);
                    break;

                case Program.MenuIDs.quiz:
                    Name = _name;
                    MenuName = MenuQuiz.Name;
                    Id = MenuQuiz.Id;
                    InputMenu = MenuQuiz.InputMenu;
                    break;

                case Program.MenuIDs.information:
                    Name = _name;
                    MenuName = MenuInformation.Name;
                    Id = MenuInformation.Id;
                    InputMenu = MenuInformation.InputMenu;
                    break;

                case Program.MenuIDs.admin:
                    Name = _name;
                    MenuName = MenuAdmin.Name;
                    Id = MenuAdmin.Id;
                    InputMenu = MenuAdmin.InputMenu;
                    break;
            }
        }

        public static string CreateDescription(Menu[] _menus)
        {
            string text = "";

            for (var i = 0; i < _menus.Length; i++)
            {
                if (text.Length > 0) text += $"\n({i + 1}) {_menus[i].Name}"; //text += $" / {_menus[i].Name} ({_menus[i].Id})";
                if (text.Length == 0) text += $"({i + 1}) {_menus[i].Name}"; //text += $"{_menus[i].Name} ({_menus[i].Id})";
            }

            return text;
        }

        public void Run()
        {
            switch (Name)
            {
                case Program.MenuIDs.login:
                    MenuLogin.Run();
                    break;

                case Program.MenuIDs.leaderboard:
                    MenuLeaderboard.Run();
                    break;

                case Program.MenuIDs.main:
                    MenuMain.Run();
                    break;

                case Program.MenuIDs.dashboard:
                    MenuDashboard.Run();
                    break;

                case Program.MenuIDs.quiz:
                    MenuQuiz.Run();
                    break;

                case Program.MenuIDs.information:
                    MenuInformation.Run();
                    break;

                case Program.MenuIDs.admin:
                    MenuAdmin.Run();
                    break;
            }
        }

        public static Menu GetMenuOption(Menu[] _menus)
        {
            int menuOption;

            do
            {
                menuOption = Input.GetInt("Menu");

                if (menuOption < 1 || menuOption > _menus.Length)
                {
                    Console.WriteLine("Menu does not exist!");
                }
                else
                {
                    return _menus[menuOption - 1];
                }

                /*for (var i = 0; i < _menus.Length; i++)
                {
                    if (menuOption == i + 1)
                    {
                        return _menus[i];
                    }
                }*/


                //menuOption = '\0';
            } while (menuOption < 1 || menuOption > _menus.Length);

            return _menus[0];
        }

        public static void Title(string _title)
        {
            Console.Clear();
            Console.Write($"Menu: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(_title);
            Console.ResetColor();
        }
    }

    class MenuDashboard
    {
        public static readonly Program.MenuIDs NameId = Program.MenuIDs.dashboard;
        public static readonly string Name = "Dashboard";
        public static readonly char Id = 'd';
        public static readonly bool InputMenu = false;
        public static string Text = "";

        public static Menu[] Menus { get; set; }
        public static Menu[] AdminMenus { get; set; }

        public static void Run()
        {
            if (Menus.Length > 0)
            {
                if (Program.CurrentUser.Operator && AdminMenus != null)
                {
                    AdminMenus.CopyTo(Menus, 0);
                }

                if (Text.Length == 0) Text = Menu.CreateDescription(Menus);

                Console.WriteLine(Text);
                Menu MenuOption = Menu.GetMenuOption(Menus);

                switch (MenuOption.Name)
                {
                    case Program.MenuIDs.main:
                        Program.Logout();
                        break;

                        /*case Program.MenuIDs.leaderboard:
                            Program.ChangeMenu(Program.Menus[(int)Program.MenuIDs.leaderboard]);
                            break;

                        case Program.MenuIDs.quiz:
                            Program.ChangeMenu(Program.Menus[(int)Program.MenuIDs.quiz]);
                            break;*/
                }

                Program.ChangeMenu(MenuOption);

                //Console.WriteLine("Menu selected");
                return;
            }

            Console.WriteLine($"Menu {Name} does not have any submenus");
            Program.Logout();
            Input.End();
        }
    }

    class MenuQuiz
    {
        public static readonly Program.MenuIDs NameId = Program.MenuIDs.quiz;
        public static readonly string Name = "Quiz";
        public static readonly char Id = 'q';
        public static readonly bool InputMenu = false;
        public static string Text = "";

        static Question[] questions =
            new Question[] {
                new Question("What are ski poles for",
                    new string[]{
                        "Getting extra grip on the snow"}, 0),
                new Question("What do you do when on the ski lifts",
                    new string[]{
                        "Pull down the bar",
                        "Don't sit on the edge",
                        "Do a barrel roll",
                        "Sit as far back"
                    }, 2),
                new Question("What is the number of casualties skiing",
                    new string[]
                    {
                        "54 a year",
                        "200 a year",
                        "349 a year",
                        "1048 a year",
                        "4752 a year"
                    }, 2)
            };

        public static void Run()
        {
            int score = 0;
            string correctAnswers = "";

            for (var i = 0; i < questions.Length; i++)
            {
                if (questions[i].Run() == true)
                {
                    if (i == questions.Length - 1) correctAnswers += $"{i + 1}";
                    else correctAnswers += $"{i + 1}, ";

                    score += 1;
                }
            }

            Console.WriteLine($"You got these questions correct {score} / {questions.Length}\n{correctAnswers}");

            if (Program.CurrentUser.HighScore < score)
            {
                FileEdit.EditUser(Program.CurrentUser, Program.UserFormatEnum.highscore, Convert.ToString(score), Program.PathUsers);
            }

            Input.End();
            Program.Back();
        }

        class Question
        {
            public string Text { get; private set; }
            public string[] Answers { get; private set; }
            public int CorrectAnswer { get; private set; }

            public Question(string _question, string[] _answers, int _correctAnswer)
            {
                if (_correctAnswer > _answers.Length)
                {
                    Console.WriteLine($"The correct answer is bigger than the amount of choices in question {_question}!");
                    Input.End();
                    Program.Logout();
                    return;
                }

                Text = _question;
                Answers = _answers;
                CorrectAnswer = _correctAnswer;
            }

            public bool Run()
            {
                Console.WriteLine(Text);

                for (var i = 0; i < Answers.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Answers[i]}");
                }

                int option = 0;

                while (true)
                {
                    option = Input.GetInt("Option");

                    if (option > Answers.Length || option < 1) Console.WriteLine($"Enter a number between 1 and {Answers.Length}");
                    else
                    {
                        if (option - 1 != CorrectAnswer) return false;
                        else return true;
                    }
                }
            }
        }
    }

    class MenuMain
    {
        public static readonly Program.MenuIDs NameId = Program.MenuIDs.main;
        public static readonly string Name = "Main";
        public static readonly char Id = 'm';
        public static readonly bool InputMenu = false;
        public static string Text = "";

        public static Menu[] Menus { get; set; }

        public static void Run()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("+============================+");
            Console.WriteLine("|                            |");
            Console.WriteLine("|     Clarendon Ski Trip     |");
            Console.WriteLine("|                            |");
            Console.WriteLine("+============================+");
            Console.ResetColor();

            if (Menus.Length > 0)
            {
                if (Text.Length == 0) Text = Menu.CreateDescription(Menus);

                Console.WriteLine(Text);
                Program.ChangeMenu(Menu.GetMenuOption(Menus));

                return;
            }

            Console.WriteLine("Menu does not have any submenus");
            Input.End();
        }
    }

    class MenuAdmin
    {
        public static Program.MenuIDs NameId = Program.MenuIDs.admin;
        public static string Name = "Admin";
        public static char Id = 'a';
        public static bool InputMenu = true;

        public static void Run()
        {
            int input;

            while (true)
            {
                Console.WriteLine("(1) Add student\n(2) Delete student\n(3) Edit student\n(4) Dashboard");
                input = Input.GetInt("Menu");

                switch (input)
                {
                    case 1:
                        AddUser();
                        break;

                    case 2:
                        DeleteUser();
                        break;

                    case 3:
                        EditUser();
                        break;

                    case 4:
                        Program.Back();
                        return;
                }
            }
        }

        public static void AddUser(bool forceAdmin = false)
        {
            do
            {
                string name;

                do
                {
                    name = Input.GetInput("Name");

                    if (String.IsNullOrWhiteSpace(name) || name.Contains(" ") || name.Contains(",") || name.Contains("\\")) Console.WriteLine("Name must not be empty or contain special characters");
                } while (String.IsNullOrWhiteSpace(name) || name.Contains(" ") || name.Contains(",") || name.Contains("\\"));

                string lastName;

                do
                {
                    lastName = Input.GetInput("Last name");

                    if (String.IsNullOrWhiteSpace(lastName) || lastName.Contains(" ") || lastName.Contains(",") || lastName.Contains("\\")) Console.WriteLine("Last name must not be empty or contain special characters");
                } while (String.IsNullOrWhiteSpace(lastName) || lastName.Contains(" ") || lastName.Contains(",") || lastName.Contains("\\"));

                char gender;

                do
                {
                    gender = Input.GetCharInput("Gender (M / F)");

                    if (gender != 'm' && gender != 'f' && gender != 'M' && gender != 'F') Console.WriteLine("Invalid gender");
                } while (gender != 'm' && gender != 'f' && gender != 'M' && gender != 'F');

                string password;
                string passwordConfirm;

                do
                {
                    password = Input.GetPrivateInput("Password");
                    passwordConfirm = Input.GetPrivateInput("Confirm password");

                    if (password != passwordConfirm)
                    {
                        Console.WriteLine("Passwords do not match");
                    }
                    else if (password.Length < 5)
                    {
                        Console.WriteLine("Password must be 5 characters or longer");
                    }
                } while (password != passwordConfirm | password.Length < 5);

                password = FileEdit.Sha256Hash(password);

                bool admin = forceAdmin;

                if (!forceAdmin)
                {
                    if (Input.GetYesNo("Should this user have admin privileges")) admin = true;
                }

                string username = name.Substring(0, 1) + lastName;

                int userId = 0;

                string write = "";

                for (var i = 0; i < Program.UserFormat.Split(",").Length; i++)
                {
                    if (i > 0) write += ",";

                    if (i == (int)Program.UserFormatEnum.username)
                    {
                        string[] users;

                        using (StreamReader sr = new StreamReader(Program.PathUsers))
                        {
                            users = sr.ReadToEnd().Split("\n");
                        }

                        for (var j = 0; j < users.Length; j++)
                        {
                            if (users[j].Split(",")[(int)Program.UserFormatEnum.username] == username + userId) userId += 1;
                        }

                        username += userId;

                        write += username;
                    }
                    else if (i == (int)Program.UserFormatEnum.password)
                    {
                        write += password;
                    }
                    else if (i == (int)Program.UserFormatEnum.name)
                    {
                        write += name;
                    }
                    else if (i == (int)Program.UserFormatEnum.lastName)
                    {
                        write += lastName;
                    }
                    else if (i == (int)Program.UserFormatEnum.gender)
                    {
                        write += gender;
                    }
                    else if (i == (int)Program.UserFormatEnum.op && admin)
                    {
                        write += "1";
                    }
                    else if (i == (int)Program.UserFormatEnum.dateAdded)
                    {
                        write += DateTime.Now.ToString();
                    }
                    else if (i == (int)Program.UserFormatEnum.dateEdited)
                    {
                        write += DateTime.Now.ToString();
                    }
                    else
                    {
                        write += "0";
                    }

                    if (i == Program.UserFormat.Split(",").Length - 1) write += "\n";
                }

                using (StreamWriter sw = new StreamWriter(Program.PathUsers, true))
                {
                    sw.Write(write);
                }

                Console.WriteLine($"Added user with username {username}");
                Input.End();
            } while (Input.GetYesNo("Do you want to add another user?"));
        }

        static void DeleteUser()
        {
            string username = Input.GetInput("Username");

            int userIndex = User.FindUserIndex(username, Program.PathUsers);

            if (userIndex == -1)
            {
                Console.WriteLine("Could not find user");
                return;
            }


            if (!Input.GetYesNo("Are you sure you want to delete this user")) return;

            using (StreamReader sr = new StreamReader(Program.PathUsers))
            {
                string[] users = sr.ReadToEnd().Split("\n");

                string[] usersNew = new string[users.Length - 1];

                for (var i = 0; i < usersNew.Length; i++)
                {
                    if (users[i].Split(",")[0] != username) usersNew[i] = users[i];
                }
            }
        }

        static void EditUser()
        {
            string username = Input.GetInput("Username");

            int userIndex = User.FindUserIndex(username, Program.PathUsers);

            if (userIndex == -1)
            {
                Console.WriteLine("Could not find user");
                return;
            }

            Console.WriteLine("Password (p) / Slope times (t)");

            char input = Input.GetCharInput("Value");

            switch (input)
            {
                case 'p':
                    string password = Input.GetPrivateInput("New password");
                    string passwordConfirm = Input.GetPrivateInput("Confirm password");

                    if (password == passwordConfirm) FileEdit.EditUser(username, Program.UserFormatEnum.password, FileEdit.Sha256Hash(password), Program.PathUsers);
                    break;

                case 't':
                    string users;

                    using (StreamReader sr = new StreamReader(Program.PathUsers))
                    {
                        users = sr.ReadToEnd();
                    }

                    string[] user = users.Split("\n")[userIndex].Split(",");

                    for (var i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"Current value for time {i}: {user[(int)Program.UserFormatEnum.time + i]}");
                        int time = Input.GetInt($"New time {i}");

                        switch (i)
                        {
                            case 0:
                                FileEdit.EditUser(username, Program.UserFormatEnum.time, Convert.ToString(time), Program.PathUsers);
                                break;

                            case 1:
                                FileEdit.EditUser(username, Program.UserFormatEnum.time2, Convert.ToString(time), Program.PathUsers);
                                break;

                            case 2:
                                FileEdit.EditUser(username, Program.UserFormatEnum.time3, Convert.ToString(time), Program.PathUsers);
                                break;

                            case 3:
                                FileEdit.EditUser(username, Program.UserFormatEnum.time4, Convert.ToString(time), Program.PathUsers);
                                break;
                        }
                    }

                    FileEdit.SetUserGroups(Program.PathUsers);
                    break;
            }
        }
    }

    class MenuInformation
    {
        public static Program.MenuIDs NameId = Program.MenuIDs.information;
        public static string Name = "Information";
        public static char Id = 'k';
        public static bool InputMenu = false;

        public static void Run()
        {
            FileEdit.SetUserGroups(Program.PathUsers);

            if (Program.CurrentUser.Operator)
            {
                string[] users;

                using (StreamReader sr = new StreamReader(Program.PathUsers))
                {
                    users = sr.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
                }

                string[] entries = { "Username     ", "Name                 ", "Gender", "Average slope time", "Group" };

                string head = $"| {entries[0]} | {entries[1]} | {entries[2]} | {entries[3]} | {entries[4]} |";

                string line = "";

                for (var i = 0; i < head.Length; i++)
                {
                    if (i == 0) line += "+";
                    else if (i == head.Length - 1) line += "+";
                    else line += "-";
                }

                Console.WriteLine(line);
                Console.WriteLine(head);
                Console.WriteLine(line);

                for (var i = 0; i < users.Length; i++)
                {
                    string[] user = users[i].Split(",");

                    int averageTime = (Convert.ToInt16(user[(int)Program.UserFormatEnum.time]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time2]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time3]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time4])) / 4;

                    string username = user[(int)Program.UserFormatEnum.username];

                    Console.Write($"| {username} ");

                    for (var j = 0; j < entries[0].Length - username.Length; j++)
                    {
                        Console.Write(" ");
                    }

                    string name = $"| {user[(int)Program.UserFormatEnum.name]} {user[(int)Program.UserFormatEnum.lastName]} ";

                    Console.Write(name);

                    for (var j = 0; j < entries[1].Length - name.Length + 3; j++)
                    {
                        Console.Write(" ");
                    }

                    Console.Write($"| {user[(int)Program.UserFormatEnum.gender]} ");

                    for (var j = 0; j < entries[2].Length - user[(int)Program.UserFormatEnum.gender].Length; j++)
                    {
                        Console.Write(" ");
                    }

                    Console.Write($"| {averageTime} ");

                    for (var j = 0; j < entries[3].Length - Convert.ToString(averageTime).Length; j++)
                    {
                        Console.Write(" ");
                    }

                    Console.Write($"| {user[(int)Program.UserFormatEnum.group]} ");

                    for (var j = 0; j < entries[4].Length - user[(int)Program.UserFormatEnum.group].Length; j++)
                    {
                        Console.Write(" ");
                    }

                    Console.WriteLine("|");
                }

                Console.WriteLine(line);
            }
            else
            {
                string[] user;

                int userIndex = User.FindUserIndex(Program.CurrentUser.Username, Program.PathUsers);

                using (StreamReader sr = new StreamReader(Program.PathUsers))
                {
                    user = sr.ReadToEnd().Split("\n")[userIndex].Split(",");
                }

                string[] entries = { "Username     ", "Name                 ", "Gender", "Average slope time", "Group" };

                string head = $"| {entries[0]} | {entries[1]} | {entries[2]} | {entries[3]} | {entries[4]} |";

                string line = "";

                for (var i = 0; i < head.Length; i++)
                {
                    if (i == 0) line += "+";
                    else if (i == head.Length - 1) line += "+";
                    else line += "-";
                }

                Console.WriteLine(line);
                Console.WriteLine(head);
                Console.WriteLine(line);

                int averageTime = (Convert.ToInt16(user[(int)Program.UserFormatEnum.time]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time2]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time3]) + Convert.ToInt16(user[(int)Program.UserFormatEnum.time4])) / 4;

                string username = user[(int)Program.UserFormatEnum.username];

                Console.Write($"| {username} ");

                for (var j = 0; j < entries[0].Length - username.Length; j++)
                {
                    Console.Write(" ");
                }

                string name = $"| {user[(int)Program.UserFormatEnum.name]} {user[(int)Program.UserFormatEnum.lastName]} ";

                Console.Write(name);

                for (var j = 0; j < entries[1].Length - name.Length + 3; j++)
                {
                    Console.Write(" ");
                }

                Console.Write($"| {user[(int)Program.UserFormatEnum.gender]} ");

                for (var j = 0; j < entries[2].Length - user[(int)Program.UserFormatEnum.gender].Length; j++)
                {
                    Console.Write(" ");
                }

                Console.Write($"| {averageTime} ");

                for (var j = 0; j < entries[3].Length - Convert.ToString(averageTime).Length; j++)
                {
                    Console.Write(" ");
                }

                Console.Write($"| {user[(int)Program.UserFormatEnum.group]} ");

                for (var j = 0; j < entries[4].Length - user[(int)Program.UserFormatEnum.group].Length; j++)
                {
                    Console.Write(" ");
                }

                Console.WriteLine("|");

                Console.WriteLine(line);
            }

            Input.End();
            Program.Back();
        }
    }

    class MenuLogin
    {
        public static Program.MenuIDs NameId = Program.MenuIDs.login;
        public static string Name = "Login";
        public static char Id = 'l';
        public static bool InputMenu = true;

        public static void Run()
        {


            try
            {
                do
                {
                    string username = Input.GetInput("Username");
                    string password = Input.GetPrivateInput("Password");

                    using (StreamReader sr = new StreamReader(Program.PathUsers))
                    {
                        //string[] users = sr.ReadToEnd().Split("\n");

                        //string[] user = new string[users[0].Split(",").Length];

                        string user;

                        string[] values;

                        while ((user = sr.ReadLine()) != null)
                        {
                            values = user.Split(",");

                            //values = new string[value.Split(",").Length];



                            if (values[0] == username && values[1] == FileEdit.Sha256Hash(password))
                            {
                                Program.CurrentUser = new User(values);
                                Program.ChangeMenu(Program.Menus[(int)Program.MenuIDs.dashboard]);
                                return;
                            }
                        }
                    }


                    Console.WriteLine("Wrong login info!");
                } while (Program.CurrentUser == null);
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Program.Logout();
                Input.End();
            }
        }
    }

    class MenuLeaderboard
    {
        public static Program.MenuIDs NameId = Program.MenuIDs.leaderboard;
        public static string Name = "Leaderboard";
        public static char Id = 'k';
        public static bool InputMenu = false;

        public static void Run()
        {
            /*bool edited = true;

            User[] usersSorted = Program.Users;

            while (edited) {
                for (var i = 0; i < usersSorted.Length; i++)
                {
                    User userFirst = usersSorted[i];
                    if (userFirst.values[]
                }
            }*/

            using (StreamReader sr = new StreamReader(Program.PathUsers))
            {
                string[] users = sr.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
                string[,] userValues = new string[users.Length, Program.UserFormat.Split(",").Length];

                for (var i = 0; i < users.Length; i++)
                {
                    for (var j = 0; j < userValues.GetLength(1); j++)
                    {
                        userValues[i, j] = users[i].Split(",")[j];
                    }
                }

                Array sorted = Program.BubbleSort(userValues, (int)Program.UserFormatEnum.highscore);
                //users = Array.ConvertAll<Array, string>(sorted, new Converter<Array, string>(users));

                for (var i = 0; i < sorted.GetLength(0); i++)
                {
                    Console.WriteLine($"{sorted.GetValue(i, (int)Program.UserFormatEnum.username)} {sorted.GetValue(i, (int)Program.UserFormatEnum.highscore)}");
                }

                Program.Back();
                Input.End();
            }
        }
    }
}
