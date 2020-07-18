using System;
using System.Linq;
using FluentValidation;
using TaskOrganizer.Api.Constant.cs;
using TaskOrganizer.Api.Models.Request;

namespace TaskOrganizer.Api.Validation
{
    public class DoneTaskRequestValidation : AbstractValidator<DoneTaskRequest>
    {
        public DoneTaskRequestValidation()
        {
            RuleFor(x => x.TaskBase.EndDate)
                .Empty()
                .WithMessage( x => string.Format(RequestMessage.fieldCanNotRecord, "Done", nameof(x.TaskBase.EndDate)));
        }

        public void ValidateDone(DoneTaskRequest request)
        {
            var errors = this.Validate(request);
            if(!errors.IsValid)
            {
                var mensageErrors = errors.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentException(string.Join(Environment.NewLine, mensageErrors));
            }

        }
    }
}