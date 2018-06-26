using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Model.Entities;
using Model.Interfaces;

namespace Model.ModelService
{
    public class AppointmentRepository<TInEntity, TOutEntity> : IMapRepository<TInEntity, TOutEntity> 
        where TInEntity : class 
        where TOutEntity : class
    {
        private WPFOutlookContext _context;
        private DbSet<TInEntity> _dbSet;

        public AppointmentRepository(WPFOutlookContext context)
        {
            _context = context;
            _dbSet = _context.Set<TInEntity>();
            //_context.Configuration.AutoDetectChangesEnabled = true;
        }

        public DbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<TOutEntity> Get()
        {
            var mapCollection = _dbSet.AsNoTracking().ToList();
            var collection = Mapper.Map<IEnumerable<TInEntity>, IEnumerable<TOutEntity>>(mapCollection).ToList();
            return collection;
        }
    }

}
