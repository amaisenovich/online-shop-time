//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShopTime.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public string OrderID { get; set; }
        public string OfferID { get; set; }
        public string ClientID { get; set; }
        public Nullable<System.DateTime> DateAndTime { get; set; }
        public string OrderStatus { get; set; }
    
        public virtual Offer Offer { get; set; }
        public virtual User User { get; set; }
    }
}