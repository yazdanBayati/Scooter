using Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains
{
    public class Company
    {
        public Company(string name)
        {
            this.Name = name;
        }
        
        /// <summary>
        /// Name of the company.
        /// </summary>
        public string Name { get; private set; }

        
        
        /// <summary>
        /// Start Rent
        /// </summary>
        /// <param name="scooter"></param>
        /// <returns></returns>
        public CompanyScooter StartRent(Scooter scooter)
        {
            scooter.CheckAvliablity();
            var compnayScooter = new CompanyScooter();
            compnayScooter.StartDate = DateTime.Now;
            compnayScooter.ScooterId = scooter.Id;
            compnayScooter.CompanyName = this.Name;
            return compnayScooter;
        }

        /// <summary>
        /// End the rent of the scooter.
        /// </summary>
        /// <param name="scooterComapny"></param>
        // <returns>The total price of rental. It has to calculated taking into account for how long time scooter was rented.
        /// If total amount per day reaches 20 EUR than timer must be stopped and restarted at beginning of next day at 0:00 am.</returns>
        public decimal EndRent(CompanyScooter scooterComapny)
        {
            var scooter = new Scooter(scooterComapny.ScooterId, scooterComapny.PricePerMinute);
            scooter.IsRented = true;
            decimal amount = scooter.CalaculateRent(scooterComapny.StartDate, DateTime.Now);

            scooterComapny.EndDate = DateTime.Now;
            if(amount > 20)
            {
                scooterComapny.EndDate = DateTime.Now.GetNextDay();
            }

            return amount;
        }

        /// <summary>
        /// Income report.
        /// </summary>
        /// <param name="companyScooters"></param>
        /// <returns>The total price of all rentals </returns>

        public  decimal CalculateIncome(List<CompanyScooter> companyScooters)
        {
            var amount = 0m;
            foreach (var companyScooter in companyScooters) 
            {
                var scooter = new Scooter(companyScooter.ScooterId, companyScooter.PricePerMinute);
                if(companyScooter.EndDate == null)
                {
                    companyScooter.EndDate = DateTime.Now;
                }
                amount += scooter.CalaculateRent(companyScooter.StartDate, companyScooter.EndDate.Value);
            }
            return amount;
        }

        /// <summary>
        /// Start the rent of the scooter.
        /// </summary>
        /// <param name="id">ID of the scooter.</param>
        //void StartRent(string id);
        ///// <summary>
        ///// End the rent of the scooter.
        ///// </summary>
        ///// <param name="id">ID of the scooter.</param>
        ///// <returns>The total price of rental. It has to calculated taking into account for how long time scooter was rented.
        ///// If total amount per day reaches 20 EUR than timer must be stopped and restarted at beginning of next day at 0:00 am.</returns>
        //decimal EndRent(string id);
        ///// <summary>
        ///// Income report.
        ///// </summary>
        ///// <param name="year">Year of the report. Sum all years if value is not set.</param>
        ///// <param name="includeNotCompletedRentals">Include income from the scooters that are rented out (rental has not ended yet) and
        ////calculate rental
        ///// price as if the rental would end at the time when this report was requested.</param>
        ///// <returns>The total price of all rentals filtered by year if given.</returns>
        //decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
    }
}
