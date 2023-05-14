namespace CPNVVendingMachine
{
    /// <summary>
    /// A simple article destined to be sold
    /// </summary>
    public class Article
    {
        /// <summary>
        /// The name of the article
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The price of the article
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Instanciate a vending article
        /// </summary>
        /// <param name="name">The name of the article</param>
        /// <param name="price">The price of the article</param>
        public Article(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
