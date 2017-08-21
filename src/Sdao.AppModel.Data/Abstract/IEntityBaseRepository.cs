using Sdao.AppModel.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sdao.AppModel.Data.Abstract
{
    /// <summary>
    /// IEntityBaseRepository<T> where T:class,IEntityBase,new()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityBaseRepository<T> where T:class,IEntityBase,new()
    {
        /// <summary>
        /// AllIncluding
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// GetSingle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetSingle(long id);
        /// <summary>
        /// GetSingle
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// GetSingle
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// FindBy
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="predicate"></param>
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Commit
        /// </summary>
        void Commit();
    }
}
