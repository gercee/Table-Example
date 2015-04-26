using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusalaTask.Models;

namespace MusalaTask.DB.DAO
{
    /// <summary>
    /// This is the DAO for the Document model.
    /// </summary>
    public interface DocumentDAO
    {
        /// <summary>
        /// Method for inserting Document in the database.
        /// </summary>
        /// <param name="document">Document object.</param>
        void insertDocument(Document document);

        /// <summary>
        /// Method for updating Document in the database.
        /// </summary>
        /// <param name="document">Document object.</param>
        void updateDocument(Document document);

        /// <summary>
        /// Method that return list of all Documents in the database.
        /// </summary>
        /// <returns>List of all Documents.</returns>
        List<Document> getAllDocuments();

        /// <summary>
        /// Method that return Document by specified documentID from the database.
        /// </summary>
        /// <param name="documentID">Id of the documnet that we are looking for.</param>
        /// <returns>Document object.</returns>
        Document getDocumentByID(int documentID);

        /// <summary>
        /// Method that delete Document from the database by documentID.
        /// </summary>
        /// <param name="documentID">Id of the Document that we want to delete.</param>
        /// <returns>true if the Document is deleted, false if the Document is not deleted.</returns>
        bool deleteDocumentByID(int documentID);
    }
}
