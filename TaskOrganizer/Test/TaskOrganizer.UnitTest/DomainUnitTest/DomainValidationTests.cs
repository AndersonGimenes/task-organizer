using TaskOrganizer.Domain.DomainException;
using TaskOrganizer.Domain.Entities;
using Xunit;

namespace TaskOrganizer.UnitTest.DomainUnitTest
{
    public class DomainValidationTests
    {
        [Theory]
        [InlineData("Please type some Title!", null)]
        [InlineData("Please type some Title!", "")]
        public void DomainExceptionMustBeReturnedWhenTheTitleIsNullOrEmpty(string result, string input)
        {
            var domainTask = new DomainTask();

            var ex = Assert.Throws<DomainException>(() => domainTask.SetTitle(input));

            Assert.Equal(ex.Message,result);
        }

        [Theory]
        [InlineData("Title test", "Title test")]
        public void DomainExceptionMustNotBeReturnedWhenTheTitleIsPassed(string result, string input)
        {
            var domainTask = new DomainTask();

            domainTask.SetTitle(input);

            Assert.Equal(input,result);
        }

        [Theory]
        [InlineData("Please type some Description!", null)]
        [InlineData("Please type some Description!", "")]
        public void DomainExceptionMustBeReturnedWhenTheDescriptionIsNullOrEmpty(string result, string input)
        {
            var domainTask = new DomainTask();

            var ex = Assert.Throws<DomainException>(() => domainTask.SetDescription(input));

            Assert.Equal(ex.Message,result);
        }

        [Theory]
        [InlineData("Description test", "Description test")]
        public void DomainExceptionMustNotBeReturnedWhenTheDescriptionIsPassed(string result, string input)
        {
            var domainTask = new DomainTask();

            domainTask.SetDescription(input);

            Assert.Equal(input,result);
        }
        [Theory]
        [InlineData("The progress not set, please inform some Progress!", null)]
        [InlineData("The progress not set, please inform some Progress!", "")]
        public void DomainExceptionMustBeReturnedWhenTheProgressIsNullOrEmpty(string result, string input)
        {
            var domainTask = new DomainTask();

            var ex = Assert.Throws<DomainException>(() => domainTask.SetProgress(input));

            Assert.Equal(ex.Message,result);
        }

        [Theory]
        [InlineData("ToDo", "ToDo")]
        public void DomainExceptionMustNotBeReturnedWhenTheProgressIsPassed(string result, string input)
        {
            var domainTask = new DomainTask();

            domainTask.SetDescription(input);

            Assert.Equal(input,result);
        }
        
    }
}