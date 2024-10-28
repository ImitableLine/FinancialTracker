using FinancialTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialTracker.Models
{
    [Table("transactions")] 
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int accountid { get; set; }
        public int userid { get; set; }
        public string transactiontype { get; set; }
        public decimal amount { get; set; }
        public DateTime date { get; set; }
        public string category { get; set; }
        public string? description { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        [ForeignKey("accountid")]
        public virtual Account account { get; set; }
        [ForeignKey("userid")]
        public virtual User user { get; set; }
    }
}
