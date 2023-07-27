using System.Globalization;

ProductList productList = new ProductList();
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("To enter a new product - Please enter to follow the steps | to quit - enter: 'Q' or quit");
Console.ResetColor();
while (true)
{
    try
    {
        Console.Write("Enter a Category: ");
        string category = Console.ReadLine();
        if (category.ToLower() == "q")
            break;

        Console.Write("Enter a Product Name: ");
        string productName = Console.ReadLine();
        if (productName.ToLower() == "q")
            break;

        Console.Write("Enter Price: ");
        double price = double.Parse(Console.ReadLine());
        Product product = new Product(category, productName, price);
        productList.AddProduct(product);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The product was successfully added!");
        Console.ResetColor();
    }
    catch (FormatException)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid price format. Please enter a Price.");
        Console.ResetColor();
    }
}
Console.WriteLine("\nProduct List:");
productList.DisplaySortedProducts();
productList.DisplaySum();

// Allow adding more products after presenting the list
while (true)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("\nTo enter a new product - Please enter to 'P' | to search for a Product name - Please enter 'S' | to quit - enter: 'Q' or quit");
    Console.ResetColor();
    string response = Console.ReadLine();

    if (response.ToLower() == "q")
        break;
    if (response.ToLower() == "s")
    {
        Console.WriteLine("To search please enter a Product Name:");
        string searchTerm = Console.ReadLine();
        Console.WriteLine("\nSearch Results:");
        productList.SearchProduct(searchTerm);
        continue;
    }

    if (response.ToLower() != "p")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid!!! Please enter P or S or Q.");
        Console.ResetColor();
        continue;
    }

    try
    {
        Console.Write("Enter Category: ");
        string category = Console.ReadLine();
        if (category.ToLower() == "q")
            break;

        Console.Write("Enter Product Name: ");
        string productName = Console.ReadLine();
        if (productName.ToLower() == "q")
            break;

        Console.Write("Enter Price: ");
        double price = double.Parse(Console.ReadLine());

        Product product = new Product(category, productName, price);
        productList.AddProduct(product);
    }
    catch (FormatException)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid price format. Please enter a valid numeric price.");
        Console.ResetColor();
    }
}



class Product
{
    public string Category { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }

    public Product(string category, string productName, double price)
    {
        Category = category;
        ProductName = productName;
        Price = price;
    }
    public string FormattedPrice => Price.ToString("C", CultureInfo.GetCultureInfo("sv-SE"));//To display total price with Swedish currency
}
class ProductList
{
    private List<Product> products;

    public ProductList()
    {
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void DisplaySortedProducts() // After click q is display the result of list
    {
        Console.WriteLine("--------------------------------------------------------------");
        var sortedProducts = products.OrderBy(p => p.Price);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");
        Console.ResetColor();
        foreach (Product p in sortedProducts)
        {
            Console.WriteLine(p.Category.PadRight(20) + p.ProductName.PadRight(20) + p.FormattedPrice.ToString());
        }
    }
    public void DisplaySum() //Calculate the price
    {
        double totalPrice = products.Sum(p => p.Price);
        Product totalProduct = new Product("", "Total Price:", totalPrice);
        Console.WriteLine("\n");
        Console.WriteLine(($"Total Price: {totalProduct.FormattedPrice}").PadLeft(48));
        Console.WriteLine("--------------------------------------------------------------");
    }
    public void SearchProduct(string searchTerm)//Search method
    {
        foreach (Product product in products)
        {
            if (product.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("--------------------------------------------------------------");
                var sortedProducts = products.OrderBy(p => p.Price); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");
                Console.ResetColor();
                foreach (Product p in sortedProducts)
                {
                    int index = product.ProductName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase);
                    if (index >= 0)
                    {
                        Console.ForegroundColor= ConsoleColor.Magenta;
                        Console.WriteLine(p.Category.PadRight(20) + p.ProductName.PadRight(20) + p.Price.ToString());
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                Console.WriteLine("No matching products found.");
                return;
            }
        }
    }
}