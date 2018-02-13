using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Repositories
{
    public class HelperRepository : IHelperRepository
    {      
        public ISessionRepository OpenSession()
        {
            var builder = new DbContextOptionsBuilder<AppContext>();
            builder.UseInMemoryDatabase(databaseName: "db_test");
            
            // builder.UseSqlServer(
            //     "Server=(localdb)\\mssqllocaldb;Database=config;Trusted_Connection=True;MultipleActiveResultSets=true");
 
            var context = new  AppContext(builder.Options);

            return new SessionRepository(context);
        }
    }
}
