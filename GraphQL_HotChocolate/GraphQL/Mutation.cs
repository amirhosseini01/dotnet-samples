using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.GraphQL.Platforms;

namespace GraphQL_HotChocolate.GraphQL;
public class Mutation
{
    [UseDbContext(typeof(AppDbContext))]
   public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context)
   {
       return null;
   }
}