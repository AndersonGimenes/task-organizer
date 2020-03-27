namespace TaskOrganizer.IntegrationTest.UseCaseIntegrationTest.Common
{
    public static class Helper
    {
        //controls the number of records entered
        public static int IdBase { get; private set; }

        public static void IncrementId() 
        { 
            IdBase++;
        }
    }
}