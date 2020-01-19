using System;
using SQLite;
namespace HC
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
