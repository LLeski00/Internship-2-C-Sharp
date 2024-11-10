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
                break;
            case 2:
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
    var validUser = false;

    do
    {
        Console.Clear();
        Console.WriteLine("Unesite ime i prezime korisnika: ");
        var user = Console.ReadLine();
        //find user
        //if user is valid =>
        validUser = true;
    } while (!validUser);

    do
    {
        DisplayAccounts(/*user*/);
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

static void DisplayAccounts(/*user*/)
{
    Console.Clear();
    Console.WriteLine("1. Tekući račun");
    Console.WriteLine("2. Žiro račun");
    Console.WriteLine("3. Web prepaid račun");
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
