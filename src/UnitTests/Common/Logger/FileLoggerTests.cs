using Logger;

namespace Tests.Common.Logger;

[TestFixture]
public class FileLoggerTests
{
    [Test]
    public void FileLogger_Logs_To_File_In_Correct_Format()
    {
        // Arrange
        string filePath = "test.log";
        string operation = "TestOperation";
        string details = "TestDetails";
        DateTime now = DateTime.Now;
        string expectedMessage = $"[{now:hh:mm:ss dd/MM/yyyy}] {operation.ToUpper()}: {details}\r\n";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        FileLogger logger = new FileLogger(filePath);
        
        // Act
        logger.LogAsync(now, operation, details).Wait();
        
        // Assert
        string actualMessage = File.ReadAllText(filePath);
        Assert.That(actualMessage, Is.EqualTo(expectedMessage));
    }
}