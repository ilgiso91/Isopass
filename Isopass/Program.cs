using Isopass.Models;
using Isopass.Services;
using System.Text;

static void PrintBanner()
{
    Console.Clear();
    Console.WriteLine("====================================");
    Console.WriteLine("           ISOPASS v1.0             ");
    Console.WriteLine("   Simple Encrypted Password Vault  ");
    Console.WriteLine("====================================\n");
}

static string Mask(string value)
{
    if (string.IsNullOrEmpty(value))
        return "";
    return new string('*', value.Length);
}

static bool ValidateMasterPassword(string master)
{
    string masterFile = "master.hash";

    if (!File.Exists(masterFile))
    {
        // İlk kez çalıştırılıyorsa master kaydedilir
        string hash = EncryptionService.HashMaster(master);
        File.WriteAllText(masterFile, hash);
        return true;
    }
    else
    {
        string storedHash = File.ReadAllText(masterFile);
        string currentHash = EncryptionService.HashMaster(master);

        if (storedHash == currentHash)
            return true;

        Console.WriteLine("Wrong master password. Access denied.");
        return false;
    }
}

PrintBanner();
Console.Write("Master password: ");
string master = Console.ReadLine();

if (!ValidateMasterPassword(master))
{
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
    return;
}

var encryption = new EncryptionService(master);
var manager = new PasswordManager(encryption);

while (true)
{
    PrintBanner();
    Console.WriteLine("1) Add Password");
    Console.WriteLine("2) View Passwords");
    Console.WriteLine("3) Delete Password");
    Console.WriteLine("4) Update Password");
    Console.WriteLine("5) Search");
    Console.WriteLine("6) Exit");
    Console.Write("\nChoice: ");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Write("\nSite: ");
        string site = Console.ReadLine();

        Console.Write("Username: ");
        string user = Console.ReadLine();

        Console.Write("Password: ");
        string pass = Console.ReadLine();

        manager.AddPassword(new PasswordEntry
        {
            Site = site,
            Username = user,
            Password = pass
        });

        Console.WriteLine("\nSaved!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    else if (choice == "2")
    {
        var list = manager.GetAllPasswords();

        if (list.Count == 0)
        {
            Console.WriteLine("\nNo passwords found.");
        }
        else
        {
            Console.WriteLine();
            foreach (var item in list)
            {
                Console.WriteLine($"[{item.Id}] {item.Site} | {item.Username} | {Mask(item.Password)}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    else if (choice == "3")
    {
        Console.Write("\nID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
        }
        else
        {
            if (manager.DeletePassword(id))
                Console.WriteLine("Deleted.");
            else
                Console.WriteLine("Not found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    else if (choice == "4")
    {
        Console.Write("\nID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
        }
        else
        {
            Console.Write("New Site: ");
            string site = Console.ReadLine();

            Console.Write("New Username: ");
            string user = Console.ReadLine();

            Console.Write("New Password: ");
            string pass = Console.ReadLine();

            if (manager.UpdatePassword(id, site, user, pass))
                Console.WriteLine("Updated.");
            else
                Console.WriteLine("Not found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    else if (choice == "5")
    {
        Console.Write("\nSearch keyword: ");
        string keyword = Console.ReadLine();

        var results = manager.Search(keyword);

        if (results.Count == 0)
        {
            Console.WriteLine("\nNo results found.");
        }
        else
        {
            Console.WriteLine();
            foreach (var item in results)
            {
                Console.WriteLine($"[{item.Id}] {item.Site} | {item.Username} | {Mask(item.Password)}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    else if (choice == "6")
    {
        break;
    }
    else
    {
        Console.WriteLine("\nInvalid choice.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}