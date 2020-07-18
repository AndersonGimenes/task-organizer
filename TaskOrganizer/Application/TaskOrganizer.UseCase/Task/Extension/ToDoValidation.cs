using TaskOrganizer.Domain.Constant;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;

namespace TaskOrganizer.UseCase.Task.Extension
{
    public static class DomainTaskExtensions
    {
        public static void ProgressValidation(this DomainTask domainTask, Progress progress)
        {
            if(!domainTask.Progress.Equals(progress))
                throw new UseCaseException.UseCaseException(string.Format(UseCaseMessage.invalidProgress ,nameof(domainTask.Progress),progress));
        }
    }
}