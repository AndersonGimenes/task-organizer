using System;
using System.Linq;
using FluentValidation;
using TaskOrganizer.Api.Constant.cs;
using TaskOrganizer.Api.Models.Request;

namespace TaskOrganizer.Api.Validation
{
    public class ToDoTaskRequestValidation : AbstractValidator<ToDoTaskRequest>
    {
        public ToDoTaskRequestValidation()
        {
            RuleFor(x => x.TaskBase.StartDate)
                .Empty()
                .WithMessage(x => string.Format(RequestMessage.fieldCanNotRecord, "ToDo", nameof(x.TaskBase.StartDate)));
            RuleFor(x => x.TaskBase.EndDate)
                .Empty()
                .WithMessage(x => string.Format(RequestMessage.fieldCanNotRecord, "ToDo", nameof(x.TaskBase.EndDate)));
        }

        public void ValidateToDo(ToDoTaskRequest request)
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