namespace Aetramwork.Controllers
{
    using Aetramwork.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class DatabaseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DatabaseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Connect(DatabaseConnectionModel model)
        {
            _dbContext.Database.EnsureCreated();
            return RedirectToAction("Tables");
        }

        public IActionResult Tables()
        {
            List<string> tableNames = _dbContext.Model.GetEntityTypes()
                .Select(e => e.GetTableName())
                .ToList();

            return View(tableNames);
        }

        public IActionResult Columns(string tableName)
        {
            var columns = _dbContext.Model.FindEntityType(tableName)?.GetProperties().Select(p => p.Name).ToList();
            ViewBag.TableName = tableName;
            return View(columns);
        }


    }
}
   
    



