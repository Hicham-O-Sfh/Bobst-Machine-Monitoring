namespace Service.DTOs
{
    public class MachineDTO : DtoBase
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}