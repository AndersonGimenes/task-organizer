
namespace TaskOrganizer.Domain.Constant
{
    public static class UseCaseMessage
    {
        public const string registerNotFound = "Register not found.";
        public const string fieldNotUpdate = "The {0} can't be update!";
        public const string invalidProgress = "The {0} must be {1}.";
        public const string registerCannotDelete = "Register can't delete when your progress is differente {0}.";
    }
}