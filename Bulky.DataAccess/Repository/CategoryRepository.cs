using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository(ApplicationDbContext db)
        : Repository<Category>(db), ICategoryRepository
    {
        private readonly ApplicationDbContext _db = db;

        
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}