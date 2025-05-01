using SQLite;

namespace SHROH.Models
{
    public class BudgetModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Category { get; set; }
        public double Limit { get; set; }
    }
}
