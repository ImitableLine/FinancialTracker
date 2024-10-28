namespace FinancialTracker.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("accounts")]
public class Account
{
    [Key]
    public int id { get; set; }
    public int userid { get; set; }
    public string accountname { get; set; }
    public string accounttype { get; set; }
    public decimal balance { get; set; }
    public DateTime createdat { get; set; }
    public DateTime updatedat { get; set; }

    // Navigation property
    public virtual User user { get; set; }
    public virtual ICollection<Transaction> transactions { get; set; } = new List<Transaction>();
}
