using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectioString = "Server=Localhost;Port=3306;Database=dbDddApi;Uid=root;Pwd=123456789";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(
              connectioString, ServerVersion.AutoDetect(connectioString));

            //var connectioString = "Server=CONNORYLAPTOP\\OLTP;Database=dbapiddd;User Id=sa;Password=Jmc.123";
            //optionsBuilder.UseSqlServer(connectioString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
