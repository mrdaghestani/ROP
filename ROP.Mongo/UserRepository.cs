using System;
using System.Threading.Tasks;
using ROP.Models;
using ROP.Services.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace ROP.Mongo
{
    public class UserRepository : RepositoryBase<Models.User, int>, IUserRepository
    {
        public UserRepository(IMongoDatabaseSettings settings)
        : base(settings)
        {
        }

        protected override string CollectionName => Settings.UsersCollectionName;

        public async Task<User> GetByMobileNumber(string mobileNumber)
        {
            var cursor = await Collection.FindAsync(x => x.MobileNumber == mobileNumber);
            var item = await cursor.SingleOrDefaultAsync();
            return item;
        }
    }
}