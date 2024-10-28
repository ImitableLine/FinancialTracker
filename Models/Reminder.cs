using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialTracker.Models
{
    [Table("reminders")]
    public class Reminder
    {
        [Key]
        public int id { get; set; }
        public int userid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime duedate { get; set; }
        public bool iscompleted { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        // Navigation property
        public virtual User user { get; set; }
    }
}
