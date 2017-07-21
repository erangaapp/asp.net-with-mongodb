namespace WebApp
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AspNet.Identity.MongoDB;
	using Models;
	using MongoDB.Driver;

	public class ApplicationIdentityContext : IDisposable
	{
		public static ApplicationIdentityContext Create()
		{
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MongoDb.ConnectionString"].ToString();

            // Todo add settings where appropriate to switch server & database in application

            var client = new MongoClient(connectionString);
            client = new MongoClient(connectionString);
            MongoUrl mongoUrl = MongoUrl.Create(connectionString);
			var database = client.GetDatabase(mongoUrl.DatabaseName);
			var users = database.GetCollection<ApplicationUser>("Users");
			var roles = database.GetCollection<IdentityRole>("Roles");
			return new ApplicationIdentityContext(users, roles);
		}

		private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users, IMongoCollection<IdentityRole> roles)
		{
			Users = users;
			Roles = roles;
		}

		public IMongoCollection<IdentityRole> Roles { get; set; }

		public IMongoCollection<ApplicationUser> Users { get; set; }

		public Task<List<IdentityRole>> AllRolesAsync()
		{
			return Roles.Find(r => true).ToListAsync();
		}

		public void Dispose()
		{
		}
	}
}