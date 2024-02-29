using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Logistics.Dynamics.Opportunity.Repository
{
    public class OpportunityRepository
    {
        public EntityCollection RetrieveIDs(IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("opportunity");
            query.ColumnSet.AddColumns("log_id");
            EntityCollection opportunitiesCollection = service.RetrieveMultiple(query);
            return opportunitiesCollection;
        }
    }
}
