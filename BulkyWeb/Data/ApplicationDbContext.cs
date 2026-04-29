
using Microsoft.EntityFrameworkCore;
namespace BulkyWeb.Data
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        :DbContext(options)
    {

    }
}
