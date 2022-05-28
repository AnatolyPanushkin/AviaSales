using AviaSales.Data.Models;
using GraphQL.Types;

namespace AviaSales.GraphQL.GraphTypes
{
    public class TownGraphType:ObjectGraphType<Town>
    {
        public TownGraphType()
        {
            Name = "Town";
            Field(p => p.Id, true).Description("ID of town");
            Field(p => p.Name, false);
        }
    }
}