using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db; //instance of ApplicationDbContext

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> CategoryList = _db.Categories;
            return View(CategoryList);
        }

        //GET
        //Create Page 
        public IActionResult Create()
        {
            return View();
        }

        //Put(Update)
        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); //Return 404 Error
            }

            var categoryFromDb = _db.Categories.Find(id); //Finding by ID

            if (categoryFromDb == null) //If ID not found
            {
                return NotFound(); //Return 404 Error
            }

            return View(categoryFromDb);
        }
        //Put
        //Edit Category by Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category data)
        {
            //Custom Validation
            if (data.Name == "" /*data.DisplayOrder.ToString()*//*Converting to string as it is an int*/)
            {
                ModelState.AddModelError("name", "Name field cannot be empty!"); //Custom Error
            }
            if (ModelState.IsValid) /*server side validation to check if everything is okay with Database POST fields etc..*/
            {
                _db.Categories.Update(data);//Adding the data from FORM and Updating it
                _db.SaveChanges();//Saving the data from FORM to database
                return RedirectToAction("Index");//upon successfull form submission, the page will redirect to INDEX
            }

            return View(data);
        }

        //POST
        //Create Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category data)
        {
            //Custom Validation
            if (data.Name == "" /*data.DisplayOrder.ToString()*//*Converting to string as it is an int*/)
            {
                ModelState.AddModelError("name", "Name field cannot be empty!"); //Custom Error
            }
            if (ModelState.IsValid) /*server side validation to check if everything is okay with Database POST fields etc..*/
            {
                _db.Categories.Add(data);//Adding the data from FORM
                _db.SaveChanges();//Saving the data from FORM to database
                return RedirectToAction("Index");//upon successfull form submission, the page will redirect to INDEX
            }

            return View(data);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); //Return 404 error
            }

            var categoryFromDb = _db.Categories.Find(id); //Finding from DB by Id

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(long? id)
        {

            var data = _db.Categories.Find(id); //Finding by id
            if (data == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(data); //Removing data by Id from Database
            _db.SaveChanges();//Saving the data from FORM to database

            return RedirectToAction("Index");//upon successfull form submission, the page will redirect to INDEX                                 
        }


    }
}
