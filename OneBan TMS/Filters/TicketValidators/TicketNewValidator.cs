using System.Data;
using FluentValidation;
using OneBan_TMS.Models.DTOs.Ticket;

namespace OneBan_TMS.Validators.TicketValidators
{
    public class TicketNewValidator : AbstractValidator<TicketNewDto>
    {
        public TicketNewValidator()
        {
            RuleFor(x => x.TicTopic)
                .NotEmpty()
                .WithMessage("Topic can not be empty");
            RuleFor(x => x.TicDescription)
                .NotEmpty()
                .WithMessage("Description can not be empty");
            //Todo: Ustalić walidację 
        }
    }
}