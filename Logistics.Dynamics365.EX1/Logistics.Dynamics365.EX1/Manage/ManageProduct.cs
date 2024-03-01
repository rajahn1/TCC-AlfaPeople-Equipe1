using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Dynamics365.EX1.Manage
{
    internal class ManageProduct
    {
        public static void DeleteRegister(IOrganizationService service, ITracingService trace, Guid productId)
        {
            try
            {
                service.Delete("product", productId);
            }
            catch (Exception e)
            {
                trace.Trace($"Error in Delete operation in dynamics 2: {e}");
            }
        }


        public static void CreateRegister(IOrganizationService service, ITracingService trace, Dictionary<string, object> validReferences)
        {
            Entity product = new Entity("product");
            foreach (var validReference in validReferences)
            {
                trace.Trace(validReference.Key, validReference.Value);
                product[validReference.Key] = validReference.Value;

            }
            try
            {
                Guid id = service.Create(product);
            }
            catch (Exception e)
            {
                trace.Trace($"Error in the Create operation in dynamics 2: {e}");
            }

        }

        public static void UpdateRegister(IOrganizationService service, ITracingService trace, Guid productId, string productName, string productNumber, EntityReference editableParentControl, DateTime validfromdate, DateTime validtodate, string description, EntityReference defaultuomscheduleid, string defaultuomid, string pricelevelid, int quantitydecimal, EntityReference subjectid)
        {
            Entity product = new Entity("product");
            product.Id = productId;
            product["name"] = productName;
            product["productnumber"] = productNumber;
            product["editableParentControl"] = editableParentControl;
            product["validfromdate"] = validfromdate;
            product["validtodate"] = validtodate;
            product["description"] = description;
            product["defaultuomscheduleid"] = defaultuomscheduleid;
            product["defaultuomid"] = defaultuomid;
            product["pricelevelid"] = pricelevelid;
            product["quantitydecimal"] = quantitydecimal;
            product["subjectid"] = subjectid;
            try
            {
                service.Update(product);
            }
            catch (Exception e)
            {
                trace.Trace($"Error in Update operation in Dynamics 2: {e}");
            }

        }
    }
}
