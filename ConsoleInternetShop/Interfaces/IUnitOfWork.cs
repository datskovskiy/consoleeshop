using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : IBaseEntity;
    }

}
