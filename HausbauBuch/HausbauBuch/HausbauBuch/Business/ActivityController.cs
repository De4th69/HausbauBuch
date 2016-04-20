using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausbauBuch.Classes;
using HausbauBuch.Views;
using SQLite;
using Xamarin.Forms;

namespace HausbauBuch.Business
{
    public class ActivitiesController
    {
        static readonly object Locker = new object();
        
        public ActivitiesController()
        {

            #if DEBUG
            App.SqlConnection.DropTable<Activities>();
            #endif
            App.SqlConnection.CreateTable<Activities>();
        }

        public IEnumerable<Activities> GetActivities()
        {
            lock (Locker)
            {
                return (from a in App.SqlConnection.Table<Activities>() select a).ToList().Where(a => !a.Deleted);
            }
        }

        public IEnumerable<Activities> GetUnFinishedActivites()
        {
            lock (Locker)
            {
                return App.SqlConnection.Query<Activities>("SELECT * FROM [Activities] WHERE [Finished] = 0 AND [Deleted] = 0");
            }
        }

        public IEnumerable<Activities> GetCheckListActivities()
        {
            lock (Locker)
            {
                return App.SqlConnection.Query<Activities>("SELECT * FROM [Activities] WHERE [Finished] = 0 AND [Deleted] = 0 AND [IsCheckList] = 1");
            }
        } 

        public Activities GetActivity(Guid id)
        {
            lock (Locker)
            {
                return App.SqlConnection.Table<Activities>().FirstOrDefault(a => a.Id == id);
            }
        }

        public Guid SaveActivity(Activities activity)
        {
            lock (Locker)
            {
                if (activity.Id != Guid.Empty)
                {
                    activity.ModifiedAt = DateTime.Now;
                    App.SqlConnection.Update(activity);
                    return activity.Id;
                }
                else
                {
                    activity.CreatedAt = DateTime.Now;
                    activity.ModifiedAt = DateTime.Now;
                    App.SqlConnection.Insert(activity);
                    return activity.Id;
                }
            }
        }

        public void DeleteActivity(Activities activity)
        {
            lock (Locker)
            {
                activity.Deleted = true;
                App.SqlConnection.Update(activity);
            }
        }
    }
}
