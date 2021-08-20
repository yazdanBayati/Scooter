using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains
{
    public class CompanyScooter
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ScooterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal PricePerMinute { get; set; }
    }
}
