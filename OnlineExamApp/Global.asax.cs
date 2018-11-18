﻿using AutoMapper;
using OnlineExams.Models;
using OnlineExams.Models.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineExamApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TrainerCreateVM, Trainer>();
                cfg.CreateMap<Course,CourseEditVM>();
                cfg.CreateMap<CourseEditVM,Course>();
                cfg.CreateMap<CourseEditVM, CourseTrainer>();
                cfg.CreateMap<CourseTrainer, CourseEditVM>();
                cfg.CreateMap<CourseTrainerVM,CourseTrainer>();
                cfg.CreateMap<CourseTrainer, CourseTrainerVM>();
                cfg.CreateMap<CreateExamVM, Exam>();
                cfg.CreateMap<Exam,CreateExamVM>();
                //cfg.CreateMap<Trainer, TrainerCreateVM>();


            });
        }
    }
}
