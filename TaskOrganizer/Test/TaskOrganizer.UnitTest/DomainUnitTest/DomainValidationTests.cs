using System;
using TaskOrganizer.Domain.DomainException;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
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
            var domainTask = new DomainTask
            {
                Title = input,
                Progress = Progress.ToDo,
                Description = "Any"
            };

            var ex = Assert.Throws<DomainException>(() => domainTask.IsValid());

            Assert.Equal(ex.Message,result);
        }

        [Theory]
        [InlineData("Title test", "Title test")]
        public void DomainExceptionMustNotBeReturnedWhenTheTitleIsPassed(string result, string input)
        {
            var domainTask = new DomainTask();

            domainTask.Title = input;

            Assert.Equal(domainTask.Title,result);
        }

        [Theory]
        [InlineData("Please type some Description!", null)]
        [InlineData("Please type some Description!", "")]
        public void DomainExceptionMustBeReturnedWhenTheDescriptionIsNullOrEmpty(string result, string input)
        {
            var domainTask = new DomainTask
            {
                Title = "Any",
                Progress = Progress.ToDo,
                Description = input
            };

            var ex = Assert.Throws<DomainException>(() => domainTask.IsValid());

            Assert.Equal(ex.Message,result);
        }

        [Theory]
        [InlineData("Description test", "Description test")]
        public void DomainExceptionMustNotBeReturnedWhenTheDescriptionIsPassed(string result, string input)
        {
            var domainTask = new DomainTask();

            domainTask.Description = input;

            Assert.Equal(domainTask.Description,result);
        }

        [Theory]
        [InlineData("The progress not set, please inform some Progress!", 0)]
        public void DomainExceptionMustBeReturnedWhenTheProgressIsNotValid(string result, int input)
        {
            
            var domainTask = new DomainTask
            {
                Title = "Test",
                Progress = (Progress)input,
                Description = "Any"
            };

            var ex = Assert.Throws<DomainException>(() => domainTask.IsValid());

            Assert.Equal(ex.Message, result);
        }

        [Theory]
        [InlineData(Progress.ToDo, Progress.ToDo)]
        public void DomainExceptionMustNotBeReturnedWhenTheProgressIsPassed(Progress result, Progress input)
        {
            var domainTask = new DomainTask();

            domainTask.Progress = input;

            Assert.Equal(domainTask.Progress,result);
        }
       
    }
}