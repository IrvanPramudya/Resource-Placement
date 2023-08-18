﻿using API.Contracts;
using API.DTOs.Clients;
using FluentValidation;

namespace API.Utilities.Validations.Clients
{
    public class UpdateClientValidation : AbstractValidator<ClientDto>
    {
        private readonly IClientRepository _clientRepository;

        public UpdateClientValidation(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            RuleFor(client => client.Name)
                .NotEmpty().WithMessage("Name Cannot be Null");
            RuleFor(client => client.Email)
                .NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Wrong Email")
                .Must(IsDuplicateValue).WithMessage("Email is Already Exist");
            RuleFor(client => client.Capacity)
                .NotEmpty().WithMessage("Capacity Cannot be Null");
        }

        private bool IsDuplicateValue(string arg)
        {
            return _clientRepository.IsNotExist(arg);
        }
    }
}