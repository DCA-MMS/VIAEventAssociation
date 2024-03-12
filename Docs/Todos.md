# Todos

1. The return type Result\<bool> should probably just be Result, since we don't need a payload for a void method
2. Methods should be renamed to match the concrete domain model (ChangeTitle --> UpdateTitle, etc.)
3. Some methods need to be removed (ChangeVisibility()) --> MakePublic() and MakePrivate())
4. Update Concrete Domain Model when finished


# Work Items

## Mads
- Refactor `TimeRange`
  - Add new Error message for Date and Time (UC4)
- Remove `EventTimeRange` (Move logic to `Event` and `TimeRange`)
- Delete `EventTimeRange` Unit Tests
- Refactor/Revamp `Event` + `EventFactory`
  - Move Default values for `Event` to a constant class or something.
  - Fix `EventFactory` Unit Tests

## Simon
- Finish usecase 9 + 10
- Update Concrete Domain Model
