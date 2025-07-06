using AutoMapper;
using PO.Domain.Entities.MasterData;
using PO.Shared.DTOs.MasterData;

namespace PO.Application.Mappings;

public class MasterDataMappingProfile : Profile
{
    public MasterDataMappingProfile()
    {
        // Role mappings
        CreateMap<Role, RoleDto>();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<UpdateRoleDto, Role>();
        CreateMap<Role, RoleLookupDto>();

        // User mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DeptName : null));
        
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, UserLookupDto>();

        // UOM mappings
        CreateMap<UOM, UOMDto>();
        CreateMap<CreateUOMDto, UOM>();
        CreateMap<UpdateUOMDto, UOM>();
        CreateMap<UOM, UOMLookupDto>();

        // Department mappings
        CreateMap<Department, DepartmentDto>();
    }
}