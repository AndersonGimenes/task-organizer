using TaskOrganizer.Domain.DomainException;
using TaskOrganizer.Domain.Entities;
using TaskOrganizer.Domain.Enum;
using Xunit;

namespace TaskOrganizer.UnitTest.DomainUnitTest
{
    public class DomainValidationTests
    {
        [Fact]
        public void DomainExceptionMustBeReturnedWhenTheTitleIsNull()
        {
            var result = "Please type some Title!";

            var domainTask = new DomainTask
            {
                Title = null,
                Progress = Progress.ToDo,
                Description = "Any"
            };

            var ex = Assert.Throws<DomainException>(() => domainTask.IsValid());

            Assert.Equal(ex.Message,result);
        }

        [Fact]
        public void DomainExceptionMustBeReturnedWhenTheTitleIsEmpty()
        {
            var result = "Please type some Title!";

            var domainTask = new DomainTask
            {
                Title = string.Empty,
                Progress = Progress.ToDo,
                Description = "Any"
            };

            var ex = Assert.Throws<DomainException>(() => domainTask.IsValid());

            Assert.Equal(ex.Message,result);
        }

        [Fact]
        public void DomainExceptionMustNotBeReturnedWhenTheTitleIsPassed()
        {
            var result = "Title test";

            var domainTask = new DomainTask();

            domainTask.Title = "Title test";

            Assert.Equal(domainTask.Title,result);
        }

        [Fact]
        public void DomainExceptionMustBeReturnedWhenTheDescriptionIsNullOrEmpty()
        {
            var result = "Please type some Description!";

            var domainTask = new DomainTask
            {
                Title = "Any",
                Progress = Progress.ToDo,
                Description = string.Empty
            };

            var ex = Assert.Throws<DomainException>(() => domainTask.IsValid());

            Assert.Equal(ex.Message,result);
        }

        [Fact]
        public void DomainExceptionMustNotBeReturnedWhenTheDescriptionIsPassed()
        {
            var result = "Description test";
            
            var domainTask = new DomainTask();

            domainTask.Description = "Description test";

            Assert.Equal(domainTask.Description,result);
        }

        [Fact]
        public void DomainExceptionMustBeReturnedWhenTheProgressIsNotValid()
        {
            var result = "The progress not set, please inform some Progress!";
            
            var domainTask = new DomainTask
            {
                Title = "Test",
                Progress = 0,
                Description = "Any"
            };

            var ex = Assert.Throws<DomainException>(() => domainTask.IsValid());

            Assert.Equal(ex.Message, result);
        }

        [Fact]
        public void DomainExceptionMustNotBeReturnedWhenTheProgressIsPassed()
        {
            var result = Progress.ToDo;

            var domainTask = new DomainTask();

            domainTask.Progress = Progress.ToDo;

            Assert.Equal(domainTask.Progress, result);
        }
       
    }
}