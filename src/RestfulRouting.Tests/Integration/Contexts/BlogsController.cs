using System;
using System.Web.Mvc;

namespace RestfulRouting.Tests.Integration.Contexts
{
    public class BlogsController : Controller
    {
        public ActionResult Index()
        {
            return Content("");
        }

        public ActionResult New()
        {
            return Content("");
        }

        public ActionResult Create()
        {
            return Content("");
        }

        public ActionResult Edit(int id)
        {
            return Content("");
        }

        public ActionResult Update(int id)
        {
            return Content("");
        }

        public ActionResult Destroy(int id)
        {
            return Content("");
        }

        public ActionResult Show(int id)
        {
            return Content("");
        }


        public ActionResult Up(int id)
        {
            return Content("");
        }

        public ActionResult Latest()
        {
            return Content("");
        }
    }

    public class BlogAdminController : Controller
    {
        public ActionResult Index()
        {
            return Content("");
        }
    }
}