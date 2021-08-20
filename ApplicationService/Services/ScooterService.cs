
using ApplicationService.Dtos;
using Core.DataAccess;
using Core.Domains;
using Infra.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationService.Services
{
    public class ScooterService : IScooterService
    {

        private readonly IScooterRepositroy _repository;
        public ScooterService(IScooterRepositroy repository)
        {
            this._repository = repository;
        }
        
        public void AddScooter(AddScooterRequest request)
        {
            var scooter = new Scooter(request.Id, request.PricePerMinute);
            scooter.IsRented = false;
            scooter.CheckIdValidity();
            CheckRecordByIdExist(request);
            this._repository.Add(scooter);
        }

        private void CheckRecordByIdExist(AddScooterRequest request)
        {
            var oldScooter = this._repository.GetById(request.Id);
            if (oldScooter != null)
            {
                throw new DuplicateScooterIdException();
            }
        }

        public ScooterDto GetScooterById(string scooterId)
        {
            var scooter = _repository.GetById(scooterId);
            var scooterDto = new ScooterDto();
            scooterDto.Parse(scooter);
            return scooterDto;
        }

        public IList<ScooterDto> GetScooters()
        {
            var scooterList = _repository.GetScooters();
            var res = ParseToScooterDtoList(scooterList);
            return res;
        }

        private IList<ScooterDto> ParseToScooterDtoList(IList<Scooter> scooterList)
        {
            var result = new List<ScooterDto>();
            foreach (var scooter in scooterList)
            {
                var scooterDto = new ScooterDto();
                scooterDto.Parse(scooter);
                result.Add(scooterDto);
            }

            return result;
        }

        public void RemoveScooter(string id)
        {
            var scooter = LoadScooter(id);
            scooter.CheckAvliablity();
            _repository.Remove(scooter);
        }

        private Scooter LoadScooter(string id)
        {
            var record = _repository.GetById(id);
            if (record == null)
            {
                throw new ScootersNotExistException();
            }

            return record;
        }
    }
}
