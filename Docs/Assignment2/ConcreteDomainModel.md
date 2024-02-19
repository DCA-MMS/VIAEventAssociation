```mermaid
classDiagram
    %%Event
    EventTitle "1" <-- "1" Event : Title
    EventDescription "1" <-- "1" Event : Description
    Event "1" --> "1" EventVisibility : Visibility
    Event "1" --> "1" EventStatus : Status
    Event "1" *--> "1" TimeRange : Duration
    Event "1" *--> "1" Capacity : MaxGuest
    Event "1" ..> "1" Location : Location
    Request "1" <--* "0..*" Event : Requests
    Invitation "1" <--* "0..*" Event : Invitations 
    User "1" <.. "1" Event : Creator
    User "0..*" <.. "1" Event : Participants

    %%Invitation
    Invitation "1" --> "1" InvitationStatus : Status
    User "1" <.. "1" Invitation : Guest

    %%Location
    Location "1" --> "1" LocationName : Name
    Location "1" --> "1" LocationType : Type
    Location "1" --> "1" Capacity : Capacity
    Location "1" *--> "0..*" Booking : Bookings

    %%Booking
    Booking "1" --> "1" TimeRange : Duration

    %%User
    FullName "1" <-- "1" User : FullName
    Email "1" <-- "1" User : Email

    %%Request
    Request "1" --> "1" RequestStatus : Status
    Request "1" ..> "1" User : Guest

    class TimeRange {
        <<Value>>
        +[get] Start : DateTime
        +[get] End : DateTime
        +TimeRange(start : DateTime, end : DateTime)
    }

    class Capacity {
        <<Value>>
        +[get] Value : int
        +Capacity(capacity : int)
    }

    namespace EVENT {
        class Event {
            <<Aggregate>>
            +[get] IsLocked : bool
            %% +CreateEvent() ???
            +UpdateTitle(title : EventTitle)
            +UpdateDescription(description : EventDescription)
            +UpdateDuration(duration : TimeRange)
            +SetLocation(location: Location)
            +SetPublic()
            +SetPrivate()
            +SetMaxGuests(amount : Capacity)
            +Ready()
            +Activate()
            +Cancel()
            +Delete()
            +AddGuest(guest : User)
            +RemoveGuest(guest : User)
            +InviteGuest(guest : User)
            +RequestToJoin(reason : RequestReason)
        }

        class EventTitle {
            <<Value>>
            +[get] Value : string
            +EventTitle(title : string)
        }
        
        class EventDescription {
            <<Value>>
            +[get] Value : string
            +EventDescription(description : string)
        }
        
        class EventVisibility {
            <<Enum>>
            +Public
            +Private
        }

        class EventStatus {
            <<Enum>>
            +Active
            +Draft
            +Ready
            +Cancelled
        }
    }
    
    namespace LOCATION {
        class Location {
            <<Aggregate>>
            %% +AddLocation(name : string, type : LocationType, capacity : int) ???
            +UpdateName(name : LocationName)
            +SetCapacity(capacity : Capacity)
            +SetAvailability(interval : TimeRange)
            %% +Book(timeRange : TimeRange, event : Event)
        }

        class LocationType {
            <<Enum>>
            +OpenSpaces
            +Outside
            +Inside
        }

        class LocationName {
            <<Value>>
            +[get] Value : string
            +LocationName(name : string)
        }

        class Booking {
            <<Entity>>
        }
    }

    namespace USER {
        class User {
            <<Entity>>
            %% +Register(fullName : FullName, email : Email)
        }

        class Email {
            <<Value>>
            +[get] Value : string
            +Email(email : string)
        }
        
        class FullName {
            <<Value>>
            +[get] firstName : string
            +[get] lastName : string
            +FullName(firstName : string, lastName : string)
        }

    }
    
    namespace REQUEST {
        class Request {
            <<Entity>>
            +Approve()
            +Decline()
        }

        class RequestStatus {
            <<Enum>>
            +Pending
            +Accepted
            +Rejected
        }

        class RequestReason {
            <<Value>>
            +[get] Value : string
            +RequestReason(reason : string)
        }
    }
    
    namespace INVITATION {
        class Invitation {
            <<Entity>>
            +Accept()
            +Decline()
        }

        class InvitationStatus {
            <<Enum>>
            +Pending
            +Accepted
            +Rejected
        }   
    }
```