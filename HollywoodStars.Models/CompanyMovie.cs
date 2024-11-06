using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HollywoodStars.Models
{

    [PrimaryKey(nameof(CompanyId), nameof(MovieId))]
    public class CompanyMovie
    {
        public int CompanyId { get; set; }

        public int MovieId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
   
}
