﻿using System;
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
        public Progress Progress { get; set; }
        
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
    }
}
