using ConsoleEShop.Interfaces;

namespace ConsoleEShop.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}