using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistics.Dynamics365.EX1.Repository;
using Logistics.Dynamics365.EX1.DTO;
using Logistics.Dynamics365.EX1.Manage;

namespace Logistics.Dynamics365.EX1
{
    public class ProductPlugin : IPlugin
    {

        
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            if (context.PrimaryEntityName == "product")
            {
                if (context.MessageName == "Create")
                {
                    Entity productRecord = (Entity)context.InputParameters["Target"];

                    try
                    {
                        IOrganizationService connectionWithDynamics2 = ConnectionEnvironmentDynamics2.GetService();
                        ProductDTO productDTO = new ProductDTO(connectionWithDynamics2);
                        productDTO.TestIfPropertyIsAvailable(productRecord, tracingService);

                        ManageProduct.CreateRegister(connectionWithDynamics2, tracingService, productDTO.Properties);
                    }
                    catch (Exception e)
                    {
                        tracingService.Trace(e.ToString());

                    }

                }


                if (context.MessageName == "Update")
                {
                    Entity productRecord = (Entity)context.InputParameters["Target"];
                    // this checks all the fields are present
                    if (productRecord.Contains("name")
                    && productRecord.Contains("productnumber")
                    && productRecord.Contains("hierarchypath")
                    && productRecord.Contains("description")
                    && productRecord.Contains("validfromdate")
                    && productRecord.Contains("validtodate")
                    && productRecord.Contains("defaultuomscheduleid")
                    && productRecord.Contains("defaultuomid")
                    && productRecord.Contains("pricelevelld")
                    && productRecord.Contains("quantitydecimal")
                    && productRecord.Contains("subjectId"))
                    {
                        return;
                        string productName = productRecord.GetAttributeValue<string>("name");
                        string productNumber = productRecord.GetAttributeValue<string>("productnumber");
                        EntityReference editableParentControl = productRecord.GetAttributeValue<EntityReference>("editableParentControl");
                        DateTime validfromdate = productRecord.GetAttributeValue<DateTime>("validfromdate");
                        DateTime validtodate = productRecord.GetAttributeValue<DateTime>("validtodate");
                        string description = productRecord.GetAttributeValue<string>("description");
                        EntityReference defaultuomscheduleid = productRecord.GetAttributeValue<EntityReference>("description");
                        string defaultuomid = productRecord.GetAttributeValue<string>("defaultuomid");
                        string pricelevelid = productRecord.GetAttributeValue<string>("pricelevelid");
                        int quantitydecimal = productRecord.GetAttributeValue<int>("quantitydecimal");
                        EntityReference subjectid = productRecord.GetAttributeValue<EntityReference>("subjectid");
                        IOrganizationService serviceDynamics2 = ConnectionEnvironmentDynamics2.GetService();
                        ManageProduct.UpdateRegister(serviceDynamics2, tracingService, productRecord.Id, productName, productNumber, editableParentControl, validfromdate, validtodate, description, defaultuomscheduleid, defaultuomid, pricelevelid, quantitydecimal, subjectid);
                    }

                }

                if (context.MessageName == "Delete")
                {
                    return;
                    Entity productRecord = (Entity)context.InputParameters["Target"];
                    Guid productid = productRecord.GetAttributeValue<Guid>("productid");
                    ManageProduct.DeleteRegister(service, tracingService, productid);
                }

            }

        }
    }
}
