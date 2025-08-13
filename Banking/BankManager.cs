using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class BankManager
    {
        public BankManager()
        {
            RegisteredUsers = new List<User>();

            RegisteredUsers.Add(new User()
            {
                Username = "John.Doe",
                Name = "John",
                Email = "John.Doe@gmail.com",
                Age = 34,
                Phone = 1234567890,
                Password = "Password123",
                AccountBalance = 100
            });

            RegisteredUsers.Add(new User()
            {
                Username = "Jeff.Doe",
                Name = "Jeff",
                Email = "Jeff.Doe@gmail.com",
                Age = 31,
                Phone = 0987654321,
                Password = "Password123",
                AccountBalance = 0
            });
   
        }

        public BankManager(List<User> users)
        {
            RegisteredUsers = new List<User>();
            RegisteredUsers = users;
            //Balance = 0m;
        }

        List<User> RegisteredUsers { get; set; }
        public User CurrentUser { get; set; }

        #region Menu Functions

        public void OpenStartMenu()
        {
            //display options
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\nWelcome to ABC Bank.");
            sb.AppendLine("1. Login");
            sb.AppendLine("2. Signup");
            sb.AppendLine("3. Quit");
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
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            while (!menuOptions.Contains(userInput));
        }

        public void OpenMainMenu()
        {
            //display options
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\nWelcome {CurrentUser.Name}!");
            sb.AppendLine("1. View Balance");
            sb.AppendLine("2. Deposit");
            sb.AppendLine("3. Withdraw");
            sb.AppendLine("4. Transfer");
            sb.AppendLine("5. Quit");
            Console.WriteLine(sb);

            //prompt user until they select an option that is within our array
            string[] menuOptions = { "1", "2", "3", "4", "5" };
            string? userInput = String.Empty;
            do
            {
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        ViewBalance();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        Withdraw();
                        break;
                    case "4":
                        Transfer();
                        break;
                    case "5":
                        QuitApplication();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
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

        #endregion

        #region Banking Functions

        private void ViewBalance()
        {
            Console.WriteLine($"\n Current balance: ${CurrentUser.AccountBalance}");
            OpenMainMenu();
        }

        private void Deposit()
        {
            Console.Write("\nEnter amount to deposit. Press x to return to the main menu.\n");
            string? amount = Console.ReadLine();

            //if valid input try and parse it to a useable value
            if (!String.IsNullOrEmpty(amount))
            {
                //return to main menu
                if (amount.ToLower() == "x")
                {
                    OpenMainMenu();
                }
            }

            bool isValid = ValidateDeposit(amount);

            if (isValid)
            {
                //add new value to balance
                CurrentUser.AccountBalance += decimal.Round(Decimal.Parse(amount), 2, MidpointRounding.ToEven);
                Console.WriteLine($"Deposit successful. New balance: ${CurrentUser.AccountBalance}");

                do
                {
                    Console.WriteLine("\nWould you like to make another deposit? Enter yes or no");
                    amount = Console.ReadLine();

                    if (amount != "yes" && amount != "no")
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                    else if (amount.ToLower() == "yes") //make another deposit
                    {
                        Deposit();
                    }
                    else if (amount.ToLower() == "no") //return to main menu
                    {
                        OpenMainMenu();
                    }
                }
                while (amount != "yes" || amount != "no");

            }
            else //validation failed reprompt user
            {
                Deposit();
            }
        }

        private void Withdraw()
        {
            Console.WriteLine("\nEnter amount to withdraw. Press x to return to the main menu.\n");
            string? amount = Console.ReadLine();

            //if valid input try and parse it to a useable value
            if (!String.IsNullOrEmpty(amount))
            {
                //return to main menu
                if (amount.ToLower() == "x")
                {
                    OpenMainMenu();
                }
            }

            bool isValid = ValidateWithdrawal(amount);
            if (isValid)
            {
                //subtract from balance
                CurrentUser.AccountBalance -= Decimal.Parse(amount);
                Console.WriteLine($"Withdrawal successful. New balance: ${CurrentUser.AccountBalance}");

                do
                {
                    Console.WriteLine("\nWould you like to make another withdrawal? Enter yes or no");
                    amount = Console.ReadLine();

                    if (amount != "yes" && amount != "no")
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                    else if (amount.ToLower() == "yes") //make another withdrawal
                    {
                        Withdraw();
                    }
                    else if (amount.ToLower() == "no") //return to main menu
                    {
                        OpenMainMenu();
                    }
                }
                while (amount != "yes" || amount != "no");
            }
            else //validation failed so reprompt user
            {
                Withdraw();
            }
        }

        private void Transfer()
        {
            Console.WriteLine("\nEnter username of the person you would like to transfer funds to. Press x to return to the main menu.");
            string? username = Console.ReadLine();

            Console.WriteLine("\nEnter amount to transfer. Press x to return to the main menu.\n");
            string? value = Console.ReadLine();

            //if valid input try and parse it to a useable value
            if (!String.IsNullOrEmpty(value) || !String.IsNullOrEmpty(username))
            {
                //return to main menu
                if (value.ToLower() == "x" || username.ToLower() == "x")
                {
                    OpenMainMenu();
                }
            }

            bool isValid = ValidateTransfer(username, value);

            if (isValid)
            {
                //subtract from balance
                CurrentUser.AccountBalance -= decimal.Parse(value);

                //add to other users balance
                Login user = new Login(RegisteredUsers);
                User recepient = user.GetUser(username);
                recepient.AccountBalance += decimal.Parse(value);

                Console.WriteLine($"Transfer successful. New balance: ${CurrentUser.AccountBalance}");

                do
                {
                    Console.WriteLine("\nWould you like to make another transfer? Enter yes or no");
                    value = Console.ReadLine();

                    if (value != "yes" && value != "no")
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                    else if (value.ToLower() == "yes") //make another Transfer
                    {
                        Transfer();
                    }
                    else if (value.ToLower() == "no") //return to main menu
                    {
                        OpenMainMenu();
                    }
                }
                while (value != "yes" || value != "no");
            }
            else //validation failed so reprompt user
            {
                Transfer();
            }
        }

        #endregion

        #region Validation Functions

        private bool ValidateDeposit(string value)
        {
            bool isValid = true;

            if (!decimal.TryParse(value, out decimal amount))
            {
                isValid = false;
                Console.WriteLine("\nOnly numerical data to be entered for deposit.");
            }
            if (amount <= 0)
            {
                isValid = false;
                Console.WriteLine("\nDeposit must be a positive amount.");
            }

            return isValid;
        }

        private bool ValidateWithdrawal(string value)
        {
            bool isValid = true;

            //not a valid decimal
            if (!decimal.TryParse(value, out decimal amount))
            {
                Console.WriteLine("Only numerical data to be entered for transfer.");
                isValid = false;
            }
            //not a valid amount to transfer
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawals must be a positive amount.");
                isValid = false;
            }

            amount = decimal.Round(amount, 2, MidpointRounding.ToEven);

            //cannot deduct more than you have available
            if (amount > CurrentUser.AccountBalance)
            {
                Console.WriteLine("Not sufficient funds available.");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateTransfer(string? username, string value)
        {
            bool isValid = true;
            Login login = new Login(RegisteredUsers);

            //invalid username 
            if (login.GetUser(username) == null)
            {
                isValid = false;
                Console.WriteLine("This user does not exist.");
            }
            if (username == CurrentUser.Username)
            {
                isValid = false;
                Console.WriteLine("Cannot send money to yourself.");
            }
            //not a valid decimal
            if (!decimal.TryParse(value, out decimal amount))
            {
                Console.WriteLine("Only numerical data to be entered for transfer.");
                isValid = false;
            }
            //not a valid amount to transfer
            if (amount <= 0)
            {
                Console.WriteLine("Transfer must be a positive amount.");
                isValid = false;
            }

            amount = decimal.Round(amount, 2, MidpointRounding.ToEven);

            //cannot deduct more than you have available
            if (amount > CurrentUser.AccountBalance)
            {
                Console.WriteLine("Not sufficient funds available.");
                isValid = false;
            }

            return isValid;
        }

        #endregion
    }
}
