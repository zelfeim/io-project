using Application.Domain.Aggregates.AnimalOwnerAggregate;

namespace Application.Domain.Aggregates.EmployeeAggregate;

public class Employee : IEntity, IAggregateRoot
{
    public Employee()
    {
    }

    public Employee(string name, string surname, Role role, string address, string email, string password)
    {
        Name = name;
        Surname = surname;
        Role = role;
        Address = address;
        EmailAddress = new EmailAddress(email);
        Password = password;
    }

    public string Name { get; }
    public string Surname { get; }
    public Role Role { get; }
    public string Address { get; }
    public List<WorkSchedule> WorkSchedule { get; private set; }

    public EmailAddress EmailAddress { get; private set; }

    public string Password { get; private set; }
    public int Id { get; }
}