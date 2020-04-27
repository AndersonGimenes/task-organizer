using System;
using TaskOrganizer.Domain.Extensions;

namespace TaskOrganizer.Domain.Entities
{
    public class DomainTask
    {
        public int TaskNumeber { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimatedDate { get; set; }
        public Progress Progress { get; private set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public void SetTitle(string title)
        {
            title.IsValid($"Please type some {nameof(Title)}!");
            this.Title = title;
        }

        public void SetDescription(string description)
        {
            description.IsValid($"Please type some {nameof(Description)}!");
            this.Description = description;
        }

        public void SetProgress(string progress){
            progress.IsValid($"The progress not set, please inform some {nameof(Progress)}!");
            this.Progress = ChooseProgressType(progress);
        }

        private Progress ChooseProgressType(string progress)
        {
            if(progress.Equals("ToDo")) 
                return Progress.ToDo;

            if(progress.Equals("InProgress"))
                return Progress.InProgress;

            if(progress.Equals("Done"))
                return Progress.Done;

            return default;
        }
    }
}
