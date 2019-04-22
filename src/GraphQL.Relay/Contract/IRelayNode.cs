namespace GraphQL.Relay.Contract
{
    public interface IRelayNode<out T>
    {
        T GetById(string id);
    }
}