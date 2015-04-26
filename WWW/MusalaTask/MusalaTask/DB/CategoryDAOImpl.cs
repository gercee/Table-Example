using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusalaTask.DB.DAO;
using MusalaTask.Models;
using MySql.Data.MySqlClient;

namespace MusalaTask.DB
{
    /// <summary>
    /// Implementation of the Category DAO logic.
    /// </summary>
    public class CategoryDAOImpl : BaseDAOImpl, CategoryDAO
    {
        /// <summary>
        /// Implementation of the insert logic for Category DAO.
        /// </summary>
        /// <param name="category">Category object.</param>
        public void insertCategory(Category category)
        {
            String query = "INSERT INTO mus_category (name, dateModified) VALUES (@name, CURRENT_TIMESTAMP)";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@name", category.name);
                    com.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        /// <summary>
        /// Implementation of the update logic for the Category DAO.
        /// </summary>
        /// <param name="category">Category object.</param>
        public void updateCategory(Category category)
        {
            String query = "UPDATE mus_category SET name = @name, dateModified = CURRENT_TIMESTAMP WHERE categoryID = @categoryID";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@categoryID", category.categoryID);
                    com.Parameters.AddWithValue("@name", category.name);
                    com.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        /// <summary>
        /// Implementation of the method that returns all the categories from the database.
        /// </summary>
        /// <returns>List of Categories objects.</returns>
        public List<Category> getAllCategories()
        {
            String query = "SELECT * FROM mus_category ORDER BY categoryID DESC";
            List<Category> listCategories = new List<Category>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    MySqlDataReader dataReader = com.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        con.Close();
                        return new List<Category>();
                    }

                    while (dataReader.Read())
                    {
                        Category currentRow = getCategory(dataReader);
                        listCategories.Add(currentRow);
                    }

                }
                con.Close();
                return listCategories;
            }

        }

        /// <summary>
        /// Implementation of the method that return Category object by specified ID.
        /// </summary>
        /// <param name="categoryID">Id of the Category.</param>
        /// <returns>null if the categoryID exists else return Category Object.</returns>
        public Category getCategoryByID(int categoryID)
        {
            String query = "SELECT * FROM mus_category WHERE categoryID = @categoryID";
            List<Category> listDocuments = new List<Category>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@categoryID", categoryID);
                    MySqlDataReader dataReader = com.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        con.Close();
                        return null;
                    }

                    while (dataReader.Read())
                    {
                        Category currentRow = getCategory(dataReader);
                        con.Close();
                        return currentRow;
                    }

                }
                con.Close();
                return null;
            }
        }

        /// <summary>
        /// Implementation of the delete Category object by categoryID.
        /// </summary>
        /// <param name="categoryID">ID of the Category.</param>
        /// <returns>true if is deleted, false if is not deleted.</returns>
        public bool deleteCategoryByID(int categoryID)
        {
            String query = "DELETE FROM mus_category WHERE categoryID = @categoryID";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                int result = 0;
                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@categoryID", categoryID);
                    result = com.ExecuteNonQuery();
                }
                con.Close();
                return (result > 0);
            }
        }

        /// <summary>
        /// Method that initialize Category object from MySqlDataReader.
        /// </summary>
        /// <param name="dataReader">MySqlDataReader Object</param>
        /// <returns>Category Object.</returns>
        private Category getCategory(MySqlDataReader dataReader)
        {
            Category currentRow = new Category();
            currentRow.categoryID = getDBInteger("categoryID", dataReader);
            currentRow.name = getDBString("name", dataReader);
            currentRow.dateModified = getDBDateTime("dateModified", dataReader);
            currentRow.dateCreated = getDBDateTime("dateCreated", dataReader);

            return currentRow;
        }
    }
}