namespace CPNVVendingMachine
{
    /// <summary>
    /// A article in a vending machine context
    /// </summary>
    public class VendingArticle : Article
    {
        /// <summary>
        /// A unique code for the article
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// The current quantity of the article
        /// </summary>
        public int CurrentQuantity { get; set; }

        /// <summary>
        /// Instanciate a vending article
        /// </summary>
        /// <param name="name">The name of the article</param>
        /// <param name="code">A unique code for the article</param>
        /// <param name="price">The price of the article</param>
        /// <param name="initialQuantity">The initial quantity of the article in the stock</param>
        public VendingArticle(string name, string code, decimal price, int initialQuantity):base(name,price)
        {
            Code = code;
            CurrentQuantity = initialQuantity;
        }
    }
}
