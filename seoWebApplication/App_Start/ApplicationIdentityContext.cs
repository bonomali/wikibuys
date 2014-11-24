namespace seoWebApplication
{
	using System;
	using AspNet.Identity.MongoDB;
	using MongoDB.Driver;
    using System.Configuration;

	public class ApplicationIdentityContext : IdentityContext, IDisposable
	{ 
        static string connectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
         
		public ApplicationIdentityContext(MongoCollection users, MongoCollection roles) : base(users, roles)
		{
		}

		public static ApplicationIdentityContext Create()
		{
			// todo add settings where appropriate to switch server & database in your own application

            MongoUrl con = new MongoUrl(connectionString);
            MongoClient client = new MongoClient(con);
            MongoServer mongoServer = client.GetServer();
            MongoDatabase database = mongoServer.GetDatabase(ConfigurationManager.AppSettings["DatabaseId"]); 

            var users = database.GetCollection<IdentityUser>("users");
            var roles = database.GetCollection<IdentityRole>("roles");
			return new ApplicationIdentityContext(users, roles);

            
		}

		public void Dispose()
		{
		}
	}
}