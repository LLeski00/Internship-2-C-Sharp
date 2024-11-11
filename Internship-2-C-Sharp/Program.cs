using System.Security.Principal;

struct Transaction
{
    public int id;
    public decimal amount;
    public string description;
    public string type;
    public string category;
    public DateTime date;
}

struct Account
{
    public int id;
    public decimal balance;
    public string type;
    public List<Transaction> transactions;

    public void Initialize(string type)
    {
        id = 0;
        this.type = type;

        if (type == "Tekući račun")
            balance = 100.00m;
        else
            balance = 0.00m;
        transactions = new List<Transaction>();
    }
}

struct User
{
    public int id;
    public string firstName;
    public string lastName;
    public DateTime birthDate;
    public List<Account> accounts;

    public void Initialize()
    {
        id = 0;
        firstName=string.Empty;
        lastName=string.Empty;
        birthDate = DateTime.MinValue;
        accounts = new List<Account>();
        var currentAccount = new Account();
        currentAccount.Initialize("Tekući račun");
        var giroAccount = new Account();
        giroAccount.Initialize("Žiro račun");
        var prepaidAccount = new Account();
        prepaidAccount.Initialize("Prepaid račun");
        accounts.Add(currentAccount);
        accounts.Add(giroAccount);
        accounts.Add(prepaidAccount);
    }
}

class Program
{
    static List<User> users = new List<User>();
    static int lastUserId = 0;

    static void Main()
    {
        var shutdown = false;

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
        var user = FindUserByName();

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
        var accounts = user.accounts;

        Console.Clear();
        Console.WriteLine($"1. Tekući račun, Iznos: {accounts[0].balance} €");
        Console.WriteLine($"2. Žiro račun, Iznos: {accounts[1].balance} €");
        Console.WriteLine($"3. Web prepaid račun, Iznos: {accounts[2].balance} €");
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
                user.Initialize();
                user.id = lastUserId++;
                user.firstName = firstName;
                user.lastName = lastName;
                user.birthDate = date;
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
        var exit = false;
        User user = new User();

        do
        {
            Console.Clear();
            Console.WriteLine("Pretraga po:");
            Console.WriteLine("1. ID");
            Console.WriteLine("2. Ime i prezime");
            Console.WriteLine("0. Natrag");
            int.TryParse(Console.ReadLine(), out var filter);
            
            switch (filter)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                    user = FindUserById();
                    users.Remove(user);
                    Console.WriteLine("Korisnik uspješno izbrisan!");
                    Console.ReadLine();
                    break;
                case 2:
                    user = FindUserByName();
                    users.Remove(user);
                    Console.WriteLine("Korisnik uspješno izbrisan!");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }
            
        } while (!exit);
    }

    static User FindUserById()
    {
        var validUser = false;
        User user = new User();

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite ID korisnika: ");
            int.TryParse(Console.ReadLine(),out var id);
            user = users.FirstOrDefault(item => item.id == id);

            if (!user.Equals(default(User)))
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

    static User FindUserByName()
    {
        var validUser = false;
        User user = new User();

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite ime korisnika: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Unesite prezime korisnika: ");
            var lastName = Console.ReadLine();
            user = users.FirstOrDefault(item => item.firstName == firstName && item.lastName == lastName);

            if (!user.Equals(default(User)))
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

