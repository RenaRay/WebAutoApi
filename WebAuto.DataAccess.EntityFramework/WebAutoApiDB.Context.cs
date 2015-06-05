﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Car_History> Car_History { get; set; }
        public virtual DbSet<CarOwner> CarOwner { get; set; }
        public virtual DbSet<CarOwner_History> CarOwner_History { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Message_History> Message_History { get; set; }
    
        public virtual int AddNewCar(Nullable<int> carID, Nullable<int> carOwnerId, string regNumber, string country, string brand, string model, string color)
        {
            var carIDParameter = carID.HasValue ?
                new ObjectParameter("CarID", carID) :
                new ObjectParameter("CarID", typeof(int));
    
            var carOwnerIdParameter = carOwnerId.HasValue ?
                new ObjectParameter("CarOwnerId", carOwnerId) :
                new ObjectParameter("CarOwnerId", typeof(int));
    
            var regNumberParameter = regNumber != null ?
                new ObjectParameter("RegNumber", regNumber) :
                new ObjectParameter("RegNumber", typeof(string));
    
            var countryParameter = country != null ?
                new ObjectParameter("Country", country) :
                new ObjectParameter("Country", typeof(string));
    
            var brandParameter = brand != null ?
                new ObjectParameter("Brand", brand) :
                new ObjectParameter("Brand", typeof(string));
    
            var modelParameter = model != null ?
                new ObjectParameter("Model", model) :
                new ObjectParameter("Model", typeof(string));
    
            var colorParameter = color != null ?
                new ObjectParameter("Color", color) :
                new ObjectParameter("Color", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewCar", carIDParameter, carOwnerIdParameter, regNumberParameter, countryParameter, brandParameter, modelParameter, colorParameter);
        }
    
        public virtual int InsertUser(Nullable<int> userID, string name, string login, string password, string avatar, string email, string sn_id, Nullable<System.DateTime> fLD, string mS, string job, Nullable<System.DateTime> bD, string gender, string hair, Nullable<int> carID, string regNumber, string country, string brand, string model, string color)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var loginParameter = login != null ?
                new ObjectParameter("Login", login) :
                new ObjectParameter("Login", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var avatarParameter = avatar != null ?
                new ObjectParameter("Avatar", avatar) :
                new ObjectParameter("Avatar", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var sn_idParameter = sn_id != null ?
                new ObjectParameter("sn_id", sn_id) :
                new ObjectParameter("sn_id", typeof(string));
    
            var fLDParameter = fLD.HasValue ?
                new ObjectParameter("FLD", fLD) :
                new ObjectParameter("FLD", typeof(System.DateTime));
    
            var mSParameter = mS != null ?
                new ObjectParameter("MS", mS) :
                new ObjectParameter("MS", typeof(string));
    
            var jobParameter = job != null ?
                new ObjectParameter("Job", job) :
                new ObjectParameter("Job", typeof(string));
    
            var bDParameter = bD.HasValue ?
                new ObjectParameter("BD", bD) :
                new ObjectParameter("BD", typeof(System.DateTime));
    
            var genderParameter = gender != null ?
                new ObjectParameter("gender", gender) :
                new ObjectParameter("gender", typeof(string));
    
            var hairParameter = hair != null ?
                new ObjectParameter("Hair", hair) :
                new ObjectParameter("Hair", typeof(string));
    
            var carIDParameter = carID.HasValue ?
                new ObjectParameter("CarID", carID) :
                new ObjectParameter("CarID", typeof(int));
    
            var regNumberParameter = regNumber != null ?
                new ObjectParameter("RegNumber", regNumber) :
                new ObjectParameter("RegNumber", typeof(string));
    
            var countryParameter = country != null ?
                new ObjectParameter("Country", country) :
                new ObjectParameter("Country", typeof(string));
    
            var brandParameter = brand != null ?
                new ObjectParameter("brand", brand) :
                new ObjectParameter("brand", typeof(string));
    
            var modelParameter = model != null ?
                new ObjectParameter("model", model) :
                new ObjectParameter("model", typeof(string));
    
            var colorParameter = color != null ?
                new ObjectParameter("color", color) :
                new ObjectParameter("color", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertUser", userIDParameter, nameParameter, loginParameter, passwordParameter, avatarParameter, emailParameter, sn_idParameter, fLDParameter, mSParameter, jobParameter, bDParameter, genderParameter, hairParameter, carIDParameter, regNumberParameter, countryParameter, brandParameter, modelParameter, colorParameter);
        }
    
        public virtual ObjectResult<SearchMessages_Result> SearchMessages(string regnumber, Nullable<System.DateTime> start, Nullable<System.DateTime> end)
        {
            var regnumberParameter = regnumber != null ?
                new ObjectParameter("Regnumber", regnumber) :
                new ObjectParameter("Regnumber", typeof(string));
    
            var startParameter = start.HasValue ?
                new ObjectParameter("Start", start) :
                new ObjectParameter("Start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("End", end) :
                new ObjectParameter("End", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SearchMessages_Result>("SearchMessages", regnumberParameter, startParameter, endParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int UpdateCar(Nullable<int> carID, Nullable<int> carOwnerId, string regNumber, string country, string brand, string model, string color, Nullable<double> price, string carBody, Nullable<int> prodYear, Nullable<double> run, string transmission, string engine, string drive, Nullable<bool> conflict, Nullable<bool> onSale)
        {
            var carIDParameter = carID.HasValue ?
                new ObjectParameter("CarID", carID) :
                new ObjectParameter("CarID", typeof(int));
    
            var carOwnerIdParameter = carOwnerId.HasValue ?
                new ObjectParameter("CarOwnerId", carOwnerId) :
                new ObjectParameter("CarOwnerId", typeof(int));
    
            var regNumberParameter = regNumber != null ?
                new ObjectParameter("RegNumber", regNumber) :
                new ObjectParameter("RegNumber", typeof(string));
    
            var countryParameter = country != null ?
                new ObjectParameter("Country", country) :
                new ObjectParameter("Country", typeof(string));
    
            var brandParameter = brand != null ?
                new ObjectParameter("Brand", brand) :
                new ObjectParameter("Brand", typeof(string));
    
            var modelParameter = model != null ?
                new ObjectParameter("Model", model) :
                new ObjectParameter("Model", typeof(string));
    
            var colorParameter = color != null ?
                new ObjectParameter("Color", color) :
                new ObjectParameter("Color", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(double));
    
            var carBodyParameter = carBody != null ?
                new ObjectParameter("CarBody", carBody) :
                new ObjectParameter("CarBody", typeof(string));
    
            var prodYearParameter = prodYear.HasValue ?
                new ObjectParameter("ProdYear", prodYear) :
                new ObjectParameter("ProdYear", typeof(int));
    
            var runParameter = run.HasValue ?
                new ObjectParameter("Run", run) :
                new ObjectParameter("Run", typeof(double));
    
            var transmissionParameter = transmission != null ?
                new ObjectParameter("Transmission", transmission) :
                new ObjectParameter("Transmission", typeof(string));
    
            var engineParameter = engine != null ?
                new ObjectParameter("Engine", engine) :
                new ObjectParameter("Engine", typeof(string));
    
            var driveParameter = drive != null ?
                new ObjectParameter("Drive", drive) :
                new ObjectParameter("Drive", typeof(string));
    
            var conflictParameter = conflict.HasValue ?
                new ObjectParameter("Conflict", conflict) :
                new ObjectParameter("Conflict", typeof(bool));
    
            var onSaleParameter = onSale.HasValue ?
                new ObjectParameter("OnSale", onSale) :
                new ObjectParameter("OnSale", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateCar", carIDParameter, carOwnerIdParameter, regNumberParameter, countryParameter, brandParameter, modelParameter, colorParameter, priceParameter, carBodyParameter, prodYearParameter, runParameter, transmissionParameter, engineParameter, driveParameter, conflictParameter, onSaleParameter);
        }
    
        public virtual int UpdateUser(Nullable<int> userrID, Nullable<System.DateTime> fLD, string mS, string job, Nullable<System.DateTime> bD, string gender, string hair)
        {
            var userrIDParameter = userrID.HasValue ?
                new ObjectParameter("UserrID", userrID) :
                new ObjectParameter("UserrID", typeof(int));
    
            var fLDParameter = fLD.HasValue ?
                new ObjectParameter("FLD", fLD) :
                new ObjectParameter("FLD", typeof(System.DateTime));
    
            var mSParameter = mS != null ?
                new ObjectParameter("MS", mS) :
                new ObjectParameter("MS", typeof(string));
    
            var jobParameter = job != null ?
                new ObjectParameter("Job", job) :
                new ObjectParameter("Job", typeof(string));
    
            var bDParameter = bD.HasValue ?
                new ObjectParameter("BD", bD) :
                new ObjectParameter("BD", typeof(System.DateTime));
    
            var genderParameter = gender != null ?
                new ObjectParameter("gender", gender) :
                new ObjectParameter("gender", typeof(string));
    
            var hairParameter = hair != null ?
                new ObjectParameter("Hair", hair) :
                new ObjectParameter("Hair", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateUser", userrIDParameter, fLDParameter, mSParameter, jobParameter, bDParameter, genderParameter, hairParameter);
        }
    }
}