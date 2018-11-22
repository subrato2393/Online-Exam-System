using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineExams.Models
{
    public class Organization
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Org_Name { get; set; }
        [DisplayName("Code")]
        public string Org_Code { get; set; }
        public string Address { get; set; }
        [DisplayName("Contact")]
        public string ContactNo { get; set; }
        [AllowHtml]
        public string About { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase Logo { get; set; }
        public virtual List<Course> Courses { get; set; }
        public virtual List<Trainer> Trainers { get; set; }
      
    }
}
