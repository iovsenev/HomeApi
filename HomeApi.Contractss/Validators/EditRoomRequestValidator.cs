using FluentValidation;
using HomeApi.Contracts.Validators;
using HomeApi.Contractss.Models.Rooms;

namespace HomeApi.Contractss.Validators
{
    public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
    {
        public EditRoomRequestValidator()
        {
            RuleFor(r => r.Voltage)
                .InclusiveBetween(120, 220)
                .When(r => r.Voltage != null);
            RuleFor(r => r.Name)
                .Must(BeSupported)
                .When(r => r.Name != null)
                .WithMessage($"Please choose one of the following locations: " +
                $"{string.Join(", ", Values.ValidRooms)}");
        }

        private bool BeSupported(string location)
        {
            // Проверим, содержится ли значение в списке допустимых
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
