using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineExams.BLL;
using OnlineExams.DataContext;
using OnlineExams.Models;
using System.Linq.Dynamic;

namespace OnlineExamApp.Controllers
{
    public class OrganizationsController : Controller
    {
        public OnlineExamDbContext db = new OnlineExamDbContext();
        OrganizationManager _organizationManager = new OrganizationManager();

        // GET: Organizations
        public ActionResult Index()
        {
            return View(db.Organizations.ToList());
        }

        //nazmulhyder : 01.11.18
        [HttpPost]
        public ActionResult GetAllOrganization()
        {

            //server side parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            db.Configuration.ProxyCreationEnabled = false;
            var organizationList = db.Organizations.ToList<Organization>();
            int totalRows = organizationList.Count;
            if (!string.IsNullOrEmpty(searchValue))
            {
                organizationList = organizationList.Where(x => x.Org_Name.ToLower().Contains(searchValue.ToLower())
                                                               || x.Org_Code.ToLower().Contains(searchValue.ToLower())
                                                               || x.About.ToLower().Contains(searchValue.ToLower())
                                                               || x.Address.ToLower().Contains(searchValue.ToLower())
                                                               || x.ContactNo.ToLower().Contains(searchValue.ToLower())).ToList<Organization>();

            }

            int afterFilteringRow = organizationList.Count;
            //sorting
            organizationList = organizationList.OrderBy(sortColumnName + " " + sortDirection).ToList<Organization>();
            //paging
            organizationList = organizationList.Skip(start).Take(length).ToList<Organization>();

            return Json(new { data = organizationList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = afterFilteringRow }, JsonRequestBehavior.AllowGet);
        }

        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        [HttpPost]
        public ActionResult Save(Organization organization)
        {


            bool status = false;
            if (ModelState.IsValid)
            {
                db.Organizations.Add(organization);
                db.SaveChanges();
                status = true;

            }


            return Json(status);
        }
        // GET: Organizations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Organization organization)
        {
            if (ModelState.IsValid)
            {
                AddImages(organization);
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("SearchOrganization");
            }

            return View(organization);
        }
        public void AddImages(Organization organization)
        {
            string fileName = Path.GetFileNameWithoutExtension(organization.Logo.FileName);
            string extension = Path.GetExtension(organization.Logo.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            organization.ImagePath = "/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
            organization.Logo.SaveAs(fileName);
        }
        public ActionResult SearchOrganization()
        {
            var organizations = _organizationManager.GetAll();
            return View(organizations);
        }
        [HttpPost]
        public ActionResult SearchOrganization(string org_name, string address, string org_code, string contactNo)
        {
            var organizations = from m in _organizationManager.GetAll()
                                select m;
            if (!String.IsNullOrEmpty(org_name))
            {
                organizations = organizations.Where(s => s.Org_Name.Contains(org_name)
                                                    || s.Org_Name.ToLower().Contains(org_name)
                );
            }
            if (!String.IsNullOrEmpty(address))
            {
                organizations = organizations.Where(s => s.Address.Contains(address)
                                        || s.Address.ToLower().Contains(address)
                );
            }
            if (!String.IsNullOrEmpty(org_code))
            {
                organizations = organizations.Where(s => s.Org_Code.Contains(org_code)
                 || s.Org_Code.ToLower().Contains(org_code)
                );
            }
            if (!String.IsNullOrEmpty(contactNo))
            {
                organizations = organizations.Where(s => s.ContactNo.Contains(contactNo)
                 || s.ContactNo.ToLower().Contains(contactNo)
                );
            }
            return View(organizations);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return new JsonResult() { Data = organization, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Org_Name,Org_Code,Address,ContactNo,About,ImagePath")] Organization organization)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                status = true;

            }
            return Json(status);
        }
        public ActionResult InformationDetails(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Organization organization = db.Organizations.Find(Id);
            if (organization == null)
            {
                return HttpNotFound();
            }

            return View(organization);
        }


        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            bool status;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
                status = false;
            }
            else
            {
                db.Organizations.Remove(organization);
                db.SaveChanges();
                status = true;

            }
            return new JsonResult() { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
