using API.Contracts;
using API.DTOs.Clients;
using FluentValidation;

namespace API.Utilities.Validations.Clients
{
    public class InsertClientValidation : AbstractValidator<NewClientDto>
    {
        private readonly IClientRepository _clientRepository;

        public InsertClientValidation(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            RuleFor(client => client.Name)
                .NotEmpty().WithMessage("Name Cannot be Null");
            RuleFor(client => client.Email)
                .NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Wrong Email")
                .Must(IsDuplicateValue).WithMessage("Email is Already Exist");
            RuleFor(client => client.IsAvailable).NotNull().WithMessage("Please input with true/false");

            When(client => client.IsAvailable == true, () =>
            {
                RuleFor(client => client.Capacity).NotEmpty().WithMessage("Please Input Capacity if this Client Available");
            });

            When(client => client.IsAvailable == false, () =>
            {
                RuleFor(client => client.Capacity).Equal(0);
            });
        }

        private bool IsDuplicateValue(string arg)
        {
            return _clientRepository.IsNotExist(arg);
        }
    }
}
