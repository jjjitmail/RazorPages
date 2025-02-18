using Dto.Attributes;

namespace Dto.Enum
{
    public enum StatusType
    {
        [StringValue("Not started")]
        not_started = 0,
        [StringValue("In progress")]
        in_progress = 1,
        [StringValue("Completed")]
        completed = 2
    }
}
