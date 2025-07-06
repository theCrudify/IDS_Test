import { apiService } from './api.service';
import { API_ENDPOINTS } from '../constants/api.constants';
import {
  ApiResult,
  POHeaderDto,
  ApprovalRequestDto
} from '../types/api.types';

export class ApprovalService {
  // Approve purchase order
  async approvePurchaseOrder(
    poId: number, 
    userId: number, 
    notes?: string
  ): Promise<ApiResult<boolean>> {
    const requestDto: ApprovalRequestDto = { notes };
    const url = `${API_ENDPOINTS.APPROVE_PO(poId)}?userId=${userId}`;
    return apiService.post<boolean>(url, requestDto);
  }

  // Reject purchase order
  async rejectPurchaseOrder(
    poId: number, 
    userId: number, 
    reason: string
  ): Promise<ApiResult<boolean>> {
    const requestDto: ApprovalRequestDto = { reason };
    const url = `${API_ENDPOINTS.REJECT_PO(poId)}?userId=${userId}`;
    return apiService.post<boolean>(url, requestDto);
  }

  // Get pending approvals for user
  async getPendingApprovals(userId: number): Promise<ApiResult<POHeaderDto[]>> {
    return apiService.get<POHeaderDto[]>(API_ENDPOINTS.PENDING_APPROVALS(userId));
  }

  // Get approval history for PO (if endpoint exists)
  async getApprovalHistory(poId: number): Promise<ApiResult<any[]>> {
    // This would need to be implemented in the backend if needed
    return apiService.get<any[]>(`${API_ENDPOINTS.PURCHASE_ORDER_BY_ID(poId)}/approval-history`);
  }
}

export const approvalService = new ApprovalService();