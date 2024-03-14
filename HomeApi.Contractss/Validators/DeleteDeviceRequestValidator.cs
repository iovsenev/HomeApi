using FluentValidation;
using HomeApi.Contractss.Models.Devices;

namespace HomeApi.Contractss.Validators
{
    public class DeleteDeviceRequestValidator : AbstractValidator<DeleteDeviceRequest>
    {
        public DeleteDeviceRequestValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty();
        }
    }
}
