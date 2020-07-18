using System;
using System.Linq;
using FluentValidation;
using TaskOrganizer.Api.Constant.cs;
using TaskOrganizer.Api.Models.Request;

namespace TaskOrganizer.Api.Validation
{
    public class InProgressTaskRequestValidation : AbstractValidator<InProgressTaskRequest>
    {
        public InProgressTaskRequestValidation()
        {
            RuleFor(x => x.TaskRequest.EndDate)
                .Empty()
                .WithMessage(x => string.Format(RequestMessage.fieldCanNotRecord, "InProgress", nameof(x.TaskRequest.EndDate)));
        }

        public void ValidateInProgress(InProgressTaskRequest request)
        {
            var errors = this.Validate(request);
            if(!errors.IsValid)
            {
                var mensageErrors = errors.Errors.Select(x => x.ErrorMessage);
                throw new System.ArgumentException(string.Join(Environment.NewLine, mensageErrors));
            }
        }
    }
}