namespace MyTestViajaNet.DomainService.Interfaces.Services
{
    using System.Collections.Generic;

    public interface IServiceBase<Tentity> where Tentity : class
    {
        void Add(Tentity obj);
        Tentity GetById(int id);
        IEnumerable<Tentity> GetAll();
        void Update(Tentity obj);
        void Remove(Tentity obj);
        void Dispose();

    }
}
