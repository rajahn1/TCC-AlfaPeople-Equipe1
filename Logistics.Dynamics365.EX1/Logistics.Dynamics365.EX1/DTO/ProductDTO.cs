using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Dynamics365.EX1.DTO
{
    internal class ProductDTO
    {
        public Dictionary<string, string> PropertyType { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public ProductDTO(IOrganizationService service)
        {
            PropertyType = new Dictionary<string, string>
            {
                {"name", "string"},
                {"productnumber","int"},
                {"quantitydecimal","int"},
                {"isstockitem","bool"},
                {"description","string"},
                {"modifiedon","time"},
                {"validtodate","time"}
            };
            Properties = new Dictionary<string, object>();
            GetDefaultProperties(service);
        }
        public void GetDefaultProperties(IOrganizationService service)
        {
            var context = new OrganizationServiceContext(service);
            var contatos = (from contact in context.CreateQuery("product")
                            where contact["defaultuomid"] != null
                                && contact["defaultuomscheduleid"] != null
                                && contact["organizationid"] != null
                            select new
                            {
                                defaultuomid = contact["defaultuomid"],
                                defaultuomscheduleid = contact["defaultuomscheduleid"],
                                organizationid = contact["organizationid"],
                            }).AsEnumerable().ToList();
            var firstElement = contatos.FirstOrDefault();
            if (firstElement != null)
            {
                Properties.Add("defaultuomid", firstElement.defaultuomid);
                Properties.Add("defaultuomscheduleid", firstElement.defaultuomscheduleid);
                Properties.Add("organizationid", firstElement.organizationid);
            }
        }
        public bool IsverifyPropertyNull(Entity productEntity, string property)
        {
            return productEntity.Contains(property);
        }
        public void TestIfPropertyIsAvailable(Entity productEntity, ITracingService trace)
        {
            foreach (var mykey in PropertyType)
            {
                trace.Trace(mykey.Key);
                trace.Trace(IsverifyPropertyNull(productEntity, mykey.Key).ToString());
                if (IsverifyPropertyNull(productEntity, mykey.Key))
                {
                    continue;
                }
                var answer = PropertyType[mykey.Key];
                if (answer == "string")
                {
                    string mystring = productEntity.GetAttributeValue<string>(mykey.Key);
                    Properties.Add(mykey.Key, mystring);
                }
                if (answer == "int")
                {
                    int myInt = productEntity.GetAttributeValue<int>(mykey.Key);
                    Properties.Add(mykey.Key, myInt);
                }
                if (answer == "EntityReference")
                {
                    Properties.Add(mykey.Key, productEntity.GetAttributeValue<EntityReference>(mykey.Key));
                }
                if (answer == "Entity")
                {
                    Properties.Add(mykey.Key, productEntity.GetAttributeValue<Entity>(mykey.Key));
                }
                if (answer == "bool")
                {
                    Properties.Add(mykey.Key, productEntity.GetAttributeValue<bool>(mykey.Key));
                }
                if (answer == "time")
                {
                    Properties.Add(mykey.Key, productEntity.GetAttributeValue<DateTime>(mykey.Key));
                }
                
            }
            Properties.Add("log_dynamicsid1", productEntity.GetAttributeValue<Guid>("productid").ToString());
            
        }

    }
}
