using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HausbauBuch.Business;
using HausbauBuch.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseAccess))]

namespace HausbauBuch.Droid
{
    public class DatabaseAccess : IDatabaseAccess
    {
        public DatabaseAccess()
        {
            
        }

        public SQLiteAsyncConnection GetConnection()
        {
            var sqlDbFileName = "Hausbau.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentsPath, sqlDbFileName);

            var connection = new SQLiteAsyncConnection(path);

            return connection;
        }
    }
}