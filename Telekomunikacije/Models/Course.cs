using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telekomunikacije.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name ="Ime Kursa")]
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
                
        [Display(Name ="Lozinka")]
        [Required(ErrorMessage ="Molimo Vas da Unesete Lozinku")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Godina Studija")]
        [Required]
        public byte Year { get; set; }

        [Display(Name = "Broj ESPB Bodova")]
        [Required]
        public byte ESPB { get; set; }

       

        public List<int> ProffesorIds { get; set; }
    }
}