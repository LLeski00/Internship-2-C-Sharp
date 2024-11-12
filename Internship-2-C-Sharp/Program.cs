using System;
using System.Reflection;
using System.Security.Principal;

class Program
{
    static List<Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>>> users = new List<Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>>>();
    static int lastUserId = 0;
    static int lastAccountId = 0;
    static int lastTransactionId = 0;

    static void Main()
    {
        users.Add(GenerateDefaultUser("Ivo", "Ivic", new DateTime(2000,11,11)));
        users.Add(GenerateDefaultUser("Marko", "Maric", new DateTime(2000, 11, 11)));
        users.Add(GenerateDefaultUser("Stipe", "Stipic", new DateTime(2000, 11, 11)));

        var shutdown = false;

        do
        {
            DisplayMainMenu();
            if(!int.TryParse(Console.ReadLine(), out var option))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }

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

    static Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>> GenerateDefaultUser(string name, string surname, DateTime date)
    {
        var transaction1 = new Dictionary<string, string>() {
            {"id", "0"},
            {"amount", "1000.00" },
            {"description", "Standardna transakcija"},
            {"type", "Income"},
            {"category", "Salary"},
            {"date", "2000-11-11"},
        };
        var transaction2 = new Dictionary<string, string>() {
            {"id", "1"},
            {"amount", "10.00" },
            {"description", "Standardna transakcija"},
            {"type", "Expense"},
            {"category", "Food"},
            {"date", "2000-11-11"},
        };
        var transaction3 = new Dictionary<string, string>() {
            {"id", "2"},
            {"amount", "50.00" },
            {"description", "Standardna transakcija"},
            {"type", "Income"},
            {"category", "Gift"},
            {"date", "2000-11-11"},
        };

        var currentAccountTransactions = new List<Dictionary<string, string>>();
        currentAccountTransactions.Add(transaction1);
        currentAccountTransactions.Add(transaction2);
        currentAccountTransactions.Add(transaction3);

        var currentAccount = Tuple.Create(0, 100.00m, "Current", currentAccountTransactions);

        var giroAccountTransactions = new List<Dictionary<string, string>>();
        giroAccountTransactions.Add(transaction1);
        giroAccountTransactions.Add(transaction2);
        giroAccountTransactions.Add(transaction3);

        var giroAccount = Tuple.Create(0, 0.00m, "Giro", giroAccountTransactions);

        var prepaidAccountTransactions = new List<Dictionary<string, string>>();
        prepaidAccountTransactions.Add(transaction1);
        prepaidAccountTransactions.Add(transaction2);
        prepaidAccountTransactions.Add(transaction3);

        var prepaidAccount = Tuple.Create(0, 0.00m, "Prepaid", prepaidAccountTransactions);

        var accounts = new List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>();
        accounts.Add(currentAccount);
        accounts.Add(giroAccount);
        accounts.Add(prepaidAccount);

        var user = Tuple.Create(lastUserId++, name, surname, date, accounts);

        return user;
    }

    static void UsersMenu()
    {
        var exit = false;

        do
        {
            DisplayUsersMenu();
            if (!int.TryParse(Console.ReadLine(), out var option))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }

            switch (option)
            {
                case 0:
                    exit = true;
                    break;
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
                    FilterUsers();
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
        var userIndex = users.FindIndex(item => item.Item1 == user.Item1);

        do
        {
            DisplayAccounts(user);
            if (!int.TryParse(Console.ReadLine(), out var account))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }

            switch (account)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                case 2:
                case 3:
                    users[userIndex] = TransactionsManagement(user, account-1);
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }

        } while (!exit);
    }

    static Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>> TransactionsManagement(Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>> user, int accountIndex)
    {
        var exit = false;
        var account = Tuple.Create(0, 0.00m, "", new List<Dictionary<string, string>>());

        do
        {
            DisplayTransactionsMenu();
            if (!int.TryParse(Console.ReadLine(), out var option))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }

            switch (option)
            {
                case 1:
                    account = AddNewTransaction(user.Item5[accountIndex]);
                    user.Item5[accountIndex] = account;
                    break;
                case 2:
                    account = DeleteTransaction(user.Item5[accountIndex]);
                    user.Item5[accountIndex] = account;
                    break;
                case 3:
                    break;
                case 4:
                    DisplayTransactions(user.Item5[accountIndex]);
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

        return user;
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

    static void DisplaySortedUsersMenu()
    {
        Console.Clear();
        Console.WriteLine("Ispisi korisnike: ");
        Console.WriteLine("1. Sortirani abecedno");
        Console.WriteLine("2. Korisnici stariji od 30 godina");
        Console.WriteLine("3. Zaduženi korisnici");
        Console.WriteLine("0. Natrag");
        Console.Write("Tvoj odabir: ");
    }

    static void DisplayEditUserMenu()
    {
        Console.Clear();
        Console.WriteLine("Izmjeni korisnikovo: ");
        Console.WriteLine("1. Ime");
        Console.WriteLine("2. Prezime");
        Console.WriteLine("3. Datum rođenja");
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

    static void DisplayUsers(List<Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>>> sortedUsers, string filter)
    {
        switch (filter)
        {
            case "":
                foreach (var user in sortedUsers)
                    Console.WriteLine($"{user.Item1} - {user.Item2} - {user.Item3} - {user.Item4}");
                break;
            case "OlderThan30":
                foreach (var user in sortedUsers)
                {
                    var today = DateTime.Today;

                    var age = today.Year - user.Item4.Year;

                    if (today.Month < user.Item4.Month || (today.Month == user.Item4.Month && today.Day < user.Item4.Day))
                        age--;

                    if (age >= 30)
                        Console.WriteLine($"{user.Item1} - {user.Item2} - {user.Item3} - {user.Item4}");
                }
                break;
            case "InDebt":
                foreach (var user in sortedUsers)
                {
                    foreach (var account in user.Item5)
                    {
                        if (account.Item2 < 0.00m)
                        {
                            Console.WriteLine($"{user.Item1} - {user.Item2} - {user.Item3} - {user.Item4}");
                            break;
                        }
                    }
                }
                break;
            default:
                Console.WriteLine("Nepoznati filter!");
                break;
        }
    }

    static void DisplayAccounts(Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>> user)
    {
        var accounts = user.Item5;

        Console.Clear();
        Console.WriteLine($"1. Tekući račun, Iznos: {accounts[0].Item2} EUR");
        Console.WriteLine($"2. Žiro račun, Iznos: {accounts[1].Item2} EUR");
        Console.WriteLine($"3. Web prepaid račun, Iznos: {accounts[2].Item2} EUR");
        Console.WriteLine("0. Natrag");
        Console.Write("Tvoj odabir: ");
    }

    static void DisplayTransactions(Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        Console.Clear();
        Console.WriteLine("Transakcije");

        foreach(var transaction in account.Item4)
        {
            Console.WriteLine($"{transaction["type"]} - {transaction["amount"]} - {transaction["description"]} - {transaction["category"]} - {transaction["date"]}");
        }

        Console.ReadLine();
    }

    static void AddUser()
    {
        do
        {
            Console.Clear();
            Console.Write("Unesite ime: ");
            var firstName = Console.ReadLine();
            Console.Write("Unesite prezime: ");
            var lastName = Console.ReadLine();
            Console.Write("Unesite datum rodenja (YYYY-MM-DD): ");

            if (DateTime.TryParse(Console.ReadLine(), out var date) && firstName != string.Empty && lastName != string.Empty)
            {
                var user = GenerateDefaultUser(firstName, lastName, date);
                users.Add(user);
                Console.WriteLine("Korisnik uspješno dodan!");
                Console.ReadLine();
                break;
            }
            else
            {
                Console.WriteLine("Pogreska pri unosenju!");
                Console.ReadLine();
            }
        } while (true);
    }

    static void DeleteUser()
    {
        var exit = false;
        var user = GenerateDefaultUser("", "", DateTime.Now);

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
                    var id = InputId();
                    user = FindUserById(id);
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

    static void EditUser()
    {
        var id = InputId();
        var user = FindUserById(id);
        var newUser = GenerateDefaultUser("", "", DateTime.Now);
        var userIndex = users.FindIndex(item => item.Item1 == user.Item1);
        var exit = false;

        do
        {
            user = FindUserById(id);
            DisplayEditUserMenu();
            int.TryParse(Console.ReadLine(), out var option);
            Console.Clear();

            switch (option)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                    Console.WriteLine($"Trenutno ime: {user.Item2}, unesite novo ime:");
                    var firstName = Console.ReadLine();
                    if (firstName == null) {
                        Console.WriteLine("Niste unijeli ime!");
                        break;
                    }
                    newUser = Tuple.Create(user.Item1, firstName, user.Item3, user.Item4, user.Item5);
                    Console.WriteLine($"{userIndex}");
                    users[userIndex] = newUser;
                    Console.WriteLine("Uspješna izmjena!");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine($"Trenutno prezime: {user.Item3}, unesite novo prezime:");
                    var lastName = Console.ReadLine();
                    if (lastName == null)
                    {
                        Console.WriteLine("Niste unijeli ime!");
                        break;
                    }
                    newUser = Tuple.Create(user.Item1, user.Item2, lastName, user.Item4, user.Item5);
                    users[userIndex] = newUser;
                    Console.WriteLine("Uspješna izmjena!");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine($"Trenutni datum rođenja: {user.Item4}, unesite novi datum rođenja:");
                    if(!DateTime.TryParse(Console.ReadLine(), out var birthdate))
                    {
                        Console.WriteLine("Nepoznat format datuma!");
                        break;
                    }
                    newUser = Tuple.Create(user.Item1, user.Item2, user.Item3, birthdate, user.Item5);
                    users[userIndex] = user;
                    Console.WriteLine("Uspješna izmjena!");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }
        } while (!exit);
    }

    static void FilterUsers()
    {
        var exit = false;
        var sortedUsers = users
            .OrderBy(user => user.Item3)
            .ThenBy(user => user.Item2)
            .ToList();

        do
        {
            DisplaySortedUsersMenu();
            if (!int.TryParse(Console.ReadLine(), out var filter))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }
            Console.Clear();

            switch (filter)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                    Console.WriteLine("KORISNICI");
                    DisplayUsers(sortedUsers, "");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("KORISNICI STARIJI OD 29 GODINA");
                    DisplayUsers(sortedUsers, "OlderThan30");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("ZADUZENI KORISNICI");
                    DisplayUsers(sortedUsers, "InDebt");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }
        } while (!exit);
    }

    static int InputId()
    {
        var id = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite ID: ");
            if (int.TryParse(Console.ReadLine(), out id))
                break;
        } while (true);

        return id;
    }

    static Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>> FindUserById(int id)
    {
        var validUser = false;
        var user = GenerateDefaultUser("", "", DateTime.Now);

        do
        {
            user = users.FirstOrDefault(item => item.Item1 == id);

            if (user != null && !user.Equals(default(Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>>)))
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

    static Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>> FindUserByName()
    {
        var validUser = false;
        var user = GenerateDefaultUser("", "", DateTime.Now);

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite ime korisnika: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Unesite prezime korisnika: ");
            var lastName = Console.ReadLine();
            user = users.FirstOrDefault(item => item.Item2 == firstName && item.Item3 == lastName);

            if (user != null && !user.Equals(default(Tuple<int, string, string, DateTime, List<Tuple<int, decimal, string, List<Dictionary<string, string>>>>>)))
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

    static Tuple<int, decimal, string, List<Dictionary<string, string>>> AddNewTransaction(Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        var exit = false;
        var transaction = new Dictionary<string, string>();
        var newAccount = Tuple.Create(0, 0.00m, "", new List<Dictionary<string, string>>());
        var newBalance = 0.00m;
        var newDate = DateTime.Now;

        do
        {
            Console.Clear();
            Console.WriteLine("Unos nove transakcije:");
            Console.WriteLine("1. Trenutno izvršena transakcija");
            Console.WriteLine("2. Ranije izvršena transakcija");
            Console.WriteLine("0. Natrag");
            Console.WriteLine("Tvoj odabir:");

            if (!int.TryParse(Console.ReadLine(), out var option))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }

            switch (option)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                    transaction = AddTransactionData(transaction);
                    transaction["date"] = DateTime.Now.ToString();
                    newBalance = transaction["type"] == "Income" ? account.Item2 + decimal.Parse(transaction["amount"]) : account.Item2 - decimal.Parse(transaction["amount"]);
                    newAccount = Tuple.Create(account.Item1, newBalance, account.Item3, account.Item4);
                    account = newAccount;
                    account.Item4.Add(transaction);
                    Console.WriteLine("Uspjesno dodana transakcija");
                    Console.ReadLine();
                    break;
                case 2:
                    transaction = AddTransactionData(transaction);

                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Unesite datum transakcije: ");
                        if (!DateTime.TryParse(Console.ReadLine(), out var date)){
                            Console.WriteLine("Krivo unesen datum!");
                            Console.ReadLine();
                            continue;
                        }

                        transaction["date"] = date.ToString();
                        break;
                    } while (true);

                    newBalance = transaction["type"] == "Income" ? account.Item2 + decimal.Parse(transaction["amount"]) : account.Item2 - decimal.Parse(transaction["amount"]);
                    newAccount = Tuple.Create(account.Item1, newBalance, account.Item3, account.Item4);
                    account = newAccount;
                    account.Item4.Add(transaction);
                    Console.WriteLine("Uspjesno dodana transakcija");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }
        } while (!exit);

        return newAccount;
    }

    static Dictionary<string, string> AddTransactionData(Dictionary<string, string> transaction)
    {
        var option = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite tip transakcije:");
            Console.WriteLine("1. Prihod");
            Console.WriteLine("2. Rashod");
            Console.WriteLine("Tvoj odabir:");
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }

            switch (option)
            {
                case 1:
                    transaction["type"] = "Income";
                    break;
                case 2:
                    transaction["type"] = "Expense";
                    break;
                default:
                    Console.WriteLine("Nepoznati tip!");
                    Console.ReadLine();
                    break;
            }
        } while (option != 1 && option != 2);

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite kategoriju transakcije:");

            if (transaction["type"] == "Income")
            {
                Console.WriteLine("1. Plaća");
                Console.WriteLine("2. Honorar");
                Console.WriteLine("3. Poklon");
            }
            else
            {
                Console.WriteLine("1. Hrana");
                Console.WriteLine("2. Prijevoz");
                Console.WriteLine("3. Sport");
            }

            Console.WriteLine("Tvoj odabir:");
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }

            switch (option)
            {
                case 1:
                    transaction["category"] = transaction["type"] == "Income" ? "Salary" : "Food";
                    break;
                case 2:
                    transaction["category"] = transaction["type"] == "Income" ? "Fee" : "Transport";
                    break;
                case 3:
                    transaction["category"] = transaction["type"] == "Income" ? "Gift" : "Sport";
                    break;
                default:
                    Console.WriteLine("Nepoznata kategorija!");
                    Console.ReadLine();
                    break;
            }
        } while (option < 1 || option > 3);

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite opis transakcije:");
            transaction["description"] = Console.ReadLine();

            if (transaction["description"] == string.Empty)
            {
                Console.WriteLine("Niste unijeli opis!");
                Console.ReadLine();
            }
        } while (transaction["description"] == string.Empty);

        var amount = 0.00m;

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite iznos transakcije:");
            if (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0.00m)
            {
                Console.WriteLine("Pogrešno unesen iznos!");
                Console.ReadLine();
                continue;
            }

            transaction["amount"] = amount.ToString();
        } while (amount <= 0.00m);

        return transaction;
    }

    static Tuple<int, decimal, string, List<Dictionary<string, string>>> DeleteTransaction(Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        var exit = false;

        do
        {
            Console.Clear();
            Console.WriteLine("Brisanje transakcije: ");
            Console.WriteLine("1. Po ID-u");
            Console.WriteLine("2. Ispod unesenog iznosa");
            Console.WriteLine("3. Iznad unesenog iznosa");
            Console.WriteLine("4. Sve prihode");
            Console.WriteLine("5. Sve rashode");
            Console.WriteLine("6. Po kategoriji");
            Console.WriteLine("0. Natrag");
            Console.WriteLine("Tvoj odabir: ");

            if (!int.TryParse(Console.ReadLine(), out var option))
            {
                Console.WriteLine("Nepoznata opcija!");
                Console.ReadLine();
                continue;
            }

            switch (option)
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                    account = DeleteTransactionById(account);
                    break;
                case 2:
                    account = DeleteTransactionsBelowAmount(account);
                    break;
                case 3:
                    account = DeleteTransactionsAboveAmount(account);
                    break;
                case 4:
                    account = DeleteIncomeTransactions(account);
                    break;
                case 5:
                    account = DeleteExpenseTransactions(account);
                    break;
                case 6:
                    account = DeleteTransactionsByCategory(account);
                    break;
                default:
                    Console.WriteLine("Nepoznata opcija!");
                    Console.ReadLine();
                    break;
            }
        } while (!exit);

        return account;
    }

    static Tuple<int, decimal, string, List<Dictionary<string, string>>> DeleteTransactionById(Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        var validTransaction = false;
        var transaction = new Dictionary<string, string>();

        do
        {
            var id = InputId();
            transaction = account.Item4.FirstOrDefault(item => item["id"] == id.ToString());

            if (transaction != null && !transaction.Equals(default(Dictionary<string, string>)))
            {
                validTransaction = true;
            }
            else
            {
                Console.WriteLine("Transakcija nije pronađena!");
                Console.ReadLine();
            }
        } while (!validTransaction);

        account.Item4.Remove(transaction);

        return account;
    }

    static Tuple<int, decimal, string, List<Dictionary<string, string>>> DeleteTransactionsBelowAmount(Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        var amount = 0.00m;

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite maksimalan iznos transakcije: ");

            if (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0.00m)
            {
                Console.WriteLine("Krivo unesen iznos!");
                Console.ReadLine();
            }
        } while (amount <= 0.00m);

        foreach (var item in account.Item4.ToList())
        {
            if (decimal.Parse(item["amount"]) < amount)
                account.Item4.Remove(item);
        }

        Console.WriteLine("Uspješno izbrisane transakcije");
        Console.ReadLine();

        return account;
    }

    static Tuple<int, decimal, string, List<Dictionary<string, string>>> DeleteTransactionsAboveAmount(Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        var amount = 0.00m;

        do
        {
            Console.Clear();
            Console.WriteLine("Unesite minimalan iznos transakcije: ");

            if (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0.00m)
            {
                Console.WriteLine("Krivo unesen iznos!");
                Console.ReadLine();
            }
        } while (amount <= 0.00m);

        foreach (var item in account.Item4.ToList())
        {
            if (decimal.Parse(item["amount"]) > amount)
                account.Item4.Remove(item);
        }

        Console.WriteLine("Uspješno izbrisane transakcije");
        Console.ReadLine();

        return account;
    }

    static Tuple<int, decimal, string, List<Dictionary<string, string>>> DeleteIncomeTransactions (Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        foreach (var item in account.Item4.ToList())
        {
            if (item["type"] == "Income")
                account.Item4.Remove(item);
        }

        Console.WriteLine("Uspjesno izbrisane transakcije!");
        Console.ReadLine();

        return account;
    }

    static Tuple<int, decimal, string, List<Dictionary<string, string>>> DeleteExpenseTransactions(Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        foreach (var item in account.Item4.ToList())
        {
            if (item["type"] == "Expense")
                account.Item4.Remove(item);
        }

        Console.WriteLine("Uspjesno izbrisane transakcije!");
        Console.ReadLine();

        return account;
    }

    static Tuple<int, decimal, string, List<Dictionary<string, string>>> DeleteTransactionsByCategory(Tuple<int, decimal, string, List<Dictionary<string, string>>> account)
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Unesite kategoriju transakcije:");
            Console.WriteLine("Salary");
            Console.WriteLine("Fee");
            Console.WriteLine("Gift");
            Console.WriteLine("Food");
            Console.WriteLine("Transport");
            Console.WriteLine("Sport");
            Console.WriteLine("Tvoj odabir:");
            var option = Console.ReadLine();

            if(option != "Salary" && option != "Fee" && option != "Gift" && option != "Food" && option != "Transport" && option != "Sport")
            {
                Console.WriteLine("Nepoznata kategorija!");
                Console.ReadLine();
            }
            else
            {
                foreach (var item in account.Item4.ToList())
                {
                    if (item["category"] == option)
                        account.Item4.Remove(item);
                }

                break;
            }

        } while (true);

        Console.WriteLine("Uspjesno izbrisane transakcije!");
        Console.ReadLine();

        return account;
    }
}

