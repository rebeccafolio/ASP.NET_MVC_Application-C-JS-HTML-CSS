using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITP245_Model
{
    public interface IEmail
    {
        string Email { get; }
        string Contact { get; }       
    }


    [MetadataType(typeof(PurchaseOrderMetaData))]

    public partial class PurchaseOrder
    {
        private sealed class PurchaseOrderMetaData
        {
            [DataType(DataType.Date)]
            [Display(Name = "Purchase Date")]
            public string PoDate { get; set; }

            [Display(Name = "Purchase Order #")]
            public string PurchaseOrderNumber { get; set; }

            [DataType(DataType.Currency)]
            [Display(Name = "Total Amount")]
            public string Amount { get; set; }

        }
    }


    [MetadataType(typeof(VendorMetaData))]

    public partial class Vendor : IEmail
    {
        private sealed class VendorMetaData
        {
            [Display(Name = "Vendor Name")]
            public string Name { get; set; }

            [DataType(DataType.PostalCode)]
            [Display(Name = "Zip Code")]
            public string ZipCode { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Address 1")]
            public string Address1 { get; set; }

            [Display(Name = "Address 2")]
            public string Address2 { get; set; }
                        
            [Display(Name = "Vendor")]
            public string VendorId { get; set; }
                    
            [Display(Name = "Email Address")]
            public string Email { get; set; }
        }
    }

    
    [MetadataType(typeof(SpoilageMetaData))]

    public partial class Spoilage
    {
        private sealed class SpoilageMetaData
        {
            [DataType(DataType.Date)]
            [Display(Name = "Date")]
            public string SpoilageDate { get; set; }

            [Display(Name = "Reason")]
            public string ReasonType { get; set; }

            [Display(Name = "Spoilage")]
            public string SpoilageId { get; set; }

            [DataType(DataType.Currency)]
            [Display(Name = "Cost")]
            public string Value { get; set; }

        }
    }

    [MetadataType(typeof(CategoryMetaData))]

    public partial class Category
    {
        private sealed class CategoryMetaData
        {
            [Display(Name = "Category")]
            public string Name { get; set; }

            [Display(Name = "Category Name")]
            public string CategoryId { get; set; }

        }
    }

    [MetadataType(typeof(ItemMetaData))]

    public partial class Item
    {
        public HttpPostedFileBase FileName { get; set; }

        private sealed class ItemMetaData
        {
            


            [Display(Name = "Quantity in Stock")]
            public string QuantityOnHand { get; set; }
            
            [DataType(DataType.Currency)]
            [Display(Name = "Retail Price")]
            public string RetailPrice { get; set; }

            [DataType(DataType.Currency)]
            [Display(Name = "Standard Cost")]
            public string StandardCost { get; set; }

            [Display(Name = "Location of Image")]
            public string ImageLocation { get; set; }

            [Display(Name = "Name of Item")]
            public string Name { get; set; }

            [Display(Name = "Item")]
            public string ItemId { get; set; }
        }
    }

   

    public partial class PoItem
    {
        
        public int OrderItem { get; set; }
        public static List<PoItem> Fill(PurchaseOrder poitemqty)
        {
            var itemqties = poitemqty.PoItems.ToList();           
            var itemIds = itemqties.Select(i => i.ItemId).ToArray();
            using (var db = new InventoryEntities())
            {
                var itemNotonOrder = db.Items
                    .Where(o => !itemIds.Contains(o.ItemId))
                    .ToList();
                foreach (var item in itemNotonOrder)
                {
                    poitemqty.PoItems.Add(new PoItem()
                    {
                        PurchaseOrderNumber = poitemqty.PurchaseOrderNumber,
                        PurchaseOrder = poitemqty,
                        ItemId = item.ItemId,
                        Item = item,                       
                        OrderItem = 0 

                    });
                }
            }
            return itemqties;              

        }
    }
}
