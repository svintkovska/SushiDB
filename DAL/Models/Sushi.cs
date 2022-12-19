using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("tblSushi")]
    public class Sushi
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string Description { get; set; }
        [Required]
        public string MainPhoto { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
