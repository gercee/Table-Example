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
    /// Api Controller for the Documents
    /// </summary>
    public class DocumentsController : ApiController
    {
        /// <summary>
        /// Get all documents from database.
        /// </summary>
        /// <returns>List of Document Objects.</returns>
        public List<Document> Get()
        {
            DocumentDAO dbController = new DocumentDAOImpl();
            List<Document> listDocs = dbController.getAllDocuments();
            return listDocs;
        }

        /// <summary>
        /// Get Document Object from the database for the specified ID.
        /// </summary>
        /// <param name="id">ID of the Document Object.</param>
        /// <returns>Document Object if the there is a Document Object with the specified id in the Database, else return null</returns>
        public Document Get(int id)
        {
            DocumentDAO dbController = new DocumentDAOImpl();
            Document tmpDoc = dbController.getDocumentByID(id);
            return tmpDoc;
        }

        /// <summary>
        /// Insert Document Object in the database.
        /// </summary>
        /// <param name="value">Document Object.</param>
        public void Post([FromBody]Document value)
        {
            DocumentDAO dbController = new DocumentDAOImpl();
            dbController.insertDocument(value);
        }

        /// <summary>
        /// Update Document Object in the database.
        /// </summary>
        /// <param name="value">Document Object.</param>
        public void Put([FromBody]Document value)
        {
            DocumentDAO dbController = new DocumentDAOImpl();
            dbController.updateDocument(value);
        }

        /// <summary>
        /// Delete Document Object from the database with the sepcified ID.
        /// </summary>
        /// <param name="id">DocumentID for the Document object that will be deleted.</param>
        public void Delete(int id)
        {
            DocumentDAO dbController = new DocumentDAOImpl();
            dbController.deleteDocumentByID(id);
        }
    }
}
