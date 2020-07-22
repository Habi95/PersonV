using SecurityData.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecurityData.model
{
    public class EmailTemplate
    {
        public int id { get; set; }
        public EDocumentType document_type { get; set; }
        public string text { get; set; }

        [NotMapped]
        public string password { get; set; }

        [NotMapped]
        public string securityWord { get; set; }
    }
}