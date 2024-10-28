using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialTracker.Models
{
    [Table("budgets")]
    public class Budget
    {
        [Key]
        public int id { get; set; }
        public int userid { get; set; }
        public string category { get; set; }
        public decimal amount { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        // Navigation property
        public virtual User user { get; set; }
    }
}
