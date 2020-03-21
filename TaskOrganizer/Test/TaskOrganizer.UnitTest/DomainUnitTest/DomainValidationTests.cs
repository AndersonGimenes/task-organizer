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

            Assert.True(ex.Message.Equals(result));
        }

        [Theory]
        [InlineData("Title test", "Title test")]
        public void DomainExceptionMustNotBeReturnedWhenTheTitleIsPassed(string result, string input)
        {
            var domainTask = new DomainTask();

            domainTask.SetTitle(input);

            Assert.True(input.Equals(result));
        }

        [Theory]
        [InlineData("Please type some Description!", null)]
        [InlineData("Please type some Description!", "")]
        public void DomainExceptionMustBeReturnedWhenTheDescriptionIsNullOrEmpty(string result, string input)
        {
            var domainTask = new DomainTask();

            var ex = Assert.Throws<DomainException>(() => domainTask.SetDescription(input));

            Assert.True(ex.Message.Equals(result));
        }
        
    }
}