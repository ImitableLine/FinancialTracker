using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialTracker.Models
{
    [Table("users")] 
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string passwordhash { get; set; }

        [Required]
        public string email { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        // Navigation properties
        public virtual ICollection<Account> accounts { get; set; }
        public virtual ICollection<Budget> budgets { get; set; }
        public virtual ICollection<Reminder> reminders { get; set; }
        public virtual ICollection<Transaction> transactions { get; set; }
    }
}
