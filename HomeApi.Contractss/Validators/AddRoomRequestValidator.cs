using FluentValidation;
using HomeApi.Contractss.Models.Devices;

namespace HomeApi.Contracts.Validators
{
    public class AddRoomRequestValidator : AbstractValidator<AddRoomRequest>
    {
        public AddRoomRequestValidator()
        {
            RuleFor(x => x.Area).NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(BeSupported)
                .WithMessage($"Please choose one of the following locations: " +
                $"{string.Join(", ", Values.ValidRooms)}");
            RuleFor(x => x.Voltage)
                .NotEmpty()
                .InclusiveBetween(120,220);
            RuleFor(x => x.GasConnected).NotEmpty();
        }

        private bool BeSupported(string location)
        {
            // Проверим, содержится ли значение в списке допустимых
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
