using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollywoodStars.Models
{
    public class Movie
    {

        [Key]
        [DisplayName("Movie Id")]

        public int MovieId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [DisplayName("Release Year")]

        public int ReleaseYear { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public List<Company> Companies { get; set; } = new List<Company>();
    }
}
