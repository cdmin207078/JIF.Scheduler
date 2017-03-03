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
        private SchedulerContext _schedulerContext;

        public HomeController(JobInfoServices jobInfoService,
           SchedulerContext schedulerContext)
        {
            _jobInfoService = jobInfoService;
            _schedulerContext = schedulerContext;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Jobs = _jobInfoService.GetJobs();

            return View();
        }

        public ActionResult Detail(string id)
        {
            ViewBag.Job = _jobInfoService.Get(id);

            return View();
        }

        // 暂停单个 Job
        [HttpPost]
        public JsonResult PauseJob(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return AjaxFail("任务编号不能为空");

            try
            {

                // http://stackoverflow.com/questions/1933676/quartz-java-resuming-a-job-excecutes-it-many-times
                // 恢复之后多次触发原因
                _schedulerContext.Scheduler.PauseJob(new Quartz.JobKey(id, "httpservice-job"));
                _schedulerContext.Scheduler.PauseTrigger(new Quartz.TriggerKey(id, "httpservice-trigger"));

                return AjaxOk();
            }
            catch (Exception ex)
            {
                return AjaxFail(ex.Message);
            }
        }

        // 启动单个 Job
        [HttpPost]
        public JsonResult StartJob(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return AjaxFail("任务编号不能为空");

            try
            {
                _schedulerContext.Scheduler.ResumeJob(new Quartz.JobKey(id, "httpservice-job"));
                _schedulerContext.Scheduler.ResumeTrigger(new Quartz.TriggerKey(id, "httpservice-trigger"));

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