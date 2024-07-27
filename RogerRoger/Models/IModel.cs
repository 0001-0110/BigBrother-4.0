namespace RogerRoger.Models
{
    public interface IModel<TId>
    {
        TId Id { get; }
    }
}
