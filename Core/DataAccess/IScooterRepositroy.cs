using Core.Domains;
using System.Collections.Generic;

namespace Core.DataAccess
{
    public interface IScooterRepositroy
    {
        /// <summary>
        /// Add scooter to DB
        /// </summary>
        /// <param name="scooter"></param>
        void Add(Scooter scooter);
        
        /// <summary>
        /// get scooter by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Scooter GetById(string id);
        /// <summary>
        /// change rent state  in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isRented"></param>
        void ChangeScooterState(string id, bool isRented);

        /// <summary>
        /// remove scooter from db
        /// </summary>
        /// <param name="scooter"></param>
        void Remove(Scooter scooter);
        /// <summary>
        /// get list of avilable scooters
        /// </summary>
        IList<Scooter> GetScooters();
    }
}