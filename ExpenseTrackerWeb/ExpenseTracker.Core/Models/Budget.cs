using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Models
{
    public class Budget
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> Users { get; set; }
    }
}
