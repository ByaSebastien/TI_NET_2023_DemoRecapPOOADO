using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_2023_DemoRecapPOOADO.Domain.Entities;

namespace TI_NET_2023_DemoRecapPOOADO.DAL.Repositories
{
    public interface IBaseRepository<TKey,TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        TEntity ReadOne(TKey id);
        IEnumerable<TEntity> ReadAll();
        bool Update(TKey id, TEntity entity);
        bool Delete(TKey id);
    }
}
