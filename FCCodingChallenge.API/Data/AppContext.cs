using Microsoft.EntityFrameworkCore;

namespace FCCodingChallenge.API.Data
{
    public class AppContext : DbContext
    {
        public AppContext() { }
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}
