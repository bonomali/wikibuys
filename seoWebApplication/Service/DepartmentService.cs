using MongoDB.Driver;
using MongoDB.Driver.Builders;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using seoWebApplication.st.SharkTankDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Service
{
    public class DepartmentService
    {
         private readonly MongoHelper<Departments> _departments;
         public DepartmentService()
        {
            _departments = new MongoHelper<Departments>();
        }

         public void Create(Departments departments)
        {
            departments.department_id = GetLastId();
            _departments.Collection.Insert(departments);
        }

         public bool Delete(int department_id)
         {
             try
             {
                 var query = Query<Departments>.EQ(e => e.department_id, department_id);
                 _departments.Collection.Remove(query);
                 return true;
             }
             catch
             {
                 return false;
             }
         }

         internal void Update(Departments p)
         {
             var query = Query<Departments>.EQ(e => e.department_id, p.department_id);
             var update = Update<Departments>.Set(e => e.Name, p.Name)
                                            .Set(e => e.Description, p.Description)
                                            .Set(e => e.UpdateENTUserAccountId, p.UpdateENTUserAccountId)
                                            .Set(e => e.UpdateDate, p.UpdateDate);

             _departments.Collection.Update(query, update);
         }

         public IList<Departments> GetDepartments()
        {
            try
            {
                int Id = dBHelper.GetWebstoreId();
               
                return _departments.Collection.Find(Query.EQ("webstore_id", Id)).ToList<Departments>();
            }
            catch (MongoConnectionException)
            {
                return new List<Departments>();
            }
        }
         public Departments GetDepartmentsByGuid(Guid Id)
         {
             try
             { 
                     var query = Query<Departments>.EQ(e => e.Id, Id);
                     var list = _departments.Collection.Find(query).First<Departments>();
                     return list; 

             }
             catch (MongoConnectionException)
             {
                 return new Departments();
             }
         }
         public Departments GetDepartmentsById(int Id)
         {
             try
             {
                 if (Id > 0)
                 {
                     var query = Query.And(
                                     Query<Departments>.EQ(e => e.department_id, Id),
                                     Query<Departments>.EQ(e => e.webstore_id, dBHelper.GetWebstoreId())
                                 );
                     var list = _departments.Collection.Find(query).First<Departments>();
                     return list;
                 }
                 else {
                     return null;
                 }
                 
             }
             catch (MongoConnectionException)
             {
                 return new Departments();
             }
         }

         
        

         public int GetLastId()
        {
            try
            {
                var query = (from d in GetDepartments() orderby d.department_id descending select d).First();

                
                return query.department_id + 1;
            }
            catch {
                return 1;
            }
        }

         internal Departments GetDepartmentsByName(string name)
         {
             try
             {
                     var query = Query<Departments>.EQ(e => e.Name, name);
                     var list = _departments.Collection.Find(query).First<Departments>();
                     return list;
             }
             catch (MongoConnectionException)
             {
                 return new Departments();
             }
         }
    }
}