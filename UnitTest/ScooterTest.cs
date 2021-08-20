using ApplicationService.Dtos;
using ApplicationService.Services;
using Core.DataAccess;
using Core.Domains;
using DataAccess.Fake.repositories;
using Infra.ExceptionTypes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest
{
   

   
    public class ScooterTest
    {
        private IScooterService scooterService;
        private IScooterRepositroy scooterRepository;

        private List<Scooter> seedData;

        [SetUp]
        public void Setup()
        {
            this.seedData = new List<Scooter>()
            {
               new Scooter("1", 2.5m),
               new Scooter("2", 3m)
            };
            this.scooterRepository = new ScooterRepositoryFake(seedData);
            this.scooterService = new ScooterService(scooterRepository);
        }

       

        [Test]
        public void AddScooter()
        {
            var addScooterRequest = new AddScooterRequest();
            addScooterRequest.Id = "3";
            addScooterRequest.PricePerMinute = 4.5m;
            this.scooterService.AddScooter(addScooterRequest);
            var addedScooter = this.scooterRepository.GetById(addScooterRequest.Id);
            Assert.AreNotEqual(addedScooter, null);
            Assert.AreEqual(addedScooter.PricePerMinute, addScooterRequest.PricePerMinute);
        }

        [Test]
        public void AddScooter_DuplicateId()
        {
            var addScooterRequest = new AddScooterRequest();
            addScooterRequest.Id = "1";
            addScooterRequest.PricePerMinute = 2.5m;
           
            Assert.Throws<DuplicateScooterIdException>(()=>scooterService.AddScooter(addScooterRequest));
        }

        [Test]
        public void RemoveScooter()
        {
            var addedItem = seedData.FirstOrDefault();
            scooterService.RemoveScooter(addedItem.Id);
            var nullItem = scooterRepository.GetById(addedItem.Id);
            Assert.IsNull(nullItem);
        }

        [Test]
        public void RemoveScooter_NotAvliableScooter()
        {
            var addedItem = seedData.FirstOrDefault();
            scooterRepository.ChangeScooterState(addedItem.Id, true);
            Assert.Throws<ScooterIsNotAvailableException>(() => scooterService.RemoveScooter(addedItem.Id));
        }

        [Test]
        public void RemoveScooter_ScooterNotExist()
        {
            Assert.Throws<ScootersNotExistException>(() => scooterService.RemoveScooter("1000"));
        }

        [Test]
        public void GetScooterList()
        {
            var addedItem = seedData.FirstOrDefault();
            scooterRepository.ChangeScooterState(addedItem.Id, true);
            var list = scooterService.GetScooters();

            var avliableRecordsCount= seedData.Where(x => !x.IsRented).Count();
            Assert.AreEqual(list.Count, avliableRecordsCount);
        }

        [Test]
        public void GetScooterById()
        {
            var addedItem = seedData.FirstOrDefault();
            scooterRepository.ChangeScooterState(addedItem.Id, true);
            var item = scooterService.GetScooterById(addedItem.Id);

            Assert.AreEqual(item.Id, addedItem.Id);
            Assert.AreEqual(item.PricePerMinute, addedItem.PricePerMinute);
        }
    }
}
