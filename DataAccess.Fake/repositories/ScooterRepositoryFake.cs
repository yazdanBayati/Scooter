using Core.DataAccess;
using Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Fake.repositories
{
    public class ScooterRepositoryFake : IScooterRepositroy
    {
        private List<Scooter> scooterList;
        
        public ScooterRepositoryFake(List<Scooter> scooters)
        {
            this.scooterList = scooters;
        }

        public void Add(Scooter scooter)
        {
            this.scooterList.Add(scooter);
        }

        public Scooter GetById(string id)
        {
            return this.scooterList.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(Scooter scooter)
        {
            this.scooterList.Remove(scooter);
        }

        public void ChangeScooterState(string id, bool isRented)
        {
            var item = this.scooterList.FirstOrDefault(x=>x.Id == id);
            item.IsRented = isRented;
        }

        public IList<Scooter> GetScooters()
        {
            return this.scooterList.Where(x => !x.IsRented).ToList();
        }
    }
}
