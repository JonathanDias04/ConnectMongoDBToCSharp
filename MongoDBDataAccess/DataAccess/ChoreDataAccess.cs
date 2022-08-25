using Microsoft.Extensions.Configuration;
using MongoDataAccess.Models;
using MongoDB.Driver;

namespace MongoDBDataAccess.DataAccess
{
    public class ChoreDataAccess
    {
        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        private const string DatabaseName = "chore_db";
        private const string ChoreCollection = "chore_chart";
        private const string UserCollection = "users";
        private const string CHoreHistoryCollection = "chore_history";
        
        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            var results = await usersCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<List<ChoreModel>> GetAllChores()
        {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var results = await choreCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<List<ChoreModel>> GetAllChoresForAUser(UserModel user)
        {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var results = await choreCollection.FindAsync(
                    chore => chore.AssignedTo.Id == user.Id);
            return results.ToList();
        }

        public Task CreateUser(UserModel user)
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            return usersCollection.InsertOneAsync(user);
        }

        public Task CreateChore(ChoreModel chore)
        {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            return choreCollection.InsertOneAsync(chore);
        }

        public Task UpdateChore(ChoreModel chore)
        {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);
            return choreCollection.ReplaceOneAsync(filter, chore, new ReplaceOptions { IsUpsert = true });
        }

        public Task DeleteChore(ChoreModel chore)
        {
            var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);
            return choreCollection.DeleteOneAsync(c => c.Id == chore.Id);
        }
    }
}
