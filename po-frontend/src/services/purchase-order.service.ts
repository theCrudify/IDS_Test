import { apiService } from './api.service';
import { API_ENDPOINTS } from '../constants/api.constants';
import {
  ApiResult,
  PagedResult,
  PagedRequest,
  POHeaderDto,
  CreatePOHeaderDto,
  UpdatePOHeaderDto
} from '../types/api.types';

export class PurchaseOrderService {
  // Get all purchase orders (paginated)
  async getPurchaseOrdersPaged(request: PagedRequest): Promise<ApiResult<PagedResult<POHeaderDto>>> {
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

    return apiService.get<PagedResult<POHeaderDto>>(`${API_ENDPOINTS.PURCHASE_ORDERS}/paged?${params}`);
  }

  // Get purchase order by ID
  async getPurchaseOrderById(id: number): Promise<ApiResult<POHeaderDto>> {
    return apiService.get<POHeaderDto>(API_ENDPOINTS.PURCHASE_ORDER_BY_ID(id));
  }

  // Create new purchase order
  async createPurchaseOrder(dto: CreatePOHeaderDto): Promise<ApiResult<POHeaderDto>> {
    return apiService.post<POHeaderDto>(API_ENDPOINTS.PURCHASE_ORDERS, dto);
  }

  // Update purchase order
  async updatePurchaseOrder(id: number, dto: UpdatePOHeaderDto): Promise<ApiResult<POHeaderDto>> {
    return apiService.put<POHeaderDto>(API_ENDPOINTS.PURCHASE_ORDER_BY_ID(id), dto);
  }

  // Submit purchase order for approval
  async submitPurchaseOrder(id: number): Promise<ApiResult<boolean>> {
    return apiService.post<boolean>(API_ENDPOINTS.PURCHASE_ORDER_SUBMIT(id));
  }

  // Delete purchase order
  async deletePurchaseOrder(id: number): Promise<ApiResult<boolean>> {
    return apiService.delete<boolean>(API_ENDPOINTS.PURCHASE_ORDER_BY_ID(id));
  }

  // Get purchase orders by status
  async getPurchaseOrdersByStatus(status: number, request: PagedRequest): Promise<ApiResult<PagedResult<POHeaderDto>>> {
    const params = new URLSearchParams({
      pageNumber: request.pageNumber.toString(),
      pageSize: request.pageSize.toString(),
      status: status.toString(),
    });
    
    if (request.searchTerm) {
      params.append('searchTerm', request.searchTerm);
    }

    return apiService.get<PagedResult<POHeaderDto>>(`${API_ENDPOINTS.PURCHASE_ORDERS}/paged?${params}`);
  }

  // Get user's purchase orders
  async getUserPurchaseOrders(userId: number, request: PagedRequest): Promise<ApiResult<PagedResult<POHeaderDto>>> {
    const params = new URLSearchParams({
      pageNumber: request.pageNumber.toString(),
      pageSize: request.pageSize.toString(),
      createdById: userId.toString(),
    });
    
    if (request.searchTerm) {
      params.append('searchTerm', request.searchTerm);
    }

    return apiService.get<PagedResult<POHeaderDto>>(`${API_ENDPOINTS.PURCHASE_ORDERS}/paged?${params}`);
  }
}

export const purchaseOrderService = new PurchaseOrderService();