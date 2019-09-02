using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RoomPage2Controller : Controller
    {
        private RoomDBContext db = new RoomDBContext();

        private static readonly int PAGE_SIZE = 100;
      
        List<string> Iplist = new List<string>(UserInfo.array);

        private int GetPageCount(int recordCount)
        {
            int pageCount = recordCount / PAGE_SIZE;
            if (recordCount % PAGE_SIZE != 0)
            {
                pageCount += 1;
            }
            return pageCount;
        }

        private List<RoomModels> GetPagedDataSource(IQueryable<RoomModels> room,
        int pageIndex, int recordCount)
        {
            /*
            var pageCount = GetPageCount(recordCount);
            if (pageIndex >= pageCount && pageCount >= 1)
            {
                pageIndex = pageCount - 1;
            }
            */
            return room.OrderBy(m => m.ID)
             .Skip(pageIndex * PAGE_SIZE)
            .Take(PAGE_SIZE).ToList();
        }


        private List<SelectListItem> GetMajorList()
        {
            var majors = db.RoomModels.OrderBy(m => m.Name).Select(m => m.Name).Distinct();

            var items = new List<SelectListItem>();
            foreach (string major in majors)
            {
                items.Add(new SelectListItem
                {
                    Text = major,
                    Value = major
                });
            }
            return items;
        }



        // GET: RoomModels
        public ActionResult Index()
        {
            var room = db.RoomModels as IQueryable<RoomModels>;
            var recordCount = room.Count();
            var pageCount = GetPageCount(recordCount);
            room = room.Where(m => m.Page.Contains("惠南"));
            //ViewBag.PageIndex = 0;
            //ViewBag.PageCount = pageCount;
            //ViewBag.MajorList = GetMajorList();
            //return View(db.RoomModels.ToList());
            return View(GetPagedDataSource(room, 0, recordCount));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Major, string Name, int PageIndex)
        {
            var room = db.RoomModels as IQueryable<RoomModels>;
            if (!String.IsNullOrEmpty(Name))
            {
                room = room.Where(m => m.Name.Contains(Name));
            }

            if (!String.IsNullOrEmpty(Major))
            {
                room = room.Where(m => m.Name == Major);
            }

            var recordCount = room.Count();
            var pageCount = GetPageCount(recordCount);
            if (PageIndex >= pageCount && pageCount >= 1)
            {
                PageIndex = pageCount - 1;
            }

            room = room.OrderBy(m => m.Name)
                 .Skip(PageIndex * PAGE_SIZE).Take(PAGE_SIZE);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageCount = pageCount;

            ViewBag.MajorList = GetMajorList();
            return View(room.ToList());
        }


        // GET: RoomModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModels roomModels = db.RoomModels.Find(id);
            if (roomModels == null)
            {
                return HttpNotFound();
            }
            return View(roomModels);
        }

        // GET: RoomModels/Create
        public ActionResult Create()
        {
            List<SelectListItem> page = new List<SelectListItem>();
            //foreach (var it  in db.Companies)
            {
                page.Add(new SelectListItem { Text = "内部", Value = "内部", Selected = true });
                page.Add(new SelectListItem { Text = "坂田", Value = "坂田" });
                page.Add(new SelectListItem { Text = "惠南", Value = "惠南" });
            }
            ViewBag.page = page;
            return View();
        }

        // POST: RoomModels/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Peopel,Coustomer,StartTime,EndTime,Tips,Status,Page,Person")] RoomModels roomModels)
        {
            if (ModelState.IsValid)
            {
                db.RoomModels.Add(roomModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomModels);
        }

        // GET: RoomModels/Edit/5
        public ActionResult Edit(int? id)
        {
            string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            if (Iplist.Contains(ipAddress))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoomModels roomModels = db.RoomModels.Find(id);

                if (roomModels == null)
                {
                    return HttpNotFound();
                }
                return View(roomModels);
            }
            else
            {
                return View("SkipRoom");
            }
        }

        // POST: RoomModels/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [MultiButton("action1")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Peopel,Coustomer,StartTime,EndTime,Tips,Status,Page,Person")] RoomModels roomModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomModels);
        }

        [HttpPost]
        [MultiButton("action2")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "ID,Name,Peopel,Coustomer,StartTime,EndTime,Tips,Status,Page,Person")] RoomModels roomModels)
        {
            int id = roomModels.ID;
            if (ModelState.IsValid)
            {
                RoomModels room = db.RoomModels.Find(id);
                db.RoomModels.Remove(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomModels);
        }

        // GET: RoomModels/Delete/5
        public ActionResult Delete(int? id)
        {
            string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            if (Iplist.Contains(ipAddress))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoomModels roomModels = db.RoomModels.Find(id);
                if (roomModels == null)
                {
                    return HttpNotFound();
                }
                return View(roomModels);
            }
            else
            {
                return View("SkipRoom");
            }
        }

        public bool WriteHis([Bind(Include = "Id,RoomId,RoomName,Peopel,Coustomer,StartTime,EndTime,Tips,Person")] RoomModels rm)
        {
            HistoryModels room = new HistoryModels();
            room.RoomId = rm.ID;
            room.RoomName = rm.Name;
            room.Peopel = rm.Peopel;
            room.Coustomer = rm.Coustomer;
            room.StartTime = rm.StartTime;
            room.EndTime = rm.EndTime;
            room.Tips = rm.Tips;
            room.Person = rm.Person;
            if (ModelState.IsValid)
            {
                db.HistoryModels.Add(room);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        // POST: RoomModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomModels roomModels = db.RoomModels.Find(id);
            if (WriteHis(roomModels))
            {
                roomModels.Peopel = "";
                roomModels.Status = false;
                Edit(roomModels);
            }
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult DisplayRoom(RoomModels room)
        {
            if (room.Status == false)
            {
                return PartialView("Roomhide", room);
            }
            else
            {
                return PartialView("Roomshow", room);
            }
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
