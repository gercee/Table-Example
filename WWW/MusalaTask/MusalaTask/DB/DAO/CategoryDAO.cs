using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusalaTask.Models;

namespace MusalaTask.DB.DAO
{
    /// <summary>
    /// This is the DAO for the Category Model.
    /// </summary>
    public interface CategoryDAO
    {
        /// <summary>
        /// Method for inserting Category in the database.
        /// </summary>
        /// <param name="category">Category object.</param>
        void insertCategory(Category category);

        /// <summary>
        /// Method for updating Category in the database.
        /// </summary>
        /// <param name="category">Category object.</param>
        void updateCategory(Category category);

        /// <summary>
        /// Method that returns all categories from the database.
        /// </summary>
        /// <returns>List of categories.</returns>
        List<Category> getAllCategories();

        /// <summary>
        /// Method that returns Category object by categoryID.
        /// </summary>
        /// <param name="categoryID">Id of the Category that we are looking for.</param>
        /// <returns>Category object.</returns>
        Category getCategoryByID(int categoryID);

        /// <summary>
        /// Method that delete category from database by categoryID
        /// </summary>
        /// <param name="categoryID">Id of the category that we want to delete.</param>
        /// <returns>true if the Category is deleted, false if the Category is not deleted.</returns>
        bool deleteCategoryByID(int categoryID);
    }
}
