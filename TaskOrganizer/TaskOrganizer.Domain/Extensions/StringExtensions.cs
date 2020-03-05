namespace TaskOrganizer.Domain.Extensions
{
    public static class StringExtensions
    {
        internal static void IsValid(this string value, string message)
        {
            if(string.IsNullOrEmpty(value))
                throw new DomainException.DomainException(message);
        }

    }   
}
