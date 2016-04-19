using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HausbauBuch.Classes
{
    public class Activities
    {
        [PrimaryKey]
        public Guid ActivitesId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool Finished { get; set; }

        public bool IsCheckList { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime ModifiedAt { get; set; }
        
        public DateTime FinishedOn { get; set; }

        public bool Deleted { get; set; }
    }
}
