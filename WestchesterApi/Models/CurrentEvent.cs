using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WestchesterApi.Models
{
    public class CurrentEvent
    {
        [Key]
        public long ID { get; set; }
        public string raids { get; set; }
        public string eggs2 { get; set; }
        public string eggs5 { get; set; }
        public string eggs7 { get; set; }
        public string eggs10 { get; set; }
    }
}
