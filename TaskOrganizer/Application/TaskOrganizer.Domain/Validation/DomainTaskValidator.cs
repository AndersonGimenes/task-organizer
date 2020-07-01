using System;
using System.Linq;
using FluentValidation;
using TaskOrganizer.Domain.Constant;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.Validation
{
    public class DomainTaskValidator : AbstractValidator<DomainTask>
    {
        public DomainTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(x => string.Format(DomainMessage.titleOrDescripitionNullOrEmpty, nameof(x.Title)));

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(x => string.Format(DomainMessage.titleOrDescripitionNullOrEmpty, nameof(x.Description)));

            RuleFor(x => x.Progress)
                .IsInEnum()
                .WithMessage(x => string.Format(DomainMessage.invalidProgress, nameof(x.Progress)));

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