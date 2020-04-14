using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GonHuoSModel;
using GonHuoSDAL;
using System.Linq.Expressions;

namespace GonHuoSBll
{
    public class BaseService<T> where T : class
    {
        BaseRepository<T> baseRepository = null;
        public BaseRepository<T> MyRepository
        {
            get
            {
                if (baseRepository == null)
                {
                    baseRepository = new BaseRepository<T>();
                }
                return baseRepository;
            }
        }

        public List<T> GetAll()
        {
            return MyRepository.GetAll();
        }

        public List<T> GetByWhere(System.Linq.Expressions.Expression<Func<T, bool>> where ,bool proxyCreationEnabled = true)
        {
            return MyRepository.GetByWhere(where, proxyCreationEnabled);
        }

        public List<T> GetByWhereAsc<orderByT>(Expression<Func<T, bool>> where, Expression<Func<T, orderByT>> orderBy, ref int pageIndex, ref int count, ref int pageCount, ref int pageSizi)
        {
            return MyRepository.GetByWhereAsc(where,orderBy,ref pageIndex,ref count,ref pageCount,pageSizi);
        }

        public List<T> GetByWhereDesc<orderByT>(Expression<Func<T, bool>> where, Expression<Func<T, orderByT>> orderBy, ref int pageIndex, ref int count, ref int pageCount, int pageSizi)
        {
            return MyRepository.GetByWhereDesc(where, orderBy, ref pageIndex, ref count, ref pageCount, pageSizi);
        }

        public bool Add(T Model)
        {
            return MyRepository.Add(Model);
        }

        public bool Updt(T Model)
        {
            return MyRepository.Updt(Model);
        }

        public bool Dele(T Model)
        {
            return MyRepository.Dele(Model);
        }
    }
}
