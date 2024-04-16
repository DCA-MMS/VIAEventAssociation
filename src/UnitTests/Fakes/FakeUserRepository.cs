using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace Tests.Fakes;

public class FakeUserRepository : IUserRepository
{
    public List<User> Users { get; } =
    [
        User.Create(FullName.Create("Jhon", "Jhonson"), Email.Create("123456@via.dk")),
        User.Create(FullName.Create("hey", "yoo"), Email.Create("HEYO@via.dk")),
        User.Create(FullName.Create("Joe", "Joe"), Email.Create("JOJO@via.dk")),
        User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("BOBS@via.dk")),
    ];

    public Task AddAsync(User aggregate)
    {
        Users.Add(aggregate);
        return Task.CompletedTask;
    }

    public Task<User?> GetByIdAsync(Id<User> id)
    {
        return Task.FromResult(Users.FirstOrDefault(u => u.Id.Value == id.Value));
    }
}