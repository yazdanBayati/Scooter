using ApplicationService;
using ApplicationService.Dtos;
using ApplicationService.Services;
using Core.DataAccess;
using Core.Domains;
using DataAccess.Fake.repositories;
using Infra;
using Infra.ExceptionTypes;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    public class CalculateIncomeTest
    {
        private IRentScooterService service;
        private IScooterRepositroy scooterRepository;
        private ICompanyRepositroy companyRepositroy;

        private List<Scooter> scooterInitData;
        private List<CompanyScooter> companyInitData;


        [SetUp]
        public void Setup()
        {
            this.Seed();
            this.companyRepositroy = new CompanyRepositroyFake(companyInitData);
            this.scooterRepository = new ScooterRepositoryFake(scooterInitData);
            this.service = new RentScooterService(companyRepositroy, scooterRepository);
        }

        private void Seed()
        {
            this.scooterInitData = new List<Scooter>()
            {
               new Scooter("1", 0.3m),
               new Scooter("2", 0.5m),
               new Scooter("3", 1m)
            };

            this.companyInitData = new List<CompanyScooter>()
            {
                new CompanyScooter()
                {
                    Id = NumberUtil.GetNextNumber(),
                    CompanyName = "test1",
                    StartDate = DateTime.Now.AddHours(-1),
                    EndDate = DateTime.Now,
                    ScooterId = "1",
                    PricePerMinute = 0.3m
                },
                new CompanyScooter()
                {
                    Id = NumberUtil.GetNextNumber(),
                    CompanyName = "test1",
                    StartDate = DateTime.Now.AddHours(-2),
                    EndDate = DateTime.Now,
                    ScooterId = "2",
                    PricePerMinute = 0.5m
                },
                new CompanyScooter()
                {
                    Id = NumberUtil.GetNextNumber(),
                    CompanyName = "test1",
                    StartDate = DateTime.Now.AddHours(-2),
                    EndDate = null,
                    ScooterId = "2",
                    PricePerMinute = 0.5m
                },
                 new CompanyScooter()
                {
                    Id = NumberUtil.GetNextNumber(),
                    CompanyName = "test1",
                    StartDate = DateTime.Now.AddYears(-1).AddHours(-1),
                    EndDate = DateTime.Now.AddYears(-1),
                    ScooterId = "2",
                    PricePerMinute = 0.5m
                }
            };
        }

        [Test]
        public void CalculateIncome_all()
        {
            var selectedComapnyScooter = companyInitData.First();
            var calcIncomReuquest = new CalculateIncomeRequest()
            {
                CompanyName= selectedComapnyScooter.CompanyName,
                InculdeNotCompletedRentals=false
            };
            decimal amount  = service.CalculateIncome(calcIncomReuquest);
            Assert.AreEqual(amount, 108m);
        }

        [Test]
        public void CalculateIncome_allIncludeNotCompeleted()
        {
            var selectedComapnyScooter = companyInitData.First();
            var calcIncomReuquest = new CalculateIncomeRequest()
            {
                CompanyName = selectedComapnyScooter.CompanyName,
                InculdeNotCompletedRentals = true
            };
            decimal amount = service.CalculateIncome(calcIncomReuquest);
            Assert.AreEqual(amount, 168m);
        }
        [Test]
        public void CalculateIncome_filterByYear()
        {
            var selectedComapnyScooter = companyInitData.First();
            var calcIncomReuquest = new CalculateIncomeRequest()
            {
                CompanyName = selectedComapnyScooter.CompanyName,
                InculdeNotCompletedRentals = false,
                Year=2020
            };
            decimal amount = service.CalculateIncome(calcIncomReuquest);
            Assert.AreEqual(amount, 30m);
        }
        [Test]
        public void CalculateIncome_filterByYearIncludeNotCompleted()
        {
            var selectedComapnyScooter = companyInitData.First();
            var calcIncomReuquest = new CalculateIncomeRequest()
            {
                CompanyName = selectedComapnyScooter.CompanyName,
                InculdeNotCompletedRentals = true,
                Year = 2021
            };
            decimal amount = service.CalculateIncome(calcIncomReuquest);
            Assert.AreEqual(amount, 138m);
        }
    }
}