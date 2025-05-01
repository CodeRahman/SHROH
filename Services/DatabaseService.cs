using SQLite;
using SHROH.Models;

namespace SHROH.Services
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _database;
        private static readonly Lazy<DatabaseService> _instance = new(() => new DatabaseService());

        public static DatabaseService Instance => _instance.Value;

        private DatabaseService() { }

        public async Task Init()
        {
            if (_database is not null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "finance.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<TransactionModel>();
            await _database.CreateTableAsync<BudgetModel>();
            await _database.CreateTableAsync<SettingsModel>();
        }

        // Transaction Methods
        public Task<List<TransactionModel>> GetTransactionsAsync() => _database.Table<TransactionModel>().ToListAsync();
        public Task<int> SaveTransactionAsync(TransactionModel item)
        {
            if (item.Id != 0)
                return _database.UpdateAsync(item); 
            else
                return _database.InsertAsync(item); 
        }
        public Task<int> DeleteTransactionAsync(TransactionModel item) => _database.DeleteAsync(item);

        // Budget Methods
        public Task<List<BudgetModel>> GetBudgetsAsync() => _database.Table<BudgetModel>().ToListAsync();
        public async Task<int> SaveBudgetAsync(BudgetModel item)
        {
            var existing = await _database.Table<BudgetModel>()
                                          .Where(b => b.Category == item.Category)
                                          .FirstOrDefaultAsync();

            if (existing != null)
            {
                existing.Limit = item.Limit;
                return await _database.UpdateAsync(existing);
            }
            else
            {
                return await _database.InsertAsync(item);
            }
        }


        
        public Task<List<SettingsModel>> GetSettingsAsync()
        {
            return _database.Table<SettingsModel>().ToListAsync();
        }

        public Task<int> SaveSettingsAsync(SettingsModel item)
        {
            return _database.InsertOrReplaceAsync(item);
        }

    }
}
