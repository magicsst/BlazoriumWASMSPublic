using BlazoriumWASMS.Data;
using BlazoriumWASMS.Repository.IRepository;
using BlazoriumWASMS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazoriumWASMS.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region "Synchronous Methods Implementation"
  
        public Category Create(Category obj)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            // Γεμίζει αυτόματα το obj με τα data του insert
            return obj;
        }

        public bool Delete(int id)
        {
            var obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (obj != null)
            {
                _db.Categories.Remove(obj);
                // _db.SaveChanges(): Return how many records have updated
                return _db.SaveChanges() > 0;
            }
            return false;
        }

        public Category Get(int id)
        {
            var obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return new Category();
            }
            return obj;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public Category Update(Category obj)
        {
            // Warning: Μπορώ να κάνω το update απευθείας στο obj 
            // _db.Categories.Update(obj);
            // Όμως επειδή μπορεί να θέλω να κάνω το Update σε συγκεκριμένα object.properties και όχι σε όλα, τότε χρησιμοποιώ αυτην την προσέγγιση
            var objFromDb = _db.Categories.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                objFromDb.Name = obj.Name;
                _db.Categories.Update(objFromDb);
                _db.SaveChanges();
                return objFromDb;
            }
            // return new Category() <Ένα άδειο? Γιατί>; // ή 
            return obj;
        }
        
        #endregion

        #region "Asynchronous Methods Implementation"
        public async Task<Category> CreateAsync(Category obj)
        {
            await _db.Categories.AddAsync(obj);
            await _db.SaveChangesAsync();
            // Γεμίζει αυτόματα το obj με τα data του insert
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.Categories.Remove(obj);
                // _db.SaveChanges(): Return how many records have updated
                return (await _db.SaveChangesAsync() > 0);
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var obj = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (obj == null)
            {
                return new Category();
            }
            return obj;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            // Warning: Μπορώ να κάνω το update απευθείας στο obj 
            // _db.Categories.Update(obj);
            // Όμως επειδή μπορεί να θέλω να κάνω το Update σε συγκεκριμένα object.properties και όχι σε όλα, τότε χρησιμοποιώ αυτην την προσέγγιση
            var objFromDb = await _db.Categories.FirstOrDefaultAsync(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                objFromDb.Name = obj.Name;
                _db.Categories.Update(objFromDb);
                await _db.SaveChangesAsync();
                return objFromDb;
            }
            // return new Category() <Ένα άδειο? Γιατί>; // ή 
            return obj;
        }
        #endregion
       
    }
}
