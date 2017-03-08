using JIF.Scheduler.Core.Services.Jobs;
using JIF.Scheduler.Core.Services.Jobs.Dtos;
using System;
using System.Web.Mvc;

namespace JIF.Scheduler.Web.Controllers
{
    public class HomeController : BaseController
    {
        private JobInfoServices _jobInfoService;

        public HomeController(JobInfoServices jobInfoService)
        {
            _jobInfoService = jobInfoService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Jobs = _jobInfoService.GetAllJobs();

            return View();
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            ViewBag.Job = _jobInfoService.Get(id);

            return View();
        }

        [HttpPost]
        public ActionResult Detail(int id, JobInfoUpdateInputModel model)
        {
            _jobInfoService.Update(id, model);

            return Detail(id);
        }

        // 暂停单个 Job
        [HttpPost]
        public JsonResult PauseJob(int id)
        {
            try
            {
                _jobInfoService.PauseJob(id);

                return AjaxOk();
            }
            catch (Exception ex)
            {
                return AjaxFail(ex.Message);
            }
        }

        // 启动单个 Job
        [HttpPost]
        public JsonResult ResumeJob(int id)
        {
            try
            {
                _jobInfoService.ResumeJob(id);

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