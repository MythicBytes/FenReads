namespace FenReads.Domain.Enums;

public enum ScrapingStatus
{
    Pending = 0,
    InQueue = 1,
    Running = 2,
    Completed = 3,
    Failed = 4,
    Cancelled = 5,
    Retrying = 6
}