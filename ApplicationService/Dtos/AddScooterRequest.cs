using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Dtos
{
    public class AddScooterRequest
    {
        public string Id { get; set; }
        public decimal PricePerMinute { get; set; }
    }
}
