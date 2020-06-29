using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using TaskOrganizer.Api.Models;

namespace TaskOrganizer.Api.Validation
{
    public  class TaskModelValidator : AbstractValidator<TaskModel>
    {
        public TaskModelValidator()
        {
            RuleFor(x => x.StartDate)
                .Empty()
                .WithMessage("When Progress is {0} cannot record StartDate.");
            RuleFor(x => x.EndDate)
                .Empty()
                .WithMessage("When Progress is {0} cannot record EndDate.");
        }

        public void ValidateToDo(TaskModel taskModel)
        {
            var errors = this.Validate(taskModel);
            if(!errors.IsValid)
            {
                var mensageErrors = errors.Errors.Select(x => x.ErrorMessage);
                var mensageErrorsFormated = FormatMensageError(mensageErrors, taskModel.Progress);

                throw new ArgumentException(string.Join(Environment.NewLine, mensageErrorsFormated));
            }
        }

        public void ValidateInProgress(TaskModel taskModel, string param)
        {
            var errors = this.Validate(taskModel, param);
            if(!errors.IsValid)
            {
                var mensageErrors = errors.Errors.Select(x => x.ErrorMessage);
                var mensageErrorsFormated = FormatMensageError(mensageErrors, taskModel.Progress);
                
                throw new ArgumentException(string.Join(Environment.NewLine, mensageErrorsFormated));
            }
        }

        private List<string> FormatMensageError(IEnumerable<string> mensageErrors, string type)
        {
            var result = new List<string>();
            foreach(var message in mensageErrors)
            {
                result.Add(string.Format(message, type));
            }

            return result;
        }

    }
}