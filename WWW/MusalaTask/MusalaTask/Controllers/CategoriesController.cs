using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MusalaTask.DB;
using MusalaTask.DB.DAO;
using MusalaTask.Models;

namespace MusalaTask.Controllers
{
    /// <summary>
    /// Api Controller for Categories.
    /// </summary>
    public class CategoriesController : ApiController
    {
        /// <summary>
        /// Get all categories from the database.
        /// </summary>
        /// <returns>List of Category Objects.</returns>
        public List<Category> Get()
        {
            CategoryDAO categoryDao = new CategoryDAOImpl();
            return categoryDao.getAllCategories();
        }

        /// <summary>
        /// Get Caetgory object by categoryID
        /// </summary>
        /// <param name="id">ID of the Category Object.</param>
        /// <returns>Category Object if the there is a Category Object with the specified id in the Database, else return null</returns>
        public Category Get(int id)
        {
            CategoryDAO categoryDao = new CategoryDAOImpl();
            return categoryDao.getCategoryByID(id);
        }

        /// <summary>
        /// Insert Category Object in the database.
        /// </summary>
        /// <param name="value">Category Object.</param>
        public void Post([FromBody]Category value)
        {
            CategoryDAO categoryDao = new CategoryDAOImpl();
            categoryDao.insertCategory(value);
        }

        /// <summary>
        /// Update Category Object in the database.
        /// </summary>
        /// <param name="value">Category Object.</param>
        public void Put([FromBody]Category value)
        {
            CategoryDAO categoryDao = new CategoryDAOImpl();
            categoryDao.updateCategory(value);
        }

        /// <summary>
        /// Delete Category Object from the database with the sepcified ID.
        /// </summary>
        /// <param name="id">CategoryID for the Category object that will be deleted.</param>
        public void Delete(int id)
        {
            CategoryDAO categoryDao = new CategoryDAOImpl();
            categoryDao.deleteCategoryByID(id);
        }
    }
}
