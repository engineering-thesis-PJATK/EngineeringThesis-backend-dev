using System.Threading.Tasks;
using FluentValidation;
using OneBan_TMS.Models.DTOs.Ticket;

namespace OneBan_TMS.Validators.TicketValidators.Interfaces
{
    public class TicketNewFilter : ITicketNewFilter
    {
        private readonly IValidator<TicketNewDto> _ticketNewValidation;
        public TicketNewFilter(IValidator<TicketNewDto> ticketNewValidation)
        {
            _ticketNewValidation = ticketNewValidation;
        }
        public async Task<FilterResult> IsValid(TicketNewDto entity)
        {
            var validatorResults = await _ticketNewValidation.ValidateAsync(entity);
            if (!(validatorResults.IsValid))
            {
                return new FilterResult()
                {
                    Message = validatorResults.Errors[0].ErrorMessage,
                    PropertyName = validatorResults.Errors[0].PropertyName,
                    Valid = false
                };
            }
            return new FilterResult()
            {
                Valid = true
            };
        }

        public Task<FilterResult> IsValid(TicketNewDto entity, int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}