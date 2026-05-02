using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository(ApplicationDbContext db)
        : Repository<Category>(db), ICategoryRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}