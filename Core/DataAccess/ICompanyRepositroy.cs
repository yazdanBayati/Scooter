using Core.Domains;
using System.Collections.Generic;

namespace Core.DataAccess
{
    public interface ICompanyRepositroy
    {
        void AddCompanyScooter(CompanyScooter companyScooter);
        CompanyScooter GetCompanyScooterById(int id);
        CompanyScooter GetCompanyScooterByScooterId(string scooterId);
        void UpdateScooterCompany(CompanyScooter scooterComapny);
        List<CompanyScooter> GetCompanyRentedScooterList(string companyName, int? year, bool inculdeNotCompletedRentals);
    }
}