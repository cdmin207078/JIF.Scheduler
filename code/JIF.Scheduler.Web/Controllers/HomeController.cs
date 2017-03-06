using JIF.Scheduler.Core.Infrastructure;
using JIF.Scheduler.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIF.Scheduler.Web.Controllers
{
    public class HomeController : BaseController
    {
        private JobInfoServices _jobInfoService;

        public HomeController(JobInfoServices jobInfoService,
           SchedulerContext schedulerContext)
        {
            _jobInfoService = jobInfoService;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Jobs = _jobInfoService.GetAllJobs();

            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Job = _jobInfoService.Get(id);

            return View();
        }

        // 暂停单个 Job
        [HttpPost]
        public JsonResult PauseJob(int id)
        {
            try
            {
                _jobInfoService.StopJob(id);

                return AjaxOk();
            }
            catch (Exception ex)
            {
                return AjaxFail(ex.Message);
            }
        }

        // 启动单个 Job
        [HttpPost]
        public JsonResult StartJob(int id)
        {
            try
            {
                _jobInfoService.StartUpJob(id);

                return AjaxOk();
            }
            catch (Exception ex)
            {
                return AjaxFail(ex.Message);
            }
        }


        [HttpPost]
        public JsonResult PauseAll()
        {
            return AjaxOk();
        }

        [HttpPost]
        public JsonResult ResumeAll()
        {
            return AjaxOk();
        }
    }
}