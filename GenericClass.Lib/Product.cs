namespace GenericClass.Lib;

public class Product
{
    #region properties
    public int ProductId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    #endregion

    #region Constructors

    public Product(int productId, string title, decimal price)
    {
        ProductId = productId;
        Title = title;
        Price = price;
    }
    #endregion
}