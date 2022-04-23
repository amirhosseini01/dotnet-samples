using GraphQL_HotChocolate.Models;

namespace GraphQL_HotChocolate.GraphQL;

public class Subscription
{
    [Subscribe]
    [Topic]
    public Platform OnPlatformAdded([EventMessage] Platform platform)
    {
        return platform;
    }
}