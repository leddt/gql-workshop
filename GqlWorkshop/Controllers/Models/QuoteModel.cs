using System.ComponentModel.DataAnnotations;

namespace GqlWorkshop.Controllers.Models
{
    public class QuoteModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string SaidBy { get; set; }
    }
}