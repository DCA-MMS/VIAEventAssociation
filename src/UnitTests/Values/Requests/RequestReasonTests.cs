using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Request.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Values.Requests;

public class RequestReasonTests
{
    [Test, Category("RequestReason")]
    [TestCase("test")]
    [TestCase("Request Reason with a very good reason.")]
    [TestCase("58943 #-@!! Nice., mess4geæØå")]
    [TestCase("This message is 250 characters longfoijsd ffoijdsodfsijofijsdoijfdsofdijsojisdf odsfji fjdsoifjdsojfiod sjfoidsjfoijds oifjdsoi fodijfiodsjf oidsjfoidsj ofijdsiofjodisjfiodsjf oidsjfoisd jofidsjofi jdsoifjoai jsodjaoi jdoiawjdoijawiodjawoidjwaiojddio")]
    [TestCase("This message is 249 characters longfoijsd ffoijdsodfsijofijsdoijfdsofdijsojisdf odsfji fjdsoifjdsojfiod sjfoidsjfoijds oifjdsoi fodijfiodsjf oidsjfoidsj ofijdsiofjodisjfiodsjf oidsjfoisd jofidsjofi jdsoifjoai jsodjaoi jdoiawjdoijawiodjawoidjwagiojdi")]
    [TestCase("")]
    public void Success_Create_RequestReason(string reason)
    {
        // Arrange
        var requestReason = RequestReason.Create(reason);
            
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(requestReason.IsFailure, Is.False);
            Assert.That(requestReason.Value.Value, Is.EqualTo(reason));
        });
    }
    
    
    [Test, Category("RequestReason")]
    [TestCase("This message is 251 characters longfoijsd ffoijdsodfsijofijsdoijfdsofdijsojisdf odsfji fjdsoifjdsojfiod sjfoidsjfoijds oifjdsoi fodijfiodsjf oidsjfoidsj ofijdsiofjodisjfiodsjf oidsjfoisd jofidsjofi jdsoifjoai jsodjaoi jdoiawjdoijawiodjawoidjwaiojddios")]
    [TestCase("This message is 252 characters longfoijsd ffoijdsodfsijofijsdoijfdsofdijsojisdf odsfji fjdsoifjdsojfiod sjfoidsjfoijds oifjdsoi fodijfiodsjf oidsjfoidsj ofijdsiofjodisjfiodsjf oidsjfoisd jofidsjofi jdsoifjoai jsodjaoi jdoiawjdoijawiodjawoidjwaiojddiosd")]
    public void Failure_Create_RequestReason_Is_Too_Long(string reason)
    {
        // Arrange
        var requestReason = RequestReason.Create(reason);
            
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(requestReason.IsFailure, Is.True);
            Assert.That(requestReason.Errors.Any(x => x.Code == ErrorCode.RequestReasonIsTooLong), Is.True);
        });
    }
} 