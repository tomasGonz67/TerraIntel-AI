using SQLite;

public class InfoItemDatabase
{
    SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await database.CreateTableAsync<InfoItem>();
    }

    public async Task<int> AddInfoItemAsync(string name)
    {
        await Init();
        var newInfoItem = new InfoItem
        {
            Name = name,
        };

        return await database.InsertAsync(newInfoItem);
    }
    public async Task<List<InfoItem>> GetItemsAsync()
    {
        await Init();
        return await database.Table<InfoItem>().ToListAsync();
    }


    public async Task<InfoItem> GetItemAsync(int id)
    {
        await Init();
        return await database.Table<InfoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(InfoItem item)
    {
        await Init();
        if (item.ID != 0)
            return await database.UpdateAsync(item);
        else
            return await database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(InfoItem item)
    {
        await Init();
        return await database.DeleteAsync(item);
    }
}