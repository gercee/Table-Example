using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusalaTask.Models
{
    public class Category
    {
        public int? categoryID { get; set; }
        public string name { get; set; }
        public DateTime? dateCreated { get; set; }
        public DateTime? dateModified { get; set; }
    }
}