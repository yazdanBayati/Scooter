using Core.DataAccess;
using Core.Domains;
using Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Fake.repositories
{
    public class CompanyRepositroyFake : ICompanyRepositroy
    {
        private IList<CompanyScooter> _companyScooters;

        public CompanyRepositroyFake(IList<CompanyScooter> comapnyScooters)
        {
            _companyScooters = comapnyScooters;
        }
        
        public void AddCompanyScooter(CompanyScooter companyScooter)
        {
            companyScooter.Id = NumberUtil.GetNextNumber();
            _companyScooters.Add(companyScooter);
        }

        public List<CompanyScooter> GetCompanyRentedScooterList(string companyName, int? year, bool inculdeNotCompletedRentals)
        {

            var result = _companyScooters.Where(x => x.CompanyName == companyName);
            if(year == null)
            {
                
                result = LoadAllTheCopmanyRentedScooters(result, inculdeNotCompletedRentals);
            }
            else
            {
                result = LoadCompanyReneteScooterPerYear(result, year, inculdeNotCompletedRentals);
            }

            return result.ToList();
        }

        private IEnumerable<CompanyScooter> LoadCompanyReneteScooterPerYear(IEnumerable<CompanyScooter> list, int? year, bool inculdeNotCompletedRentals)
        {
            IEnumerable<CompanyScooter> result = list.Where(x => x.StartDate.Year == year);
            if (!inculdeNotCompletedRentals)
            {
                result = list.Where(x => x.EndDate.HasValue && x.EndDate.Value.Year == year);
            }

            return result;
        }

        private IEnumerable<CompanyScooter> LoadAllTheCopmanyRentedScooters(IEnumerable<CompanyScooter> list, bool inculdeNotCompletedRentals)
        {
            if (!inculdeNotCompletedRentals)
            {
                list = list.Where(x => x.EndDate.HasValue && x.EndDate.Value <= DateTime.Now);
            }
            return list;
        }

        public CompanyScooter GetCompanyScooterById(int id)
        {
            return _companyScooters.FirstOrDefault(x => x.Id == id);
        }

        public CompanyScooter GetCompanyScooterByScooterId(string scooterId)
        {
            return _companyScooters.FirstOrDefault(x => x.ScooterId == scooterId && x.EndDate == null);
        }

        public void UpdateScooterCompany(CompanyScooter scooterComapny)
        {
            var item = _companyScooters.FirstOrDefault(x => x.Id == scooterComapny.Id);
            item = scooterComapny;
        }
    }
}
