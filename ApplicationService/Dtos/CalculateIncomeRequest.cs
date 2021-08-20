using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Dtos
{
    public class CalculateIncomeRequest
    {
        public string CompanyName { get; set; }
        public int? Year { get; set; }
        public bool InculdeNotCompletedRentals { get; set; }
    }
}
