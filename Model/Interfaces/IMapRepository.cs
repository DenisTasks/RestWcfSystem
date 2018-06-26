using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IMapRepository<TInEntity, TOutEntity> : IDisposable 
        where TInEntity : class
        where TOutEntity : class
    {
        DbContextTransaction BeginTransaction();
        IEnumerable<TOutEntity> Get();
        void Save();
    }
}
