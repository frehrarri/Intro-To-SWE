using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class Bank
    {
        public Bank(List<User> users)
        {
            RegisteredUsers = new List<User>();
            RegisteredUsers = users;
        }

        List<User> RegisteredUsers { get; set; }

        public void OpenStartMenu()
        {
            //display options
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Welcome to ABC Bank.");
            sb.AppendLine("1. Login");
            sb.AppendLine("2. Signup");
            sb.AppendLine("3. Quit");
            Console.WriteLine(sb);

            //prompt user until they select an option that is within our array
            string[] menuOptions = { "1", "2", "3" };
            string userInput = String.Empty;
            do
            {
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Login login = new Login(RegisteredUsers);
                        login.AppLogin();
                        break;
                    case "2":
                        Signup signup = new Signup(RegisteredUsers);
                        signup.RegisterUser();
                        break;
                    case "3":
                        QuitApplication();
                        break;
                    default:
                        break;
                }
            }
            while (!menuOptions.Contains(userInput));
        }

        public void OpenMainMenu()
        {
            //display options
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Welcome!");
            sb.AppendLine("1. View Balance");
            sb.AppendLine("2. Deposit");
            sb.AppendLine("3. Withdraw");
            sb.AppendLine("4. Transfer");
            sb.AppendLine("5. Quit");
            Console.WriteLine(sb);

            //prompt user until they select an option that is within our array
            string[] menuOptions = { "1", "2", "3", "4", "5" };
            string userInput = String.Empty;
            do
            {
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        //todo: view balance
                        throw new NotImplementedException();
                        break;
                    case "2":
                        //todo: Deposit
                        throw new NotImplementedException();
                        break;
                    case "3":
                        //todo: Withdraw
                        throw new NotImplementedException();
                        break;
                    case "4":
                        //todo: Transfer
                        throw new NotImplementedException();
                        break;
                    case "5":
                        QuitApplication();
                        break;
                    default:
                        break;
                }
            }
            while (!menuOptions.Contains(userInput));
        }

        public void QuitApplication()
        {
            Console.WriteLine("Goodbye.");
            Thread.Sleep(500);
            Environment.Exit(0);
        }
    }
}
