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


        [HttpPost]
        public JsonResult PasueJob(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return AjaxFail();

            _schedulerContext.Scheduler.PauseTrigger(new Quartz.TriggerKey(id, "httpservice-trigger"));

            return AjaxOk();
        }
    }
}