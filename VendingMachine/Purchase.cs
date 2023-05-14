namespace CPNVVendingMachine
{
    /// <summary>
    /// A purchase made by a customer
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// The article purchased
        /// </summary>
        public Article Article { get; private set; }
        /// <summary>
        /// The date of the purchase
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Create a new purchase
        /// </summary>
        /// <param name="article">The article purchased</param>
        /// <param name="date">The date of the purchase</param>
        public Purchase(Article article, DateTime date)
        {
            Article = article;
            Date = date;
        }
    }
}
