using Core.DataAccess;
using Infra;
using Infra.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domains
{
    public class Scooter
    {
        public Scooter(string id, decimal pricePerMinute)
        {
            Id = id;
            PricePerMinute = pricePerMinute;
        }
        

        /// <summary>
        /// Unique ID of the scooter.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Rental price of the scooter per one
        ///minute.
        /// </summary>
        public decimal PricePerMinute { get; private set; }
        /// <summary>
        /// Identify if someone is renting this
        ///scooter.
         /// </summary>
       public bool IsRented { get;set; }

        /// <summary>
        /// Check scooter is avliable
        /// </summary>
        public void CheckAvliablity()
        {
            if (this.IsRented)
            {
                throw new ScooterIsNotAvailableException();
            }
        }

        /// <summary>
        /// check Id format is valid or not 
        /// </summary>
        public void CheckIdValidity()
        {
            // todo:check for length of Id and other necessary rules
        }

        public decimal CalaculateRent(DateTime startDate, DateTime endDate)
        {
            var diffMintues = startDate.CalcDiffInMinutes(endDate);

            return this.PricePerMinute * Convert.ToDecimal(diffMintues);
        }
    }
}
