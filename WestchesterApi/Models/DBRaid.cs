using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WestchesterApi.Models
{
    public class DBRaid
    {
        [Key]
        public long raidID { get; set; }
        public long stars { get; set; }
        public string pokemon { get; set; }
        public string eventName { get; set; }
        public long total { get; set; }
        public long caught { get; set; }
        public long shiny { get; set; }
    }
}
