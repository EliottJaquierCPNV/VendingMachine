using System.Globalization;

namespace CPNVVendingMachine
{
    /// <summary>
    /// A machine that sells articles
    /// </summary>
    public class VendingMachine
    {
        private List<VendingArticle> _articles;
        private decimal _currentChange;
        private decimal _currentBalance;
        private List<Purchase> _purchaseHistory = new List<Purchase>();

        /// <summary>
        /// Instanciate a vending machine with a list of articles
        /// </summary>
        /// <param name="articles">The list of vending articles</param>
        public VendingMachine(List<VendingArticle> articles)
        {
            _articles = articles;
        }

        /// <summary>
        /// Get the current change in the vending machine.
        /// </summary>
        /// <returns>The amount of money inserted in the vending machine that has not been used yet.</returns>
        public decimal GetChange()
        {
            return _currentChange;
        }

        /// <summary>
        /// Get the total amount collected
        /// </summary>
        /// <returns>The total amount of money collected</returns>
        public decimal GetBalance()
        {
            return _currentBalance;
        }

        /// <summary>
        /// Insert money in the vending machine
        /// </summary>
        /// <param name="amount">The amount of money to add to the change</param>
        public void Insert(decimal amount)
        {
            _currentChange += amount;
        }

        /// <summary>
        /// Choose an article from the vending machine
        /// </summary>
        /// <param name="code">The unique code of the article to choose</param>
        /// <returns>The result of the choice in form of a english text. Either an error message or a success message.</returns>
        public string Choose(string code)
        {
            var article = _articles.FirstOrDefault(a => a.Code == code);
            if (article == null) return "Invalid selection!";
            if (article.CurrentQuantity == 0) return $"Item {article.Name}: Out of stock!";
            if (_currentChange < article.Price) return "Not enough money!";
            _currentChange -= article.Price;
            _currentBalance += article.Price;
            article.CurrentQuantity--;
            _purchaseHistory.Add(new Purchase(article, DateProvider.ActualDate));
            return $"Vending {article.Name}";
        }

        /// <summary>
        /// Take the purchase history to get the top hours that generated the most revenue
        /// </summary>
        /// <param name="limit">The limit of best hours to return</param>
        /// <returns>The sorted results in form of a list of english text.</returns>
        public string[] GetTopRevenueByHour(int limit = 3)
        {
            List<Tuple<int,decimal>> moneyRevenueByHour = new List<Tuple<int, decimal>>();
            for (int i = 0; i < 24; i++)
            {
                moneyRevenueByHour.Add(new Tuple<int, decimal>(i, _purchaseHistory.Where(p => p.Date.Hour == i).Sum(p => p.Article.Price)));
            }
            moneyRevenueByHour.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            return moneyRevenueByHour.Take(limit).Select(t => $"Hour {t.Item1,2} generated a revenue of {t.Item2.ToString("0.00", CultureInfo.CreateSpecificCulture("en-GB"))}").ToArray();
        }
    }
}
