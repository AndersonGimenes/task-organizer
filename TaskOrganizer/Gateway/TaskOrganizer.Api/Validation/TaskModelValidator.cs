using System;
using System.Linq;
using FluentValidation;
using TaskOrganizer.Api.Models;

namespace TaskOrganizer.Api.Validation
{
    public  class TaskModelValidator : AbstractValidator<TaskModel>
    {
        public TaskModelValidator()
        {
            RuleFor(x => x.StartDate).Empty().WithMessage("When Progress is ToDo cannot record StartDate.");
            RuleFor(x => x.EndDate).Empty().WithMessage("When Progress is ToDo cannot record EndDate.");
        }

        public void ValidateInProgress(TaskModel taskModel, string param)
        {
            var errors = this.Validate(taskModel, param);
            if(!errors.IsValid)
            {
                var mensageErrors = errors.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentException(string.Join(Environment.NewLine, mensageErrors));
            }
        }

        public void ValidateToDo(TaskModel taskModel)
        {
            var errors = this.Validate(taskModel);
            if(!errors.IsValid)
            {
                var mensageErrors = errors.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentException(string.Join(Environment.NewLine, mensageErrors));
            }
        }
    }
}