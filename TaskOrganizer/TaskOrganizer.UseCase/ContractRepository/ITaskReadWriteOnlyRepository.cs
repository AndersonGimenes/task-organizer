using System;
using System.Collections.Generic;
using System.Text;
using TaskOrganizer.Domain.Entities;

namespace TaskOrganizer.UseCase.ContractRepository
{
    interface ITaskReadWriteOnlyRepository
    {
        void Add(DomainTask domainTask);
        void Update(DomainTask domainTask);
        void Delete(DomainTask domainTask);
    }
}
