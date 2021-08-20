using ApplicationService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Services
{
    public interface IRentScooterService
    {
        public void StartRent(StartRentScooterRequest request);
        decimal EndRent(string id);
        decimal CalculateIncome(CalculateIncomeRequest calcIncomReuquest);
    }
}
