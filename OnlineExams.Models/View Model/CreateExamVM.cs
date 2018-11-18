using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExams.Models.View_Model
{
    public class CreateExamVM
    {
    
        public string ExamType { get; set; }
        public string ExamCode { get; set; }
        public string Topic { get; set; }
        public double FullMarks { get; set; }
        public string Duration { get; set; }
        public int CourseId { get; set; }
    }
}
