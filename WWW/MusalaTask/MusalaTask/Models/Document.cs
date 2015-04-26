using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MusalaTask.Models
{
    public class Document
    {

        public int? documentID { get; set; }
        public int categoryID { get; set; }
        public String name { get; set; }
        public String categoryName { get; set; }
        public String location { get; set; }
        public String description { get; set; }
        public DateTime? dateCreated { get; set; }
        public DateTime? dateModified { get; set; }

    }
}