﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineExams.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public string CourseDuration { get; set; }
        public double Credit { get; set; }
        public string CourseTag { get; set; }
        public string CourseOutLine { get; set; } 
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<CourseTrainer> CourseTrainers { get; set; }
     
        public virtual List<Exam> Exams { get; set; }
        public virtual List<Batch> Batches { get; set; }
    }
}
