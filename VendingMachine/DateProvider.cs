namespace CPNVVendingMachine
{
    /// <summary>
    /// A date provider for the vending machine (can be modified for testing purposes)
    /// </summary>
    public static class DateProvider
    {
        private static DateTime _date;
        private static bool _customDateSet = false;

        /// <summary>
        /// Get the current date or override it for testing purposes
        /// </summary>
        public static DateTime ActualDate
        {
            get { 
                return _customDateSet?_date:DateTime.Now; 
            }
#if DEBUG
            set { 
                _date = value;
                _customDateSet = true;
            }
#endif
        }
    }
}
