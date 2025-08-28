using BlazoriumWASMS.Shared.Models;

namespace BlazoriumWASMS.Repository.IRepository
{
    // Αυτα ειναι τα endpoints που θα χρησιμοποιησουμε για να κανουμε CRUD
    // Καλύτερα να τα κάνω ασύγχρονα.
    public interface ICategoryRepository
    {
        #region "Synchronous Methods"
        public Category Create(Category obj); // Αυτό που περνάμε σαν όρισμα το δημιουργούμε στη βάση
        public Category Update(Category obj);
        public bool Delete(int id); // Is the delete succesfull or not
        public Category Get(int id);
        public IEnumerable<Category> GetAll();
        #endregion

        #region "Asynchronous Methods"
        public Task<Category> CreateAsync(Category obj); // Αυτό που περνάμε σαν όρισμα το δημιουργούμε στη βάση
        public Task<Category> UpdateAsync(Category obj);
        public Task<bool> DeleteAsync(int id); // Is the delete succesfull or not
        public Task<Category> GetAsync(int id);
        public Task<IEnumerable<Category>> GetAllAsync();
        #endregion
    }
}
