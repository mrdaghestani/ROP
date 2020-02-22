using System.Collections.Generic;
using System.Threading.Tasks;

namespace ROP.Services.Repositories
{
    public interface IRepository<TModel, TKey>
        where TModel : Models.IModel<TKey>
    {
        Task<List<TModel>> GetAll();
        Task<TKey> GetNextKey();
        Task<TModel> GetByKey(TKey key);
        Task Create(TModel model);
        Task Modify(TModel model);
        Task CreateOrModify(TModel model);
    }
}