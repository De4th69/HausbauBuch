using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausbauBuch.Helper
{
    public class RemindMeValues
    {
        public Dictionary<string, int> RemindMe => new Dictionary<string, int>
        {
            {"Keine", 0},
            {"Fünf Minuten", 5},
            {"Zehn Minuten", 10},
            {"Fünfzehn Minuten", 15},
            {"Halbe Stunde", 30},
            {"1 Stunde", 60},
            {"2 Stunden", 120},
            {"3 Stunden", 180},
            {"4 Stunden", 240},
            {"5 Stunden", 300},
            {"6 Stunden", 360},
            {"8 Stunden", 480},
            {"12 Stunden", 720},
            {"18 Stunden", 1080},
            {"1 Tag" ,1440}
        };
    }
}
