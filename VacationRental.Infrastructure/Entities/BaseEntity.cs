namespace VacationRental.Infrastructure.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public BaseEntity(TKey key)
        {
            Id = key;
        }
        public TKey Id { get; set; }
    }
}
