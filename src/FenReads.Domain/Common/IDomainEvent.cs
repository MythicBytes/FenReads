namespace FenReads.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}