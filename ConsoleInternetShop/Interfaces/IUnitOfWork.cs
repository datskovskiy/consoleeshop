using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IAuthRepository AuthRepository { get; }
    }
}
