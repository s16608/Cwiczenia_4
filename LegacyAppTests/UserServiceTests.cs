using LegacyApp;

namespace LegacyAppTests;
public class UserServiceTest
{
    [Test]
    public void AddUser_Should_Return_False_when_Missing_FirstName()
    {
        //Arrange
        var service = new UserService();
        //Act
        var result = service.AddUser(null, null, "kowalski@wp.pl", new DateTime (1990, 10, 10), 2137);
        //Assert
        Assert.False(result);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}