using AutoMapper;
using PO.Domain.Entities.MasterData;
using PO.Domain.Entities.PurchaseOrder;
using PO.Domain.Entities.System;
using PO.Shared.DTOs.MasterData;
using PO.Shared.DTOs.PurchaseOrder;
using PO.Shared.DTOs.System;

namespace PO.Application.Mappings;

public class MasterDataMappingProfile : Profile
{
    public MasterDataMappingProfile()
    {
        // Role Mappings
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<UpdateRoleDto, Role>();
        CreateMap<Role, RoleLookupDto>();

        // Department Mappings
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<CreateDepartmentDto, Department>();
        CreateMap<UpdateDepartmentDto, Department>();
        CreateMap<Department, DepartmentLookupDto>();

        // User Mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            .ForMember(dest => dest.DeptName, opt => opt.MapFrom(src => src.Department.DeptName))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager != null ? src.Manager.FullName : null));
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, UserLookupDto>();

        // Vendor Mappings
        CreateMap<Vendor, VendorDto>().ReverseMap();
        CreateMap<CreateVendorDto, Vendor>();
        CreateMap<UpdateVendorDto, Vendor>();
        CreateMap<Vendor, VendorLookupDto>();

        // Item Mappings
        CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.DefaultUOMCode, opt => opt.MapFrom(src => src.DefaultUOM != null ? src.DefaultUOM.UOMCode : null))
            .ForMember(dest => dest.DefaultTaxCode, opt => opt.MapFrom(src => src.DefaultTax != null ? src.DefaultTax.TaxCode : null));
        CreateMap<CreateItemDto, Item>();
        CreateMap<UpdateItemDto, Item>();
        CreateMap<Item, ItemLookupDto>();

        // UOM Mappings
        CreateMap<UOM, UOMDto>().ReverseMap();
        CreateMap<CreateUOMDto, UOM>();
        CreateMap<UpdateUOMDto, UOM>();
        CreateMap<UOM, UOMLookupDto>();

        // Tax Mappings
        CreateMap<Tax, TaxDto>().ReverseMap();
        CreateMap<CreateTaxDto, Tax>();
        CreateMap<UpdateTaxDto, Tax>();
        CreateMap<Tax, TaxLookupDto>();

        // Division Mappings
        CreateMap<Division, DivisionDto>().ReverseMap();
        CreateMap<CreateDivisionDto, Division>();
        CreateMap<UpdateDivisionDto, Division>();
        CreateMap<Division, DivisionLookupDto>();

        // Purchase Order Mappings
        CreateMap<CreatePOHeaderDto, POHeader>();
        CreateMap<UpdatePOHeaderDto, POHeader>();
        CreateMap<POHeader, POHeaderDto>();
        CreateMap<CreatePODetailDto, PODetail>();
        CreateMap<UpdatePODetailDto, PODetail>();
        CreateMap<PODetail, PODetailDto>();

        // Notification Mappings
        CreateMap<Notification, NotificationDto>();
    }
}