namespace CPNVVendingMachine
{
    /// <summary>
    /// Unit tests for the vending machine
    /// </summary>
    public class VendingMachineTests
    {
        private VendingMachine _vendingMachine;
        [SetUp]
        public void Setup()
        {
            List<VendingArticle> articles = new List<VendingArticle>{ 
                new VendingArticle("Smarlies","A01",1.6m,10), 
                new VendingArticle("Carampar","A02",.6m,5),
                new VendingArticle("Avril","A03",2.1m,2),
                new VendingArticle("KokoKola","A04",2.95m,1)
            };
            _vendingMachine = new VendingMachine(articles);
        }

        [Test]
        public void VendingMachine_ChooseA01_NominalCase()
        {
            //Given
            _vendingMachine.Insert(3.4m);
            //When
            string result = _vendingMachine.Choose("A01");
            //Then
            Assert.That(result, Is.EqualTo("Vending Smarlies"));
            Assert.That(_vendingMachine.GetChange(), Is.EqualTo(1.8m));
        }

        [Test]
        public void VendingMachine_ChooseA03_NominalCase()
        {
            //Given
            _vendingMachine.Insert(2.1m);
            //When
            string result = _vendingMachine.Choose("A03");
            //Then
            Assert.That(result, Is.EqualTo("Vending Avril"));
            Assert.That(_vendingMachine.GetChange(), Is.EqualTo(0m));
            Assert.That(_vendingMachine.GetBalance(), Is.EqualTo(2.1m));
        }

        [Test]
        public void VendingMachine_ChooseA01_NotEnoughMoney()
        {
            //Given + When
            string result = _vendingMachine.Choose("A01");
            //Then
            Assert.That(result, Is.EqualTo("Not enough money!"));
        }

        [Test]
        public void VendingMachine_ChooseMuliple_EnougMoneyForOne()
        {
            //Given
            _vendingMachine.Insert(1m);
            //When + Then
            string result1 = _vendingMachine.Choose("A01");
            Assert.That(result1, Is.EqualTo("Not enough money!"));
            Assert.That(_vendingMachine.GetChange(), Is.EqualTo(1m));
            string result2 = _vendingMachine.Choose("A02");
            Assert.That(result2, Is.EqualTo("Vending Carampar"));
            Assert.That(_vendingMachine.GetChange(), Is.EqualTo(0.4m));
        }

        [Test]
        public void VendingMachine_ChooseA05_InvalidSelection()
        {
            //Given + When
            string result = _vendingMachine.Choose("A05");
            //Then
            Assert.That(result, Is.EqualTo("Invalid selection!"));
        }

        [Test]
        public void VendingMachine_ChooseA04_OutOfStock()
        {
            //Given
            _vendingMachine.Insert(6m);
            _vendingMachine.Choose("A04");
            //When
            string result = _vendingMachine.Choose("A04");
            //Then
            Assert.That(result, Is.EqualTo("Item KokoKola: Out of stock!"));
            Assert.That(_vendingMachine.GetChange(), Is.EqualTo(3.05m));
        }

        [Test]
        public void VendingMachine_ChooseMuliple_GetMultiple()
        {
            //Given
            _vendingMachine.Insert(6m);
            _vendingMachine.Choose("A04");
            _vendingMachine.Insert(6m);
            //When + Then
            string result1 = _vendingMachine.Choose("A04");
            Assert.That(result1, Is.EqualTo("Item KokoKola: Out of stock!"));
            string result2 = _vendingMachine.Choose("A01");
            Assert.That(result2, Is.EqualTo("Vending Smarlies"));
            string result3 = _vendingMachine.Choose("A02");
            Assert.That(result3, Is.EqualTo("Vending Carampar"));
            string result4 = _vendingMachine.Choose("A02");
            Assert.That(result4, Is.EqualTo("Vending Carampar"));
            Assert.That(_vendingMachine.GetChange(), Is.EqualTo(6.25m));
            Assert.That(_vendingMachine.GetBalance(), Is.EqualTo(5.75m));
        }

        [Test]
        public void VendingMachine_GetTopRevenueByHour_NominalCase()
        {
            //Given
            _vendingMachine.Insert(1000m);
            DateProvider.ActualDate = new DateTime(2020, 1, 1, 20, 30, 0);
            _vendingMachine.Choose("A01");
            DateProvider.ActualDate = new DateTime(2020, 3, 1, 23, 30, 0);
            _vendingMachine.Choose("A01");
            DateProvider.ActualDate = new DateTime(2020, 3, 4, 09, 22, 0);
            _vendingMachine.Choose("A01");
            DateProvider.ActualDate = new DateTime(2020, 4, 1, 23, 0, 0);
            _vendingMachine.Choose("A01");
            DateProvider.ActualDate = new DateTime(2020, 4, 1, 23, 59, 59);
            _vendingMachine.Choose("A01");
            DateProvider.ActualDate = new DateTime(2020, 4, 4, 09, 12, 0);
            _vendingMachine.Choose("A01");
            //When
            string[] results = _vendingMachine.GetTopRevenueByHour();
            //Then
            Assert.That(results[0], Is.EqualTo("Hour 23 generated a revenue of 4.80"));
            Assert.That(results[1], Is.EqualTo("Hour  9 generated a revenue of 3.20"));
            Assert.That(results[2], Is.EqualTo("Hour 20 generated a revenue of 1.60"));
        }
    }
}