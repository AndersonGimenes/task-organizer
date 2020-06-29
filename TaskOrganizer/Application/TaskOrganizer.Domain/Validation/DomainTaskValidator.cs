using System;
using System.Linq;
using FluentValidation;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.Validation
{
    public class DomainTaskValidator : AbstractValidator<DomainTask>
    {
        public DomainTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(x => $"Please type some {nameof(x.Title)}!");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(x => $"Please type some {nameof(x.Description)}!");

        }

        public void DomainTaskValidate(DomainTask domainTask)
        {
            var errors = this.Validate(domainTask);

            if(!errors.IsValid)
            {
                var messageErrors = errors.Errors.Select( x => x.ErrorMessage);
                throw new DomainException.DomainException(string.Join(Environment.NewLine, messageErrors));
            }
        }
    }
}