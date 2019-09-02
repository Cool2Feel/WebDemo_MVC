using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;

namespace WebApplication1.Controllers
{
    public class RoomPageController : Controller
    {
        private RoomDBContext db = new RoomDBContext();

        private static readonly int PAGE_SIZE = 12;
        
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

        private List<HistoryModels> GetPagedDataSource(IQueryable<HistoryModels> room,
        int pageIndex, int recordCount)
        {
            var pageCount = GetPageCount(recordCount);
            if (pageIndex >= pageCount && pageCount >= 1)
            {
                pageIndex = pageCount - 1;
            }

            return room.OrderBy(m => m.RoomName)
             .Skip(pageIndex * PAGE_SIZE)
            .Take(PAGE_SIZE).ToList();
        }


        private List<SelectListItem> GetMajorList()
        {
            var majors = db.HistoryModels.OrderBy(m => m.RoomName).Select(m => m.RoomName).Distinct();

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
        /*
        // GET: RoomPage
        public ActionResult Index()
        {
            var room = db.HistoryModels as IQueryable<HistoryModels>;
            var recordCount = room.Count();
            var pageCount = GetPageCount(recordCount);

            ViewBag.PageIndex = 0;
            ViewBag.PageCount = pageCount;

            ViewBag.MajorList = GetMajorList();
            
            //return View(db.RoomModels.ToList());
            return View(GetPagedDataSource(room, 0, recordCount));
        }       
        */

        public ActionResult Index(int? p)
        {
            string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            if (Iplist.Contains(ipAddress))
            {
                var room = from s in db.HistoryModels select s;
                ViewBag.MajorList = GetMajorList();
                //第几页
                int pageNumber = p ?? 1;
                //每页显示多少条
                int pageSize = PAGE_SIZE;
                //根据ID排序
                room = room.OrderBy(x => x.ID);
                var pageData = room.ToPagedList(pageNumber, pageSize);

                //将分页处理后的列表传给View
                return View(pageData);
            }
            else
            {
                return View("SkipRoom");
            }
        }

        /*
        public List<HistoryModels> GetOrders(int pageindex, int pagesize, HistoryModels query, ref int count)
        {
            var orders = new List<HistoryModels>();
            var whereStr = string.Empty;

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.RoomName))
                {
                    whereStr += string.Format(" and CustomerName like '%{0}%' ", query.RoomName);
                }
            }

            var cmd = string.Format(@"SELECT COUNT(*) FROM [Orders] WHERE 1=1 {0};
                        SELECT *  FROM (
                        SELECT *, row_number() OVER (ORDER BY orderId DESC ) AS [row] 
                                  FROM [Orders] WHERE 1=1  {0} )t
                        WHERE t.row >@indexMin AND t.row<=@indexMax", whereStr);

            using (IDbConnection conn = BaseDBHelper.GetConn())
            {
                using (var multi = conn.QueryMultiple(cmd,
                   new { indexMin = (pageindex - 1) * pagesize, indexMax = pageindex * pagesize }))
                {
                    count = multi.Read<int>().SingleOrDefault();
                    orders = multi.Read<HistoryModels>().ToList();
                }
            }
            return orders;
        }
        public ActionResult List(ViewModel orderViewModel, int page = 1)
        {
            var pagesize = 10;
            var count = 0;

            var orders = _orderService.GetOrders(page, pagesize, orderViewModel.QueryModel, ref count);

            orderViewModel.OrderList = new StaticPagedList<HistoryModels>(orders, page, pagesize, count);

            return View(orderViewModel);
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Major, string Name, int PageIndex)
        {
            var room = db.HistoryModels as IQueryable<HistoryModels>;
            if (!String.IsNullOrEmpty(Name))
            {
                room = room.Where(m => m.RoomName.Contains(Name));
            }

            if (!String.IsNullOrEmpty(Major))
            {
                room = room.Where(m => m.RoomName == Major);
            }

            int pageSize = PAGE_SIZE;
            room = room.OrderBy(x => x.ID);
            var pageData = room.ToPagedList(PageIndex, pageSize);

            ViewBag.MajorList = GetMajorList();
            //将分页处理后的列表传给View
            return View(pageData);
        }



        // GET: RoomPage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryModels roomModels = db.HistoryModels.Find(id);
            if (roomModels == null)
            {
                return HttpNotFound();
            }
            return View(roomModels);
        }

        // GET: RoomPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomPage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Peopel,Coustomer,StartTime,EndTime,Tips,Person")] HistoryModels roomModels)
        {
            if (ModelState.IsValid)
            {
                //db.HistoryModels.Add(roomModels);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomModels);
        }

        // GET: RoomPage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryModels roomModels = db.HistoryModels.Find(id);
            if (roomModels == null)
            {
                return HttpNotFound();
            }
            return View(roomModels);
        }

        // POST: RoomPage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Peopel,Coustomer,StartTime,EndTime,Tips,Person")] HistoryModels roomModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomModels);
        }

        // GET: RoomPage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoryModels roomModels = db.HistoryModels.Find(id);
            if (roomModels == null)
            {
                return HttpNotFound();
            }
            return View(roomModels);
        }

        // POST: RoomPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoryModels roomModels = db.HistoryModels.Find(id);
            db.HistoryModels.Remove(roomModels);
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
