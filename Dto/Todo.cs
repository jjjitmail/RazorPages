using Dto.Enum;

namespace Dto
{
    public class Todo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Priority { get; set; }
        public StatusType Status { get; set; }
        public bool CanBeDeleted => Status == StatusType.completed;

        public string StatusValue => Dto.Enum.StringEnum.GetStringValue(Status);
    }
}
