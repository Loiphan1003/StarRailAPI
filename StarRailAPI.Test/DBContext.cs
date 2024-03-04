using Microsoft.EntityFrameworkCore;
using StarRailAPI.Data;

namespace StarRailAPI.Test
{
    public sealed class DBContext
    {
        private static StarRailContext? _instance;
        private static readonly object _lock = new object();

        private DBContext()
        {

        }

        public static StarRailContext GetDBContext()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    var options = new DbContextOptionsBuilder<StarRailContext>()
                        .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                        .Options;

                    _instance = new StarRailContext(options);
                }

                return _instance;
            }
        }
    }
}