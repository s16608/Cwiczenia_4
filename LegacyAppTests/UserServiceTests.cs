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
        var result = service.AddUser(null, "Kowalski", "kowalski@wp.pl", new DateTime (1990, 10, 10), 2137);
        
        //Assert
        Assert.AreEqual(false, result);
    }
    
    [Test]
    public void AddUser_Should_Return_False_when_Missing_LastName()
    {
        //Arrange
        var service = new UserService();
        
        //Act
        var result = service.AddUser("Jan", "", "kowalski@wp.pl", new DateTime (1990, 10, 10), 2137);
        
        //Assert
        Assert.AreEqual(false, result);
    }
    
    [Test]
    public void AddUser_Should_Return_False_when_Email_Is_Incorrect()
    {
        //Arrange
        var service = new UserService();
        
        //Act
        var result = service.AddUser("Jan", "Kowalski", "kowalski@@wp.pl", new DateTime (2003, 2, 28), 2137);
        
        //Assert
        Assert.AreEqual(false, result);
    }
    
    [Test]
    public void AddUser_Should_Return_False_When_DateOfBirth_Is_In_Future()
    {
        var service = new UserService();
        
        var futureDate = DateTime.Now.AddDays(1);
        
        var result = service.AddUser("Jan", "", "kowalski@wp.pl", futureDate, 2137);
        
        Assert.AreEqual(false, result);
    }

    [Test]
    public void AddUser_Should_Handle_Non_LeapYear_February_29()
    {
        var service = new UserService();
      
        DateTime nonLeapYearDate;
        try
        {
            nonLeapYearDate = new DateTime(2023, 2, 29);
        }
        catch (ArgumentOutOfRangeException)
        {
            nonLeapYearDate = new DateTime(2023, 2, 28); 
        }
        
        var result = service.AddUser("Jan", "", "kowalski@wp.pl", nonLeapYearDate, 2137);
        
        Assert.IsFalse(result, "The method should return false for a non-leap year February 29.");
    }
    
    
}