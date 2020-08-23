using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WestchesterApi.Models
{
    public class DBEgg
    {
        [Key]
        public long id { get; set; }
        public long distance { get; set; }
        public string name { get; set; }
        public string eventName { get; set; }
        public long hatched { get; set; }
        public long shiny { get; set; }
    }
}
