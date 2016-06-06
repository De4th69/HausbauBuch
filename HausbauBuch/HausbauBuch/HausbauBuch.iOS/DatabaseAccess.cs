using System;
using System.Collections.Generic;
using System.Text;
using HausbauBuch.Business;
using HausbauBuch.iOS;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseAccess))]

namespace HausbauBuch.iOS
{
    public class DatabaseAccess : IDatabaseAccess
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var sqlDbFileName = "Hausbau.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentsPath, sqlDbFileName);

            var connection = new SQLiteAsyncConnection(path);

            return connection;
        }
    }
}
