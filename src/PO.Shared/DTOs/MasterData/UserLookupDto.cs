// FILE LOCATION: src/PO.Shared/DTOs/MasterData/UserLookupDto.cs

namespace PO.Shared.DTOs.MasterData;

public class UserLookupDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
}
