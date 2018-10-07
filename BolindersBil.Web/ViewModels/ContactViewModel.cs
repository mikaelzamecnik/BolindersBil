using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Du måste skriva in ditt Namn!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Fyll i korrekt e-post!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Fyll i fältet Title!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Skriv ditt meddelande!")]
        public string Message { get; set; }
        public string DealershipEmail { get; set; }

    }
}