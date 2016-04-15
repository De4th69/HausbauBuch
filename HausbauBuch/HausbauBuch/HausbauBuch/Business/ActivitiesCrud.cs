using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Classes;
using SQLite;
using Xamarin.Forms;

namespace HausbauBuch.Business
{
    public class ActivitiesCrud
    {
        static readonly object Locker = new object();

        private readonly SQLiteConnection _sqlLiteConnection;
        public ActivitiesCrud()
        {
            _sqlLiteConnection = DependencyService.Get<IDatabaseAccess>().GetConnection();
            _sqlLiteConnection.CreateTable<Activities>();
        }

        public IEnumerable<Activities> GetActivities()
        {
            lock (Locker)
            {
                return (from a in _sqlLiteConnection.Table<Activities>() select a).ToList();
            }
        }

        public IEnumerable<Activities> GetUnFinishedActivites()
        {
            lock (Locker)
            {
                return _sqlLiteConnection.Query<Activities>("SELECT * FROM [Activities] WHERE [Finished] = 0 AND [Deleted] = 0");
            }
        }

        public Activities GetActivity(Guid id)
        {
            lock (Locker)
            {
                return _sqlLiteConnection.Table<Activities>().FirstOrDefault(a => a.ActivitesId == id);
            }
        }

        public Guid SaveActivity(Activities activity)
        {
            lock (Locker)
            {
                if (activity.ActivitesId != Guid.Empty)
                {
                    _sqlLiteConnection.Update(activity);
                    return activity.ActivitesId;
                }
                else
                {
                    _sqlLiteConnection.Insert(activity);
                    return activity.ActivitesId;
                }
            }
        }

        public void DeleteActivity(Activities activity)
        {
            lock (Locker)
            {
                activity.Deleted = true;
                _sqlLiteConnection.Update(activity);
            }
        }
    }
}
