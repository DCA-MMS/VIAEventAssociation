namespace VIAEventAssociation.Core.Tools.OperationResult.Errors;

public enum ErrorCode
{
    // * USER ERRORS - EXAMPLES
    UserNotFound = 1000,
    UserAlreadyExists = 1001,
    UserIsNotActive = 1002,
    UserIsNotCreator = 1003,
    UserIsNotGuest = 1004,
    
    // * EVENT ERRORS - EXAMPLES
    EventNotFound = 2000,
    EventAlreadyExists = 2001,
    EventIsNotActive = 2002,
    EventIsNotPublic = 2003,
    EventIsNotPrivate = 2004,
    EventIsNotPublished = 2005,
    EventIsNotDraft = 2006,
    EventIsNotCancelled = 2007,
    
    // * BOOKING ERRORS - EXAMPLES
    BookingNotFound = 3000,
    BookingAlreadyExists = 3001,
    BookingIsNotActive = 3002,
    BookingIsNotConfirmed = 3003,
    BookingIsNotCancelled = 3004
}