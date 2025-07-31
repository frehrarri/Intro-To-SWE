using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class Signup
    {
        public Signup(List<User> users)
        {
            RegisteredUsers = new List<User>();
            RegisteredUsers = users;
        }

        List<User> RegisteredUsers { get; set; }

        public void RegisterUser()
        {
            User user = new User();

            //add a user if all information passes validation checks
            if (ValidateRegistrationForm(user))
            {
                RegisteredUsers.Add(user);
            }

            Console.WriteLine("");

            //prompt user to log in with newly added credentials
            Login login = new Login(RegisteredUsers);
            login.AppLogin();
        }

        private bool ValidateRegistrationForm(User user)
        {
            return ValidateUsername(user) && ValidatePassword(user) && ValidateAge(user) && ValidatePhone(user) && ValidateEmail(user);
        }

        private bool ValidateUsername(User user)
        {
            bool isValid = false;

            //continue prompting for username if it isn't a valid username
            do
            {
                Console.WriteLine("Please enter a username. Press x to return to the main menu.");
                user.Username = Console.ReadLine();

                //exit to main menu
                if (user.Username.ToLower() == "x")
                {
                    Bank bank = new Bank(RegisteredUsers);
                    bank.OpenStartMenu();
                }

                //is a valid string
                if (!String.IsNullOrEmpty(user.Username))
                {
                    //check to see if username already exists
                    var username = RegisteredUsers.SingleOrDefault(u => u.Username == user.Username);

                    //username doesn't exist
                    if (username == null)
                    {
                        isValid = true;
                    }
                    else //username exists already
                    {
                        Console.WriteLine($"Username already exists.");
                    }
                }
                else //not a valid string
                {
                    Console.WriteLine("Username cannot be empty.");
                }
            }
            while (isValid == false);

            return isValid;
        }

        private bool ValidatePassword(User user)
        {
            bool isValid = false;

            //continue prompting for password if it isn't a valid password
            do
            {
                Console.WriteLine("Please enter a password. Passwords must have atleast 6 characters. Press x to return to the main menu.");
                user.Password = Console.ReadLine();

                //exit to main menu
                if (user.Password.ToLower() == "x")
                {
                    Bank bank = new Bank(RegisteredUsers);
                    bank.OpenStartMenu();
                }

                //password is valid string
                if (!String.IsNullOrEmpty(user.Password))
                {
                    //password passes the minimum character requirement
                    if (user.Password.Count() >= 6)
                    {
                        isValid = true;
                    }
                    else //password doesn't have enough characters
                    {
                        Console.WriteLine("Passwords must have at least 6 characters.");
                    }
                    
                }
                else //password isn't a valid string
                {
                    Console.WriteLine("Passwords cannot be empty and must have atleast 6 characters.");
                }
            }
            //check that its a valid password
            while (isValid == false);

            return isValid;
        }

        private bool ValidateAge(User user)
        {
            bool isValid = false;

            do
            {
                Console.WriteLine("Please enter your age. Press x to return to the main menu.");
                string age = Console.ReadLine();

                //exit to main menu
                if (age.ToLower() == "x")
                {
                    Bank bank = new Bank(RegisteredUsers);
                    bank.OpenStartMenu();
                }

                //is a valid string
                if (!String.IsNullOrEmpty(age))
                {
                    bool isParsed = Int32.TryParse(age, out int parsedAge);

                    //successfully converted age from string to int
                    if (isParsed)
                    {
                        //age is between 1 and 110
                        if (parsedAge > 0 && parsedAge <= 110)
                        {
                            user.Age = parsedAge;
                            isValid = true;
                        }
                        else //age isn't between the allowed ranged
                        {
                            Console.WriteLine("Please enter your age between 1 and 110.");
                        }
                    }
                } 
                else //not a valid string
                {
                    Console.WriteLine("Please enter a valid number between 1 and 110.");
                }
                
            }
            while(isValid == false);

            return isValid;
        }

        private bool ValidatePhone(User user)
        {
            bool isValid = false;
            do
            {
                Console.WriteLine("Please enter your phone number. Press x to return to the main menu.");
                string phone = Console.ReadLine();

                //exit to main menu
                if (phone.ToLower() == "x")
                {
                    Bank bank = new Bank(RegisteredUsers);
                    bank.OpenStartMenu();
                }

                //is a valid string
                if (!String.IsNullOrEmpty(phone))
                {
                    //there is a correct amount of characters
                    if (phone.Count() == 10)
                    {
                        bool isParsed = long.TryParse(phone, out long parsedPhone);

                        //successfully parsed from string to long
                        if (isParsed)
                        {
                            isValid = true;
                            user.Phone = parsedPhone;
                        }
                      
                    }
                    else //incorrect amount of characters
                    {
                        Console.WriteLine("Not enough digits. Phone numbers have 10 digits.");
                    }
                }
                else //not a valid string
                {
                    Console.WriteLine("Please enter a valid phone number");
                }
            }
            while (isValid == false);

            return isValid;
        }

        private bool ValidateEmail(User user)
        {
            bool isValid = false;

            //continue prompting for username if it isn't a valid username
            do
            {
                Console.WriteLine("Please enter your .com email address. Press x to return to the main menu.");
                user.Email = Console.ReadLine();

                //exit to main menu
                if (user.Email.ToLower() == "x")
                {
                    Bank bank = new Bank(RegisteredUsers);
                    bank.OpenStartMenu();
                }

                //is a valid string
                if (!String.IsNullOrEmpty(user.Email))
                {
                    //email has necessary formatting (probably) (Could use regex to ensure proper formatting)
                    if (user.Email.Contains("@") && user.Email.Contains(".com"))
                    {
                        isValid = true;
                    }
                    else //email exists already
                    {
                        Console.WriteLine($"Email formatting is incorrect.");
                    }
                }
                else //not a valid string
                {
                    Console.WriteLine("Email cannot be empty.");
                }
            }
            while (isValid == false);

            return isValid;
        }

    }
}
