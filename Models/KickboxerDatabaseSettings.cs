using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KickboxerApi.Models
{
    public class KickboxerDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string VideosCollectionName { get; set; } = null!;
        public string TestCollectionName { get; set; } = null!;
    }
}