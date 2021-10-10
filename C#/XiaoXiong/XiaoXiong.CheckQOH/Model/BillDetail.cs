using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoXiong.CheckQOH.Model
{
    public class BillDetail
    {
        public int Id { get; set; }
        public string DeliveryOrder { get; set; }
        public string DeliveryStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string ShipToPartnerInterRef { get; set; }
        public string ShipToPartnerName { get; set; }
        public string SalesOrder { get; set; }
        public string SalesPerson { get; set; }
        public string ProductInternalCategory { get; set; }
        public string Product { get; set; }
        public string ProductInternalRef { get; set; }
        public string UoM { get; set; }
        public double? QuantityOrdered { get; set; }
        public double? QuantityDelivered { get; set; }
        public double? QuantityInvoiced { get; set; }
        public decimal? UniPrice { get; set; }
        public decimal? OrderLineSubtotal { get; set; }
        public string Category { get; set; }
        public string ProductMajorClassification { get; set; }
        public double RemainingToShip { get; set; }
        public decimal? RemainingToInvoice { get; set; }
        public decimal? ReservedQuantity { get; set; }
        public decimal? Reserved { get; set; }
        public decimal? Unreserved { get; set; }
        public string StatusMonth { get; set; }
        public string StatusRange { get; set; }
        public string SalesOrderCustomer { get; set; }
    }
}
