namespace LearnMyCalculatorApp.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CalculatorNullTest()
    {
        var calculator = new Calculator();

        Assert.IsNotNull(calculator);

        // Assert.IsTrue(false); // Will fail the test
    }

    [Test]
    public void CalculatorAddTest()
    {
        var calculator = new Calculator();

        var actual = calculator.Add(1, 1);

        Assert.That(actual, Is.EqualTo(2));
    }

    [Test]
    public void CalculatorSubtractTest()
    {
        var calculator = new Calculator();

        var actual = calculator.Subtract(1, 1);

        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void CalculatorMultiplyTest()
    {
        var calculator = new Calculator();

        var actual = calculator.Multiply(1, 1);

        Assert.That(actual, Is.EqualTo(1));
    }

    [Test]
    public void CalculatorDivideTest()
    {
        var calculator = new Calculator();

        var actual = calculator.Divide(1, 1);

        Assert.That(actual, Is.EqualTo(1));
    }

    [Test]
    public void CalculatorDivideByZeroTest()
    {
        var calculator = new Calculator();

        var actual = calculator.Divide(10, 0);

        Assert.IsNull(actual);
    }

    [Test]
    public void CalculatorAddFluentAssertionTest()
    {
        var calculator = new Calculator();
        var actual = calculator.Add(1, 1);

        // Non-fluent asserts:
        // Assert.AreEqual(actual, 2);
        // Assert.AreNotEqual(actual, 1);

        // Same asserts as what is commented out above, but using Fluent Assertions
        actual.Should().Be(2).And.NotBe(1);
    }

    [TestCase(1, 1, 2)]
    [TestCase(2, 2, 4)]
    [TestCase(3, 3, 6)]
    [TestCase(0, 0, 1)] // The test run with this row fails
    public void CalculatorAddDataTest(int x, int y, int expected)
    {
        var calculator = new Calculator();
        var actual = calculator.Add(x, y);

        actual.Should().Be(expected);
    }

    [TestCase(1)]
    [TestCase(2)]
    public void MoqTest(int id)
    {
        // Создаем мок ICustomerRepository с помощью Moq
        var mockRepository = new Mock<ICustomerRepository>();

        // Настраиваем мок, чтобы он возвращал фиктивный объект Customer при вызове метода GetById
        mockRepository.Setup(r => r.GetById(1)).Returns(new Customer { Id = 1, Name = "Коля" });
        mockRepository.Setup(r => r.GetById(2)).Returns(new Customer { Id = 2, Name = "Вася" });
        
        // Создаем объект класса, который хотим протестировать, и передаем ему мок в качестве зависимости
        var customerService = new CustomerService(mockRepository.Object);
        
        // Вызываем тестируемый метод
        var actual = customerService.GetCustomerName(id);
        
        // Проверяем, что результат соответствует ожиданиям
        actual.Should().Be("Коля");
        
        // Проверяем, что мок был вызван с правильным параметром
        mockRepository.Verify(r => r.GetById(id), Times.Once);
    }
}
