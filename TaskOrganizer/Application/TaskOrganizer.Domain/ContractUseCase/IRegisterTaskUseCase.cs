﻿using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.Domain.ContractUseCase
{
    public interface IRegisterTaskUseCase
    {
        // create a new task or update a existing task
        int Register(DomainTask domainTask);
    }
}
