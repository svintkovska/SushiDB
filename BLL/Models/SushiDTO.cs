using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SushiDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainPhoto { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public int Price { get; set; }
    }
}
