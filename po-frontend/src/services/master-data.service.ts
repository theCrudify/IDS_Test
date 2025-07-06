import { apiService } from './api.service';
import { API_ENDPOINTS } from '../constants/api.constants';
import {
  ApiResult,
  PagedResult,
  PagedRequest,
  DepartmentDto,
  CreateDepartmentDto,
  UpdateDepartmentDto,
  DepartmentLookupDto,
  VendorDto,
  CreateVendorDto,
  UpdateVendorDto,
  VendorLookupDto,
  ItemDto,
  CreateItemDto,
  UpdateItemDto,
  ItemLookupDto,
  UserDto,
  CreateUserDto,
  UpdateUserDto,
  UserLookupDto,
  UOMDto,
  CreateUOMDto,
  UpdateUOMDto,
  UOMLookupDto,
  TaxDto,
  CreateTaxDto,
  UpdateTaxDto,
  TaxLookupDto
} from '../types/api.types';

// Base class for master data services
abstract class BaseMasterDataService<TDto, TCreateDto, TUpdateDto, TLookupDto> {
  constructor(
    protected baseEndpoint: string,
    protected pagedEndpoint: string,
    protected lookupEndpoint: string,
    protected byIdEndpoint: (id: number) => string
  ) {}

  async getAll(): Promise<ApiResult<TDto[]>> {
    return apiService.get<TDto[]>(this.baseEndpoint);
  }

  async getPaged(request: PagedRequest): Promise<ApiResult<PagedResult<TDto>>> {
    const params = new URLSearchParams({
      pageNumber: request.pageNumber.toString(),
      pageSize: request.pageSize.toString(),
    });
    
    if (request.searchTerm) {
      params.append('searchTerm', request.searchTerm);
    }
    
    if (request.sortField) {
      params.append('sortField', request.sortField);
    }
    
    if (request.sortDirection) {
      params.append('sortDirection', request.sortDirection);
    }

    return apiService.get<PagedResult<TDto>>(`${this.pagedEndpoint}?${params}`);
  }

  async getById(id: number): Promise<ApiResult<TDto>> {
    return apiService.get<TDto>(this.byIdEndpoint(id));
  }

  async create(dto: TCreateDto): Promise<ApiResult<TDto>> {
    return apiService.post<TDto>(this.baseEndpoint, dto);
  }

  async update(id: number, dto: TUpdateDto): Promise<ApiResult<TDto>> {
    return apiService.put<TDto>(this.byIdEndpoint(id), dto);
  }

  async delete(id: number): Promise<ApiResult<boolean>> {
    return apiService.delete<boolean>(this.byIdEndpoint(id));
  }

  async getLookup(): Promise<ApiResult<TLookupDto[]>> {
    return apiService.get<TLookupDto[]>(this.lookupEndpoint);
  }
}

// Department Service
export class DepartmentService extends BaseMasterDataService<
  DepartmentDto,
  CreateDepartmentDto,
  UpdateDepartmentDto,
  DepartmentLookupDto
> {
  constructor() {
    super(
      API_ENDPOINTS.DEPARTMENTS,
      API_ENDPOINTS.DEPARTMENTS_PAGED,
      API_ENDPOINTS.DEPARTMENTS_LOOKUP,
      API_ENDPOINTS.DEPARTMENT_BY_ID
    );
  }
}

// Vendor Service
export class VendorService extends BaseMasterDataService<
  VendorDto,
  CreateVendorDto,
  UpdateVendorDto,
  VendorLookupDto
> {
  constructor() {
    super(
      API_ENDPOINTS.VENDORS,
      API_ENDPOINTS.VENDORS_PAGED,
      API_ENDPOINTS.VENDORS_LOOKUP,
      API_ENDPOINTS.VENDOR_BY_ID
    );
  }
}

// Item Service
export class ItemService extends BaseMasterDataService<
  ItemDto,
  CreateItemDto,
  UpdateItemDto,
  ItemLookupDto
> {
  constructor() {
    super(
      API_ENDPOINTS.ITEMS,
      API_ENDPOINTS.ITEMS_PAGED,
      API_ENDPOINTS.ITEMS_LOOKUP,
      API_ENDPOINTS.ITEM_BY_ID
    );
  }
}

// User Service
export class UserService extends BaseMasterDataService<
  UserDto,
  CreateUserDto,
  UpdateUserDto,
  UserLookupDto
> {
  constructor() {
    super(
      API_ENDPOINTS.USERS,
      API_ENDPOINTS.USERS_PAGED,
      API_ENDPOINTS.USERS_LOOKUP,
      API_ENDPOINTS.USER_BY_ID
    );
  }
}

// UOM Service
export class UOMService extends BaseMasterDataService<
  UOMDto,
  CreateUOMDto,
  UpdateUOMDto,
  UOMLookupDto
> {
  constructor() {
    super(
      API_ENDPOINTS.UOMS,
      API_ENDPOINTS.UOMS_PAGED,
      API_ENDPOINTS.UOMS_LOOKUP,
      API_ENDPOINTS.UOM_BY_ID
    );
  }
}

// Tax Service
export class TaxService extends BaseMasterDataService<
  TaxDto,
  CreateTaxDto,
  UpdateTaxDto,
  TaxLookupDto
> {
  constructor() {
    super(
      API_ENDPOINTS.TAXES,
      API_ENDPOINTS.TAXES_PAGED,
      API_ENDPOINTS.TAXES_LOOKUP,
      API_ENDPOINTS.TAX_BY_ID
    );
  }
}

// Export service instances
export const departmentService = new DepartmentService();
export const vendorService = new VendorService();
export const itemService = new ItemService();
export const userService = new UserService();
export const uomService = new UOMService();
export const taxService = new TaxService();