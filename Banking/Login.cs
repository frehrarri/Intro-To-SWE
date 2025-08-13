using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class Login
    {
        public Login(List<User> users) 
        {
            PasswordAttempts = 0;
            RegisteredUsers = new List<User>();
            RegisteredUsers = users;
        }
        
        private List<User> RegisteredUsers { get; set; }
        private int PasswordAttempts { get; set; }
        

        public void AppLogin()
        {
            BankManager bankManager = new BankManager(RegisteredUsers);
            bool isValidCredentials = false;

            do
            {
                //prompt user for username and password
                Console.WriteLine("\nEnter your credentials to log in:");
                Console.WriteLine("\nWhat is your username?");
                string? username = Console.ReadLine();

                Console.WriteLine("\nWhat is your password?");
                string? password = Console.ReadLine();

                if (!String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(username))
                {
                    //validate credentials
                    isValidCredentials = ValidateCredentials(password, username);
                }

                //if credentials are valid login is successful
                if (isValidCredentials)
                {
                    Console.WriteLine("\nLogin Succesful!");
                    Thread.Sleep(500);
                    PasswordAttempts = 0;

                    bankManager.CurrentUser = GetUser(username); //logged in user

                    bankManager.OpenMainMenu();
                }
                //prompt user for credentials again
                else if (PasswordAttempts < 3)
                {
                    Console.WriteLine("Invalid Username or Password.");
                    OpenRetryMenu(bankManager);
                }
                //if the credential attempts is incorrect 3x then exit the program
                else
                {
                    Console.WriteLine("Too many incorrect attempts.");
                    Thread.Sleep(500);
                    bankManager.QuitApplication();
                }
            }
            while (PasswordAttempts <= 3 && !isValidCredentials);
        }

        private void OpenRetryMenu(BankManager bankManager)
        {
            //display options
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("\n1. Try again");
            Console.WriteLine("2. Main menu");
            Console.WriteLine("3. Quit");
            Console.WriteLine(sb);

            //prompt user until they select an option that is within our array
            string[] menuOptions = { "1", "2", "3" };
            string? userInput = String.Empty;
            do
            {
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        AppLogin();
                        break;
                    case "2":
                        bankManager.OpenStartMenu();
                        break;
                    case "3":
                        bankManager.QuitApplication();
                        break;
                    default:
                        Console.WriteLine("Invalid option, please select an option between 1 and 3.");
                        break;
                }
            }
            while (!menuOptions.Contains(userInput));
        }

        private bool ValidateCredentials(string password, string username)
        {
            //search for a registered user by the input username as well as a password that matches the input password
            string? correctPassword = RegisteredUsers.Where(credentials => credentials?.Username?.ToLower() == username.ToLower() && password == credentials.Password)
                                                    .Select(pass => pass.Password)
                                                    .SingleOrDefault();

            if (password == correctPassword)
            {
                return true;
            }

            PasswordAttempts++;
            return false;
        }

        public User GetUser(string username)
        {
            return RegisteredUsers?.SingleOrDefault(user => user?.Username.ToLower() == username.ToLower());
        }
    }
}
