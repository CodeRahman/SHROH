using SQLite;

namespace SHROH.Models
{
    public class SettingsModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Currency { get; set; } = "USD";
        public string Theme { get; set; } = "Light";
    }
}
