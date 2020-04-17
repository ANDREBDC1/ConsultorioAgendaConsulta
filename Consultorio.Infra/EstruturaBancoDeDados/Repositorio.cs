using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;

namespace Consultorio.Infra.EstruturaBancoDeDados
{
    public abstract class Repositorio<TEntity> where TEntity : class
    {
        private DbConsultorio _dbConsultorio = new DbConsultorio();
        public IQueryable<TEntity> GetAll()
        {
            return _dbConsultorio.Set<TEntity>();
        }

        public IQueryable<TEntity> GetSimpleBy(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        private string GetValue(object value, string fieldName)
        {
            object obj = typeof(TEntity);

            var type = obj.GetType().GetProperty(fieldName)?.PropertyType;

            if (type == typeof(string) || type == typeof(DateTime))
                return $"'{value.ToString()}'";

            return value.ToString();
        }

        //public IQueryable<TEntity> GetBy(string predicate)
        //{
        //    return GetAll().Where(predicate).AsQueryable();
        //}

        public TEntity Find(params object[] key)
        {
            return _dbConsultorio.Set<TEntity>().Find(key);
        }

        public void Update(List<TEntity> objs)
        {
            foreach (var obj in objs)
            {
                _dbConsultorio.Entry(obj).State = EntityState.Modified;
            }

        }

        public void Update(TEntity obj)
        {
            _dbConsultorio.Entry(obj).State = EntityState.Modified;
        }

        public int SaveAll()
        {
            return _dbConsultorio.SaveChanges();
        }

        public int SaveAndGetId()
        {
            _dbConsultorio.SaveChanges();
            return ExecuteQueryDb<int>($"SELECT MAX([{GetFieldsNamePrimaryKey()}]) FROM [{GetTableName()}]").FirstOrDefault();
        }

        private string GetFieldsNamePrimaryKey()
        {
            return GetAll().ElementType.GetProperties().FirstOrDefault()?.Name;
        }

        public void AddMultiple(List<TEntity> objs)
        {
            _dbConsultorio.Set<TEntity>().AddRange(objs);
        }

        public void Add(TEntity obj)
        {
            _dbConsultorio.Set<TEntity>().Add(obj);
        }

        public void DeleteBy(Func<TEntity, bool> predicate)
        {
            _dbConsultorio.Set<TEntity>().Where(predicate).ToList()
                .ForEach(delete => _dbConsultorio.Set<TEntity>().Remove(delete));
        }

        public void DeleteAll()
        {
            GetAll().ToList().ForEach(c => _dbConsultorio.Set<TEntity>().Remove(c));
        }

        public List<TEntity> ExecuteQuery(string sql)
        {
            return _dbConsultorio.Set<TEntity>().SqlQuery(sql).ToList();
        }

        public List<TT> ExecuteQueryDb<TT>(string sql, params object[] parameters)
        {
            return _dbConsultorio.Database.SqlQuery<TT>(sql, parameters).ToList();
        }

        public List<TT> ExecuteQueryDb<TT>(string sql)
        {
            return _dbConsultorio.Database.SqlQuery<TT>(sql).ToList();
        }

        public string GetTableName()
        {
            var nomeTable = typeof(TEntity).Name;
            return nomeTable; //GetNamePlural(nomeTable);
        }

        public string GetTableName<TT>()
        {
            var nomeTable = typeof(TT).Name;
            return nomeTable; //GetNamePlural(nomeTable);
        }


        public DbContext GetDbContext()
        {
            return _dbConsultorio;
        }

        public void Dispose()
        {
            _dbConsultorio?.Dispose();
        }
    }
}
