using ApplicationService.Dtos;
using Core.DataAccess;
using Core.Domains;
using Infra.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Services
{
    public class RentScooterService : IRentScooterService
    {
        private readonly ICompanyRepositroy _companyRepository;
        private readonly IScooterRepositroy _scooterRepositroy;
        
        public RentScooterService(ICompanyRepositroy companyRepositroy, IScooterRepositroy scooterRepositroy)
        {
            this._companyRepository = companyRepositroy;
            this._scooterRepositroy = scooterRepositroy;
        }

       

        /// <summary>
        /// Start Rent Service
        /// </summary>
        /// <param name="request"></param>
        public void StartRent(StartRentScooterRequest request)
        {
            Scooter scooter = LoadScooter(request.ScooterId);
            scooter.CheckAvliablity();
            var compnay = new Company(request.CompanyName);
            var companyScooter = compnay.StartRent(scooter);
            this._companyRepository.AddCompanyScooter(companyScooter);
            this._scooterRepositroy.ChangeScooterState(scooter.Id, true);
        }


        /// <summary>
        /// End the rent of the scooter.
        /// </summary>
        /// <param name="id">ID of the scooter.</param>
        /// <returns>The total price of rental. It has to calculated taking into account for how long time scooter was rented.
        /// If total amount per day reaches 20 EUR than timer must be stopped and restarted at beginning of next day at 0:00 am.</returns>
        public decimal EndRent(string scooterId)
        {
            CompanyScooter scooterComapny = LoadScooterCompany(scooterId);
            var company = new Company(scooterComapny.CompanyName);
            var amount = company.EndRent(scooterComapny);
            _companyRepository.UpdateScooterCompany(scooterComapny);
            _scooterRepositroy.ChangeScooterState(scooterId, false);
            return amount;
        }

        private CompanyScooter LoadScooterCompany(string scooterId)
        {
            var scooterComapny = _companyRepository.GetCompanyScooterByScooterId(scooterId);
            if (scooterComapny == null)
            {
                throw new RentedScootersNotExistException();
            }

            return scooterComapny;
        }

        /// <summary>
        /// Load Scooter by id and check  scooter Exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Scooter LoadScooter(string id)
        {
            var scooter = _scooterRepositroy.GetById(id);
            if (scooter == null)
            {
                throw new ScootersNotExistException();
            }

            return scooter;
        }

        /// <summary>
        /// Income report.
        /// </summary>
        /// <param name="calcIncomReuquest.CompanyName">Name of Company
        /// <param name="calcIncomReuquest.Year">Year of the report. Sum all years if value is not set.</param>
        /// <param name="calcIncomReuquest.IncludeNotCompletedRentals">Include income from the scooters that are rented out (rental has not ended yet) and
        ///calculate rental
        /// price as if the rental would end at the time when this report was requested.</param>
        /// <returns>The total price of all rentals filtered by year if given.</returns>

        public decimal CalculateIncome(CalculateIncomeRequest calcIncomReuquest)
        {
           var companyScooters =  this._companyRepository.GetCompanyRentedScooterList(calcIncomReuquest.CompanyName, calcIncomReuquest.Year, calcIncomReuquest.InculdeNotCompletedRentals);
           var comapny = new Company(calcIncomReuquest.CompanyName);
           var amount = comapny.CalculateIncome(companyScooters);
           return amount;
        }
    }
}
