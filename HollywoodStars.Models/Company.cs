using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollywoodStars.Models
{
    public class Company
    {
        [Key]
        [DisplayName("Company Id")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public DateTime CreationDate { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
