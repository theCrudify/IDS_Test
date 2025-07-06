// FILE LOCATION: src/PO.Shared/DTOs/MasterData/DepartmentDto.cs

namespace PO.Shared.DTOs.MasterData;

public class DepartmentDto
{
    public int Id { get; set; }
    public string DeptCode { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public int? DeptHeadId { get; set; }
    public bool IsActive { get; set; }
}
