using Logistics.Integrate.Opportunities;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Logistics.Dynamics.Opportunity.Managers
{
    public class OpportunityManager
    {

        public void CreateOpportunityID(IExecutionContext context, IOrganizationService service)
        {
            Entity opportunity = (Entity)context.InputParameters["Target"];
            if (opportunity.LogicalName != "opportunity" || context.MessageName.ToLower() != "create") return;
            try
            {
                string opportunityId;

                Repository.OpportunityRepository opportunityRepository = new Repository.OpportunityRepository();
                var serviceFromDynamics2 = Connection.CrmService();
                EntityCollection opportunitiesCollection = opportunityRepository.RetrieveIDs(service);

                List<string> existentIds = new List<string>();

                foreach (var entity in opportunitiesCollection.Entities)
                {
                    if (entity.Contains("log_id"))
                    {
                        existentIds.Add(entity["log_id"].ToString());
                    }
                }

                while (true)
                {
                    Utils.IdGenerator idGenerator = new Utils.IdGenerator();
                    opportunityId = idGenerator.GenerateOpportunityID();

                    if (existentIds.Count > 0)
                    {
                        if (!existentIds.Contains(opportunityId)) break;
                    }
                    else
                    {
                        break;
                    }
                }

                opportunity["log_id"] = opportunityId;
                opportunity["log_integrado"] = true;
                service.Update(opportunity);

                Entity oppDynamics2 = new Entity("opportunity");

                oppDynamics2["name"] = opportunity["name"];
                oppDynamics2["log_id"] = opportunity["log_id"];

                serviceFromDynamics2.Create(oppDynamics2);
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
