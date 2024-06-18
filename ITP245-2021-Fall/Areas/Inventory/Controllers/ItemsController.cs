using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITP245_Model;

namespace ITP245_2021_Fall.Areas.Inventory.Controllers
{
    public class ItemsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: Inventory/Items
        public ActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(d => d.Name), "CategoryId", "Name");
            var items = db.Items.Include(i => i.Category);
            return View(items.ToList());
        }

        // GET: Inventory/Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Inventory/Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Inventory/Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,CategoryId,Name,Description,QuantityOnHand,RetailPrice,StandardCost,ImageLocation,FileName")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                if (item.FileName != null)
                    item.ImageLocation = UploadImage(item.FileName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Inventory/Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            return View(item);
        }

        // POST: Inventory/Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CategoryId,Name,Description,QuantityOnHand,RetailPrice,StandardCost,ImageLocation,FileName")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                if (item.FileName != null)
                    item.ImageLocation = UploadImage(item.FileName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Inventory/Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Inventory/Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult _IndexByCategory(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var category = db.Items
                .Include(c => c.Category)
                .Where(c => c.CategoryId.Equals(id))
                .ToArray();
            return PartialView("_Index", category);
        }

        public ActionResult _IndexByName(string parm)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var name = db.Items
                .Include(c => c.Category)
                .Where(c => c.Name.Contains(parm))
                .ToArray();
            return PartialView("_Index", name);
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Images
        private string UploadImage(HttpPostedFileBase file)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var imagePath = ConfigurationManager.AppSettings["ItemImage"];
                    var mapPath = HttpContext.Server.MapPath(imagePath);
                    string path = Path.Combine(mapPath, Path.GetFileName(file.FileName));
                    string ext = Path.GetExtension(file.FileName);
                    if (allowedExtensions.Contains(ext))
                    {
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                        return $"~{imagePath}/{file.FileName}";
                    }
                }

                catch (Exception ex)
                {
                    ViewBag.Message = $"Error: {ex.Message}";
                }
            }

            return string.Empty;
        }
        #endregion
    }
}
