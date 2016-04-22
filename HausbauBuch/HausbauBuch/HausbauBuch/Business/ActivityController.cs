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
    public class ActivitiesController : EntityController<Activities>
    {
        public ActivitiesController() : base(App.SqlConnection)
        {
            App.SqlConnection.CreateTableAsync<Activities>();
        } 
    }
}
