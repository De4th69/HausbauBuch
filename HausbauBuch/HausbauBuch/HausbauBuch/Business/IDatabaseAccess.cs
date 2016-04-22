using SQLite;

namespace HausbauBuch.Business
{
    public interface IDatabaseAccess
    {
        SQLiteAsyncConnection GetConnection();
    }
}
