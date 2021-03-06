using System;

namespace TaskOrganizer.UseCase.UseCaseException
{
    public class UseCaseException : Exception
    {
        public UseCaseException()
        {
        }

        public UseCaseException(string message) : base(message)
        {
        }

        public UseCaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}