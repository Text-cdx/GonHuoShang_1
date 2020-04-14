using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GonHuoSModel;
using System.Linq.Expressions;
using System.Data.Entity;

namespace GonHuoSDAL
{
    public class BaseRepository<T> where T : class
    {
        DbContext dbcontext = null;
        public DbContext MyDbContext
        {
            get
            {
                if (dbcontext == null)
                {
                    dbcontext = new DbContext("name=GonHuoShangEntities");
                }
                return dbcontext;
            }
        }

        public List<T> GetAll()
        {
            return MyDbContext.Set<T>().ToList();
        }

        public List<T> GetByWhere(Expression<Func<T, bool>> where,bool proxyCreationEnabled=true)
        {
            MyDbContext.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            return MyDbContext.Set<T>().Where(where).ToList();
        }

        /// <summary>
        /// 条件升序查询 带分页
        /// </summary>
        /// <typeparam name="orderByT"></typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="count">总条数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageSizi">页码</param>
        /// <returns></returns>
        public List<T> GetByWhereAsc<orderByT>(Expression<Func<T, bool>> where,Expression<Func<T,orderByT>> orderBy ,ref int pageIndex,ref int count, ref int pageCount , int pageSizi)
        {
            count = MyDbContext.Set<T>().Where(where).Count();      //总条数
            pageCount = count % pageSizi == 0 ? count / pageSizi : count / pageSizi + 1;        //总页数
            if (pageIndex <= 1 || count == 0) pageIndex = 1;
            else if (pageIndex >= pageCount) pageIndex = pageCount;

            var filterCount = (pageIndex - 1) * pageSizi;  //从哪一页开始
            return MyDbContext.Set<T>().Where(where).OrderBy(orderBy).Skip(filterCount).Take(pageSizi).ToList();
        }

        /// <summary>
        /// 条件降序查询  带分页
        /// </summary>
        /// <typeparam name="orderByT"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="count"></param>
        /// <param name="pageCount"></param>
        /// <param name="pageSizi"></param>
        /// <returns></returns>
        public List<T> GetByWhereDesc<orderByT>(Expression<Func<T,bool>> where,Expression<Func<T,orderByT>> orderBy,ref int pageIndex,ref int count, ref int pageCount, int pageSizi)
        {
            count = MyDbContext.Set<T>().Where(where).Count();
            pageCount = count % pageSizi == 0 ? count / pageSizi : count / pageSizi + 1;
            if (pageIndex <= 1 || count == 0) pageIndex = 1;
            else if (pageIndex >= pageCount) pageIndex = pageCount;

            var filterCount = (pageIndex - 1) * pageSizi;
            return MyDbContext.Set<T>().Where(where).OrderByDescending(orderBy).Skip(filterCount).Take(pageSizi).ToList();
        }

        public bool Add(T Model)
        {
            MyDbContext.Entry<T>(Model).State = EntityState.Added;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }

        public bool Updt(T Model)
        {
            MyDbContext.Entry<T>(Model).State = EntityState.Modified;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }

        public bool Dele(T Model)
        {
            MyDbContext.Entry<T>(Model).State = EntityState.Deleted;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }
        
    }
}
