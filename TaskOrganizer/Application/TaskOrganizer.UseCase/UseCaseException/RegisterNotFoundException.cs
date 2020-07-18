using System;
namespace TaskOrganizer.UseCase.UseCaseException
{
    public class RegisterNotFoundException : Exception
    {
        public RegisterNotFoundException()
        {
            
        }

        public RegisterNotFoundException(string message) : base(message)
        {
            
        }

        public RegisterNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}