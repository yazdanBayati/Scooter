using Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Dtos
{
    public class ScooterDto
    {
       public string Id { get; set; }
       public decimal PricePerMinute { get; set; }

       public void Parse(Scooter scooter)
       {
            Id = scooter.Id;
            PricePerMinute = scooter.PricePerMinute;
       }
    }
}
