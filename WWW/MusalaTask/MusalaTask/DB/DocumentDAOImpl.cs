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
    /// Implementation of the Document DAO logic.
    /// </summary>
    public class DocumentDAOImpl : BaseDAOImpl, DocumentDAO
    {
        /// <summary>
        /// Implementation of the insert logic for Document DAO.
        /// </summary>
        /// <param name="document">Document object.</param>
        public void insertDocument(Document document)
        {
            String query = "INSERT INTO mus_documents (categoryID, name, location, description, dateModified) VALUES (@categoryID, @name, @location, @description, CURRENT_TIMESTAMP)";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@categoryID", document.categoryID);
                    com.Parameters.AddWithValue("@name", document.name);
                    com.Parameters.AddWithValue("@description", document.description);
                    com.Parameters.AddWithValue("@location", document.location);
                    com.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        /// <summary>
        /// Implementation of the update logic for the Document DAO.
        /// </summary>
        /// <param name="document">Document object.</param>
        public void updateDocument(Document document)
        {
            String query = "UPDATE mus_documents SET categoryID = @categoryID, name = @name, description = @description, dateModified = CURRENT_TIMESTAMP WHERE documentID = @documentID";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@documentID", document.documentID);
                    com.Parameters.AddWithValue("@categoryID", document.categoryID);
                    com.Parameters.AddWithValue("@name", document.name);
                    com.Parameters.AddWithValue("@description", document.description);
                    com.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        /// <summary>
        /// Implementation of the method that returns all the documents from the database.
        /// </summary>
        /// <returns>List of Document objects.</returns>
        public List<Document> getAllDocuments()
        {
            String query = "SELECT D.*, C.name as categoryName FROM mus_documents D, mus_category C WHERE D.categoryID = C.categoryID ORDER BY D.documentID DESC";
            List<Document> listDocuments = new List<Document>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    MySqlDataReader dataReader = com.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        con.Close();
                        return new List<Document>();
                    }

                    while (dataReader.Read())
                    {
                        Document currentRow = getDocument(dataReader);
                        listDocuments.Add(currentRow);
                    }

                }
                con.Close();
                return listDocuments;
            }

        }

        /// <summary>
        /// Implementation of the method that return Document object by specified ID.
        /// </summary>
        /// <param name="documentID">ID of the document</param>
        /// <returns>null if the document exists else return Document object.</returns>
        public Document getDocumentByID(int documentID)
        {
            String query = "SELECT D.*, C.name as categoryName FROM mus_documents D, mus_category C WHERE D.categoryID = C.categoryID and D.documentID = @documentID";
            List<Document> listDocuments = new List<Document>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@documentID", documentID);
                    MySqlDataReader dataReader = com.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        con.Close();
                        return null;
                    }

                    while (dataReader.Read())
                    {
                        Document currentRow = getDocument(dataReader);
                        con.Close();
                        return currentRow;
                    }

                }
                con.Close();
                return null;
            }
        }

        /// <summary>
        /// Implementation of the delete Document object by documentID.
        /// </summary>
        /// <param name="documentID">Id of the Document.</param>
        /// <returns>true if the document is deleted, false if the document is not deleted.</returns>
        public bool deleteDocumentByID(int documentID)
        {
            String query = "DELETE FROM mus_documents WHERE documentID = @documentID";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                int result = 0;
                using (MySqlCommand com = new MySqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@documentID", documentID);
                    result = com.ExecuteNonQuery();
                }
                con.Close();
                return (result > 0);
            }
        }


        /// <summary>
        /// Method that initialize Document object from MySqlDataReader.
        /// </summary>
        /// <param name="dataReader">MySqlDataReader Object</param>
        /// <returns>Document Object.</returns>
        private Document getDocument(MySqlDataReader dataReader)
        {
            Document currentRow = new Document();
            currentRow.documentID = getDBInteger("documentID", dataReader);
            currentRow.categoryID = getDBInteger("categoryID", dataReader);
            currentRow.name = getDBString("name", dataReader);
            currentRow.description = getDBString("description", dataReader);
            currentRow.dateModified = getDBDateTime("dateModified", dataReader);
            currentRow.dateCreated = getDBDateTime("dateCreated", dataReader);
            currentRow.categoryName = getDBString("categoryName", dataReader);
            currentRow.location = "UserFiles/documents/" + getDBString("location", dataReader);
            return currentRow;
        }
    }
}