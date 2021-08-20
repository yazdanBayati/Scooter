using ApplicationService;
using ApplicationService.Services;
using Core.DataAccess;
using Core.Domains;
using DataAccess.Fake.repositories;
using Infra.ExceptionTypes;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    public class StartRentTest
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
               new Scooter("1", 2.5m),
               new Scooter("2", 3m)
            };

            this.companyInitData = new List<CompanyScooter>()
            {
                new CompanyScooter()
                {
                    CompanyName = "test1",
                    StartDate = DateTime.Now,
                    EndDate = null,
                    ScooterId = "1"
                }
            };
        }

        [Test]
        public void StartRent()
        {
            var selectedScooter = scooterInitData.First(x => x.PricePerMinute == 3m);
            var request = new StartRentScooterRequest();
            request.CompanyName = "test1";
            request.ScooterId = selectedScooter.Id;
            service.StartRent(request);
            var scooter = scooterRepository.GetById(request.ScooterId);
            Assert.AreEqual(scooter.IsRented, true);
        }

        [Test]
        public void StartRent_ScooterNotAvilable()
        {
            var selectedScooter = scooterInitData.First(x => x.PricePerMinute == 3m);
            scooterRepository.ChangeScooterState(selectedScooter.Id, true);
            var request = new StartRentScooterRequest();
            request.CompanyName = "test1";
            request.ScooterId = selectedScooter.Id;
            Assert.Throws<ScooterIsNotAvailableException>(() => service.StartRent(request));
        }

        [Test]
        public void StartRent_ScooterNotExist()
        {
            var request = new StartRentScooterRequest();
            request.CompanyName = "test1";
            request.ScooterId = "2000";
            Assert.Throws<ScootersNotExistException>(() => service.StartRent(request));
        }
    }
}