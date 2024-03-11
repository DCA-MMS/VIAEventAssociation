namespace VIAEventAssociation.Core.Tools.OperationResult.Errors;

public enum ErrorCode
{
    // * USER ERRORS - EXAMPLES
    UserNotFound = 1000,
    UserAlreadyExists = 1001,
    UserIsNotActive = 1002,
    UserIsNotCreator = 1003,
    UserIsNotGuest = 1004,
    
    // * USER FULLNAME ERRORS
    FirstNameIsEmpty = 1100,
    FirstNameIsTooShort = 1101,
    FirstNameIsTooLong = 1102,
    FirstNameIsInvalid = 1103,
    LastNameIsEmpty = 1110,
    LastNameIsTooShort = 1111,
    LastNameIsTooLong = 1112,
    LastNameIsInvalid = 1113,
    
    // * USER EMAIL ERRORS
    EmailIsEmpty = 1200,
    EmailMustStartWith = 1201,
    EmailMustEndWith = 1202,
    EmailWithInvalidCharacters = 1203,
    
    // * EVENT ERRORS - EXAMPLES
    EventNotFound = 2000,
    EventAlreadyExists = 2001,
    EventIsNotActive = 2002,
    EventIsNotPublic = 2003,
    EventIsNotPrivate = 2004,
    EventIsNotPublished = 2005,
    EventIsNotDraft = 2006,
    EventIsNotCancelled = 2007,
    EventNotModifiable = 2008, // - Might need to be moved... - MHN
    
    // * EVENT TITLE ERRORS
    EventTitleIsEmpty = 2101,
    EventTitleIsTooShort = 2102,
    EventTitleIsTooLong = 2103,
    EventTitleNotModifiable = 2104,
    
    // * EVENT DESCRIPTION ERRORS
    EventDescriptionIsToolLong = 2201,
    EventDescriptionNotModifiable = 2202,
    
    // * EVENT CAPACITY ERRORS
    EventCapacityIsNegative = 2301,
    EventCapacityIsLessThanOne = 2302,
    EventCapacityIsGreaterThanAThousand = 2303,
    EventCapacityNotModifiable = 2304,
    
    // * EVENT TIME RANGE ERRORS
    EventTimeRangeStartAfterEndDate = 2401,
    EventTimeRangeStartAfterEndTime = 2402,
    EventTimeRangeDurationLessThanOneHour = 2403,
    EventTimeRangeDurationIsLongerThanTenHours = 2404,
    EventTimeRangeStartIsBeforeEight = 2405,
    EventTimeRangeStartIsInPast = 2406,
    EventTimeRangeNotModifiable = 2407,
    
    // * EVENT VISIBILITY ERRORS
    EventVisibilityNotModifiable = 2501,
    
    // * EVENT REQUEST ERRORS
    RequestReasonIsTooLong = 2600,
    RequestToFullEvent = 2601,
    RequestToEventGuestIsAlreadyPartaking = 2602,
    RequestToEventThatIsNotPublic = 2603,
    RequestToEventThatIsNotActive = 2604,
    
    // # EVENT INVITATION ERRORS
    InvitationToFullEvent = 2700,
    InvitationToNonReadyOrActiveEvent = 2701,
    InvitationAcceptToGuestNotInvited = 2702,
    InvitationAcceptToFullEvent = 2702,
    InvitationAcceptToCancelledEvent = 2703,
    InvitationAcceptToReadyEvent = 2704,

    // * BOOKING ERRORS - EXAMPLES
    BookingNotFound = 3000,
    BookingAlreadyExists = 3001,
    BookingIsNotActive = 3002,
    BookingIsNotConfirmed = 3003,
    BookingIsNotCancelled = 3004,
    
    // * LOCATION ERRORS
    LocationExample = 4000,
    
    // * TIME RANGE ERRORS
    TimeRangeEndBeforeOrEqualToStart = 5000,
    
}