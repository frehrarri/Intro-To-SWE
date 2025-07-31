using Banking;

//TODO: test everything

//add this default user on init
List<User> Users = new List<User>();
Users.Add(new User()
{
    Username = "John.Doe",
    Email = "John.Doe@gmail.com",
    Age = 34,
    Phone = 1234567890,
    Password = "Password123"
});

Bank bank = new Bank(Users);
bank.OpenStartMenu();
