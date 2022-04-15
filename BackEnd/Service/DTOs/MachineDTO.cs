namespace Service.DTOs
{
    public class MachineDTO : DtoBase
    {
        public int MachineId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public int Production { get; set; }
    }
}