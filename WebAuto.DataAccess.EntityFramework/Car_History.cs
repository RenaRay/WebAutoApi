//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAuto.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car_History
    {
        public int CarID { get; set; }
        public string RegNumber { get; set; }
        public string Country { get; set; }
        public Nullable<int> CarOwnerId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public Nullable<double> Price { get; set; }
        public string CarBody { get; set; }
        public Nullable<int> ProdYear { get; set; }
        public Nullable<double> Run { get; set; }
        public string Transmission { get; set; }
        public string Engine { get; set; }
        public string Drive { get; set; }
        public Nullable<bool> Conflict { get; set; }
        public Nullable<bool> OnSale { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> FlagEffective { get; set; }
    
        public virtual Car Car { get; set; }
    }
}
