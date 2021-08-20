using ApplicationService;
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
    public class EndRentScooterTest
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
                    EndDate = null,
                    ScooterId = "1",
                    PricePerMinute = 0.3m
                },
                new CompanyScooter()
                {
                    Id = NumberUtil.GetNextNumber(),
                    CompanyName = "test1",
                    StartDate = DateTime.Now.AddHours(-1),
                    EndDate = null,
                    ScooterId = "2",
                    PricePerMinute = 0.5m
                }
            };
        }

        [Test]
        public void EndRent()
        {
            var selectedComapnyScooter = companyInitData.First(x => x.ScooterId == "1");
            scooterRepository.ChangeScooterState(selectedComapnyScooter.ScooterId, true);
            var amount  = service.EndRent(selectedComapnyScooter.ScooterId);
            var scooter = scooterRepository.GetById(selectedComapnyScooter.ScooterId);
            Assert.AreEqual(amount, 18m);
            Assert.AreEqual(scooter.IsRented, false);
        }

        [Test]
        public void EndRent_MoreThan20()
        {
            var selectedComapnyScooter = companyInitData.First(x => x.ScooterId == "2");
            scooterRepository.ChangeScooterState(selectedComapnyScooter.ScooterId, true);
            var amount = service.EndRent(selectedComapnyScooter.ScooterId);
            var expectedDate = DateTime.Now.GetNextDay();
            var updatedCompanyScooter = companyRepositroy.GetCompanyScooterById(selectedComapnyScooter.Id);
            var scooter = scooterRepository.GetById(selectedComapnyScooter.ScooterId);
            Assert.AreEqual(amount, 30m);
            Assert.AreEqual(expectedDate, updatedCompanyScooter.EndDate);
            Assert.AreEqual(scooter.IsRented, false);
        }

        [Test]
        public void StartRent_RentOutRecordNotExist()
        {
            Assert.Throws<RentedScootersNotExistException>(() => service.EndRent("3"));
        }
    }
}