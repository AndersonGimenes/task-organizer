using System;
using System.Linq;
using FluentValidation;
using TaskOrganizer.Api.Models;

namespace TaskOrganizer.Api.Validation
{
    public class ToDoModelValidation : AbstractValidator<ToDoModel>
    {
        public ToDoModelValidation()
        {
            RuleFor(x => x.TaskModel.StartDate)
                .Empty()
                .WithMessage(x => $"When Progress is ToDo cannot record {nameof(x.TaskModel.StartDate)}.");
            RuleFor(x => x.TaskModel.EndDate)
                .Empty()
                .WithMessage(x => $"When Progress is ToDo cannot record {nameof(x.TaskModel.EndDate)}.");
        }

        public void ValidateToDo(ToDoModel toDoModel)
        {
            var errors = this.Validate(toDoModel);
            if(!errors.IsValid)
            {
                var mensageErrors = errors.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentException(string.Join(Environment.NewLine, mensageErrors));
            }
        }
    }
}