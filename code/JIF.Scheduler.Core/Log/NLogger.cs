using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Scheduler.Core.Log
{
    public class NLogger : ILog
    {
        Logger _log = LogManager.GetCurrentClassLogger();

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            _log.Debug(message, args);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            _log.Error(message, args);
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(string message, params object[] args)
        {
            _log.Fatal(message, args);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            _log.Info(message, args);
        }

        public void Trace(string message)
        {
            _log.Trace(message);
        }

        public void Trace(string message, params object[] args)
        {
            _log.Trace(message, args);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Warn(string message, params object[] args)
        {
            _log.Warn(message, args);
        }

    }
}
