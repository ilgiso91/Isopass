using System.Text.Json;
using Isopass.Models;

namespace Isopass.Services
{
    public class PasswordManager
    {
        private readonly string _filePath = "passwords.json";
        private readonly EncryptionService _encryption;

        public PasswordManager(EncryptionService encryption)
        {
            _encryption = encryption;
        }

        private List<PasswordEntry> Load()
        {
            if (!File.Exists(_filePath))
                return new List<PasswordEntry>();

            string json = File.ReadAllText(_filePath);
            var list = JsonSerializer.Deserialize<List<PasswordEntry>>(json);

            foreach (var item in list)
                item.Password = _encryption.Decrypt(item.Password);

            return list;
        }

        private void Save(List<PasswordEntry> list)
        {
            var encryptedList = list.Select(x => new PasswordEntry
            {
                Id = x.Id,
                Site = x.Site,
                Username = x.Username,
                Password = _encryption.Encrypt(x.Password)
            }).ToList();

            File.WriteAllText(_filePath, JsonSerializer.Serialize(encryptedList));
        }

        public void AddPassword(PasswordEntry entry)
        {
            var list = Load();

            entry.Id = list.Count == 0 ? 1 : list.Max(x => x.Id) + 1;
            list.Add(entry);

            Save(list);
        }

        public List<PasswordEntry> GetAllPasswords()
        {
            return Load();
        }

        public bool DeletePassword(int id)
        {
            var list = Load();
            var item = list.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return false;

            list.Remove(item);
            Save(list);
            return true;
        }

        public bool UpdatePassword(int id, string site, string username, string password)
        {
            var list = Load();
            var item = list.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return false;

            item.Site = site;
            item.Username = username;
            item.Password = password;

            Save(list);
            return true;
        }

        public List<PasswordEntry> Search(string keyword)
        {
            var list = Load();
            return list.Where(x =>
                x.Site.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                x.Username.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}