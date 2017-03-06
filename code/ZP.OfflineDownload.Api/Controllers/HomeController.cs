using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;
using System.Threading;

namespace ZP.OfflineDownload.Api.Controllers
{
    public class HomeController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public IHttpActionResult Info()
        {
            logger.Info("Info-Method");

            return Ok("Info-Method");
        }

        [HttpGet]
        public IHttpActionResult Error()
        {
            logger.Error("Error-Method");

            return Ok("Error-Method");
        }

        [HttpGet]
        public IHttpActionResult Debug()
        {
            logger.Debug("Debug-Method");

            return Ok("Debug-Method");
        }

        [HttpGet]
        public IHttpActionResult Trace()
        {
            logger.Trace("Trace-Method");

            return Ok("Trace-Method");
        }

        [HttpGet]
        public IHttpActionResult Warn()
        {
            logger.Warn("Warn-Method");

            return Ok("Warn-Method");
        }
    }
}
