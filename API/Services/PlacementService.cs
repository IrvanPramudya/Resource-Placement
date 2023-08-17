﻿using API.Contracts;
using API.Models;
using API.Repositories;

namespace API.Services
{
    public class PlacementService
    {
        private readonly IPlacementRepository _placementRepository;

        public PlacementService(IPlacementRepository placementRepository)
        {
            _placementRepository = placementRepository;
        }

        public IEnumerable<PlacementDto> GetAll()
        {
            var placements = _placementRepository.GetAll();
            if (!placements.Any())
            {
                return Enumerable.Empty<PlacementDto>(); // Placement is null or not found;
            }

            var placementDtos = new List<PlacementDto>();
            foreach (var placement in placements)
            {
                placementDtos.Add((PlacementDto)placement);
            }

            return placementDtos; // Placement is found;
        }

        public PlacementDto? GetByGuid(Guid guid)
        {
            var placement = _placementRepository.GetByGuid(guid);
            if (placement is null)
            {
                return null; // Placement is null or not found;
            }

            return (PlacementDto)placement; // Placement is found;
        }

        public PlacementDto? Create(NewPlacementDto newPlacementDto)
        {
            var placement = _placementRepository.Create(newPlacementDto);
            if (placement is null)
            {
                return null; // Placement is null or not found;
            }

            return (PlacementDto)placement; // Placement is found;
        }

        public int Update(PlacementDto placementDto)
        {
            var placement = _placementRepository.GetByGuid(placementDto.Guid);
            if (placement is null)
            {
                return -1; // Placement is null or not found;
            }

            Placement toUpdate = placementDto;
            toUpdate.CreatedDate = placement.CreatedDate;
            var result = _placementRepository.Update(toUpdate);

            return result ? 1 // Placement is updated;
                : 0; // Placement failed to update;
        }

        public int Delete(Guid guid)
        {
            var placement = _placementRepository.GetByGuid(guid);
            if (placement is null)
            {
                return -1; // Placement is null or not found;
            }

            var result = _placementRepository.Delete(placement);

            return result ? 1 // Placement is deleted;
                : 0; // Placement failed to delete;
        }
    }
}