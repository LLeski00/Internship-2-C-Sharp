using System.Security.Principal;

public class Transaction
{
    private int id;
    private decimal amount;
    private string description;
    private string type;
    private string category;
    private DateTime date;

    public decimal GetAmount()
    {
        return amount;
    }
    public string GetDescription()
    {
        return description;
    }
    public string GetType()
    {
        return type;
    }
    public string GetCategory()
    {
        return category;
    }
    public DateTime GetDate()
    {
        return date;
    }

    public void SetAmount(decimal amount)
    {
        this.amount = amount;
    }
    public void SetDescription(string description)
    {
        this.description = description;
    }
    public void SetType(string type)
    {
        this.type = type;
    }
    public void SetCategory(string category)
    {
        this.category = category;
    }
    public void SetDate(DateTime date)
    {
        this.date = date;
    }
}

public class Account
{
    private int id;
    private decimal balance;
    private string type;
    List<Transaction> transactions;

    public Account(string type)
    {
        this.id = IDGenerator.GetNextId();

        if(type == "Tekuci racun")
        {
            this.balance = 100.00m;
        }
        else
        {
            this.balance = 0.00m;
        }

        this.type = type;

    }

    public decimal GetBalance()
    {
        return balance;
    }
    public List<Transaction> GetTransactions()
    {
        return transactions;
    }

    public void SetBalance(decimal balance)
    {
        this.balance = balance;
    }
}

public class User
{
    private int id;
    private string firstName;
    private string lastName;
    private DateTime birthDate;
    private List<Account> accounts;

    public User()
    {
        this.id = IDGenerator.GetNextId();
        this.firstName = string.Empty;
        this.lastName = string.Empty;
        this.birthDate = DateTime.Now;
        this.accounts = new List<Account>();
        var currentAccount = new Account("Tekuci racun");
        var giroAccount = new Account("Ziro racun");
        var webPrepaidAccount = new Account("Web Prepaid racun");
        this.accounts.Add(currentAccount);
        this.accounts.Add(giroAccount);
        this.accounts.Add(webPrepaidAccount);
    }

    public string GetFirstName()
    {
        return this.firstName;
    }
    public string GetLastName()
    {
        return this.lastName;
    }
    public DateTime GetBirthDate()
    {
        return this.birthDate;
    }
    public List<Account> GetAccounts()
    {
        return this.accounts;
    }

    public void SetFirstName(string name)
    {
        this.firstName = name;
    }
    public void SetLastName(string name)
    {
        this.lastName = name;
    }
    public void SetBirthDate(DateTime date)
    {
        this.birthDate = date;
    }
}

public static class IDGenerator
{
    private static int lastId = 0;

    public static int GetNextId()
    {
        return ++lastId;
    }
}

class Program
{
    static bool shutdown = false;
    static List<User> users = new List<User>();

    static void Main()
    {
        do
        {
            DisplayMainMenu();
            int.TryParse(Console.ReadLine(), out var option);

            switch (option)
            {
                case 0:
                    Console.WriteLine("Hvala na korištenju!");
                    shutdown = true;
                    break;
                case 1:
                    UsersMenu();
                    break;
                case 2:
                    AccountsMenu();
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }
        } while (!shutdown);
    }

    static void UsersMenu()
    {
        var exit = false;

        do
        {
            DisplayUsersMenu();
            int.TryParse(Console.ReadLine(), out var option);

            switch (option)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                    AddNewUser();
                    break;
                case 2:
                    DeleteUser();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }
        } while (!exit);
    }

    static void AccountsMenu()
    {
        var exit = false;
        var user = FindUser();

        do
        {
            DisplayAccounts(user);
            int.TryParse(Console.ReadLine(), out var account);

            switch (account)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                case 2:
                case 3:
                    TransactionsManagement(/*account*/);
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }

        } while (!exit);
    }

    static void TransactionsManagement(/*account*/)
    {
        var exit = false;

        do
        {
            DisplayTransactionsMenu();
            int.TryParse(Console.ReadLine(), out var option);

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 0:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }
        } while (!exit);
    }

    static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Korisnici");
        Console.WriteLine("2. Računi");
        Console.WriteLine("0. Izlaz iz aplikacije");
        Console.Write("Tvoj odabir: ");
    }

    static void DisplayUsersMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Unos novog korisnika");
        Console.WriteLine("2. Brisanje korisnika");
        Console.WriteLine("3. Uređivanje korisnika");
        Console.WriteLine("4. Pregled korisnika");
        Console.WriteLine("0. Natrag na glavni izbornik");
        Console.Write("Tvoj odabir: ");
    }

    static void DisplayAccounts(User user)
    {
        var accounts = user.GetAccounts();

        Console.Clear();
        Console.WriteLine($"1. Tekući račun, Iznos: {accounts[0].GetBalance()} €");
        Console.WriteLine($"2. Žiro račun, Iznos: {accounts[1].GetBalance()} €");
        Console.WriteLine($"3. Web prepaid račun, Iznos: {accounts[2].GetBalance()} €");
        Console.WriteLine("0. Natrag");
        Console.Write("Tvoj odabir: ");
    }

    static void DisplayTransactionsMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Unos nove transakcije");
        Console.WriteLine("2. Brisanje transakcije");
        Console.WriteLine("3. Uređivanje transakcije");
        Console.WriteLine("4. Pregled transakcija");
        Console.WriteLine("5. Financijsko izvješće");
        Console.WriteLine("0. Natrag");
        Console.Write("Tvoj odabir: ");
    }

    static void AddNewUser()
    {
        do
        {
            Console.Clear();
            Console.Write("Unesite ime: ");
            var firstName = Console.ReadLine();
            Console.Write("Unesite prezime: ");
            var lastName = Console.ReadLine();
            Console.Write("Unesite datum rodenja (YYYY-MM-DD): ");

            if (DateTime.TryParse(Console.ReadLine(), out var date))
            {
                var user = new User();
                user.SetFirstName(firstName);
                user.SetLastName(lastName);
                user.SetBirthDate(date);
                users.Add(user);
                break;
            }
            else
            {
                Console.WriteLine("Pogreska pri unosenju datuma rodenja!");
                Console.ReadLine();
            }
        } while (true);
    }

    static void DeleteUser()
    {
        var user = FindUser();
        users.Remove(user);
    }

    static User FindUser()
    {
        var exit = false;
        var validUser = false;
        User user = null;

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite ime korisnika: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Unesite prezime korisnika: ");
            var lastName = Console.ReadLine();
            user = users.FirstOrDefault(item => item.GetFirstName() == firstName && item.GetLastName() == lastName);

            if (user != default)
            {
                validUser = true;
            }
            else
            {
                Console.WriteLine("Korisnik nije pronađen!");
                Console.ReadLine();
            }
        } while (!validUser);

        return user;
    }
}

