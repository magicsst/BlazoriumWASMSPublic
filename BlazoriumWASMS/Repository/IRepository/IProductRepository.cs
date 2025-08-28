using BlazoriumWASMS.Shared.Models;

namespace BlazoriumWASMS.Repository.IRepository
{
    public interface IProductRepository
    {
        #region "Synchronous Methods"
        public Product Create(Product obj); 
        public Product Update(Product obj);
        public bool Delete(int id); 
        public Product Get(int id);
        public IEnumerable<Product> GetAll();
        #endregion

        #region "Asynchronous Methods"
        public Task<Product> CreateAsync(Product obj); 
        public Task<Product> UpdateAsync(Product obj);
        public Task<bool> DeleteAsync(int id); 
        public Task<Product> GetAsync(int id);
        public Task<IEnumerable<Product>> GetAllAsync();
        #endregion
    }
}
