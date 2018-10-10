﻿using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineExams.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string ExamType { get; set; }
        public string ExamCode { get; set; }
        public string Topic { get; set; }
        public double FullMarks { get; set; }
        public string Duration { get; set; }
       
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual List<ExamSchedule> ExamSchedules { get; set; } 

    }
}
