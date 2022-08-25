using MongoDataAccess.Models;
using MongoDB.Driver;
using MongoDBDataAccess.DataAccess;
using MongoDBDemo;

//string connectionString = "mongodb://127.0.0.1:27017";
//string databaseName = "simple_db";
//string collectionName = "people";

//var client = new MongoClient(connectionString);
//var db = client.GetDatabase(databaseName);
//var collection = db.GetCollection<PersonModel>(collectionName);

//var person = new PersonModel { FirstName = "Jonathan", LastName = "Dias" };

//await collection.InsertOneAsync(person);

//var results = await collection.FindAsync(_ => true);

//foreach (var item in results.ToList())
//{
//    Console.WriteLine($"{item.Id}: {item.FirstName} {item.LastName}");
//}

ChoreDataAccess model = new ChoreDataAccess();

await model.CreateUser(new UserModel() { FirstName = "Jonathan", LastName = "Gonçalves Dias" });
var users = await model.GetAllUsers();

var chore = new ChoreModel()
{
    AssignedTo = users.First(),
    ChoreText = "Cortar a grama",
    FrequencyInDays = 4
};

await model.CreateChore(chore);