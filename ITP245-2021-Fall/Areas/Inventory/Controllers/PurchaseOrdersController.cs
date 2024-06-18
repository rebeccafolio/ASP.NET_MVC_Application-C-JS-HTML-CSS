using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITP245_Model;

namespace ITP245_2021_Fall.Areas.Inventory.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: Inventory/PurchaseOrders
        public ActionResult Index()
        {
            var purchaseOrders = db.PurchaseOrders.Include(p => p.Vendor);
            return View(purchaseOrders.ToList());
        }

        // GET: Inventory/PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // GET: Inventory/PurchaseOrders/Create
        public ActionResult Create()
        {
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name");
            return View();
        }

        // POST: Inventory/PurchaseOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderNumber,VendorId,PoDate,Amount")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrders.Add(purchaseOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", purchaseOrder.VendorId);
            return View(purchaseOrder);
        }

        public ActionResult _POItem(string parm)
        {
            int qty = 0;
            var parms = parm.Split('|');
            if (parms.Length > 2)
            {
                int purchaseordernumber = Convert.ToInt32(parms[1]);
                int itemId = Convert.ToInt32(parms[0]);
                var poitem = db.PoItems
                    .FirstOrDefault(q => q.ItemId.Equals(itemId) && q.PurchaseOrderNumber.Equals(purchaseordernumber));
                if (poitem != null) 
                {
                    if (!(Convert.ToInt32(parms[2]) > 0))
                        db.PoItems.Remove(poitem);
                }
                else
                {
                    if ((Convert.ToInt32(parms[2]) > 0))
                    {
                        db.PoItems.Add(new PoItem()
                        {
                            Item = db.Items.Find(itemId),
                            PurchaseOrder = db.PurchaseOrders.Find(purchaseordernumber)
                        });
                    }
                }
                db.SaveChanges();

            }
            return View(qty);
                            
            
            
        }

        // GET: Inventory/PurchaseOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", purchaseOrder.VendorId);
            return View(purchaseOrder);
        }

        // POST: Inventory/PurchaseOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseOrderNumber,VendorId,PoDate,Amount")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", purchaseOrder.VendorId);
            return View(purchaseOrder);
        }

        // GET: Inventory/PurchaseOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: Inventory/PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            db.PurchaseOrders.Remove(purchaseOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult POItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder poitemqty = db.PurchaseOrders.Find(id);
            if (poitemqty == null)
            {
                return HttpNotFound();
            }
            ITP245_Model.PoItem.Fill(poitemqty);
           
            return View(poitemqty);

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
