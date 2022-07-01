namespace SharedDLL
{
    public interface OrderCreated
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
    }
}
