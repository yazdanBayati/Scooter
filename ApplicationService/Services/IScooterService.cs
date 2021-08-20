using ApplicationService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationService.Services
{
    public interface IScooterService
    {
        /// <summary>
        /// Add scooter.
        /// </summary>
        /// <param name="request">Add Scooter Request.</param>
        void AddScooter(AddScooterRequest request);
        /// <summary>
        /// Remove scooter. This action is not allowed for scooters if the rental is in progress.
        /// </summary>
        /// <param name="id">Unique ID of the scooter.</param>
        void RemoveScooter(string id);
        /// <summary>
        /// List of scooters that belong to the company.
        /// </summary>
        /// <returns>Return a list of available scooters.</returns>
        IList<ScooterDto> GetScooters();
        /// <summary>
        /// Get particular scooter by ID.
        /// </summary>
        /// <param name="scooterId">Unique ID of the scooter.</param>
        /// <returns>Return a particular scooter.</returns>
        ScooterDto GetScooterById(string scooterId);
    }
}
