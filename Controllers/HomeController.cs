using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using AgendaTelefonica.Models;
using System.Threading.Tasks;

namespace AgendaTelefonica.Controllers
{
    public class HomeController : Controller
    {
        private IMongoCollection<Pessoa> people;

        public HomeController(MongoClient client)
        {
            var database = client.GetDatabase("AgendaTelefonicaDb");
            people = database.GetCollection<Pessoa>(nameof(people));
        }
        // GET: People
        public async Task<ActionResult> Index()
        {
            var allPeopleCursor = await people.FindAsync(FilterDefinition<Pessoa>.Empty);
            var allPeople = await allPeopleCursor.ToListAsync();
            return View(allPeople);
        }

        // GET: People/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var person = await people.FindAsync(Builders<Pessoa>.Filter.Eq(p => p.Id, ObjectId.Parse(id)));
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Pessoa person)
        {
            await people.InsertOneAsync(person);
            return RedirectToAction(nameof(Index));
        }

        // GET: People/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var personCursor = await people.FindAsync(Builders<Pessoa>.Filter.Eq(p => p.Id, ObjectId.Parse(id)));
            var person = await personCursor.FirstOrDefaultAsync();
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, Pessoa person)
        {
            if (!ModelState.IsValid)
                return View();
            person.Id = ObjectId.Parse(id);
            await people.ReplaceOneAsync(Builders<Pessoa>.Filter.Eq(p => p.Id, person.Id), person);
            return RedirectToAction(nameof(Index));
        }

        // GET: People/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            await people.DeleteOneAsync(Builders<Pessoa>.Filter.Eq(p => p.Id, ObjectId.Parse(id)));
            return RedirectToAction(nameof(Index));
        }
    }
}