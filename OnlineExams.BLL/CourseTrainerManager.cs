using OnlineExam.Repository;
using OnlineExams.Models;
using OnlineExams.Models.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExams.BLL
{
    public class CourseTrainerManager : BaseManager<CourseTrainer>
    {
        public CourseTrainerManager() : base(new CourseTrainerRepository())
        {
        }

        //public bool Add(List<CourseEditVM> courseTrainers)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Add(List<CourseEditVM> courseTrainers)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
