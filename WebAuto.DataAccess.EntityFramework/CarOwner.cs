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
    
    public partial class CarOwner
    {
        public CarOwner()
        {
            this.Car = new HashSet<Car>();
            this.Message = new HashSet<Message>();
        }
    
        public int CarOwnerID { get; set; }
        public string Login { get; set; }
        public int Avatar { get; set; }
        public string Email { get; set; }
        public string sn_id { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
        public Nullable<System.DateTime> FirstLicenseDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Gender { get; set; }
        public string HairColor { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public virtual ICollection<Car> Car { get; set; }
        public virtual ICollection<Message> Message { get; set; }
    }
}
