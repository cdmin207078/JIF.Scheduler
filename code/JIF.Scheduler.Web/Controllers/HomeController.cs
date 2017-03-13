using JIF.Scheduler.Core.Domain;
using JIF.Scheduler.Core.Services.Jobs;
using JIF.Scheduler.Core.Services.Jobs.Dtos;
using System;
using System.Web.Mvc;

namespace JIF.Scheduler.Web.Controllers
{
    public class HomeController : BaseController
    {
        private JobInfoServices _jobInfoService;
        private readonly SchedulerContainer _schedulerContainer;

        public HomeController(JobInfoServices jobInfoService,
            SchedulerContainer schedulerContainer)
        {
            _jobInfoService = jobInfoService;
            _schedulerContainer = schedulerContainer;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Jobs = _jobInfoService.GetAllJobs();

            return View();
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            ViewBag.Job = _jobInfoService.Get(id);

            return View();
        }

        [HttpPost]
        public ActionResult Detail(string id, JobInfoUpdateInputModel model)
        {
            _jobInfoService.Update(id, model);

            return Detail(id);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Job = new JobInfo();

            return View("Detail");
        }

        [HttpPost]
        public ActionResult Add(JobInfoUpdateInputModel model)
        {
            _jobInfoService.Add(model);

            return RedirectToAction("Index");
        }

        // 暂停单个 Job
        [HttpPost]
        public JsonResult PauseJob(string id)
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
        public JsonResult ResumeJob(string id)
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

        // 删除Job
        [HttpPost]
        public ActionResult Del(string id)
        {
            _jobInfoService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}