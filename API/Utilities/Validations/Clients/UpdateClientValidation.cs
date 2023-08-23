using API.Contracts;
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
                .Must((e, g) => IsDuplicateValue(e.Email, e.Guid)).WithMessage("Email is Already Exist");
            RuleFor(client => client.Capacity)
                .NotEmpty().WithMessage("Capacity Cannot be Null");
        }

        private bool IsDuplicateValue(string arg, Guid guid)
        {
            var temp = false;
            var email = GetEmail(guid);
            if (arg == email)
            {
                temp = true;
            }
            var result = _clientRepository.IsNotExist(arg) || temp;
            return result;
        }
        private string GetEmail(Guid guid)
        {
            var data = _clientRepository.GetByGuid(guid);
            return data.Email;
        }
    }
}
