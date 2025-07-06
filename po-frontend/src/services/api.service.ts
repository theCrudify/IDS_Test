import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';
import { API_BASE_URL } from '../constants/api.constants';
import { ApiResult } from '../types/api.types';

class ApiService {
  private axiosInstance: AxiosInstance;

  constructor() {
    this.axiosInstance = axios.create({
      baseURL: API_BASE_URL,
      timeout: 30000,
      headers: {
        'Content-Type': 'application/json',
      },
    });

    this.setupInterceptors();
  }

  private setupInterceptors() {
    // Request interceptor
    this.axiosInstance.interceptors.request.use(
      (config) => {
        // Add auth token if available
        const token = localStorage.getItem('auth_token');
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }
        
        console.log(`🚀 API Request: ${config.method?.toUpperCase()} ${config.url}`);
        return config;
      },
      (error) => {
        console.error('❌ Request Error:', error);
        return Promise.reject(error);
      }
    );

    // Response interceptor
    this.axiosInstance.interceptors.response.use(
      (response: AxiosResponse<ApiResult>) => {
        console.log(`✅ API Response: ${response.status} ${response.config.url}`);
        return response;
      },
      (error) => {
        console.error('❌ Response Error:', error);
        
        if (error.response?.status === 401) {
          // Handle unauthorized - redirect to login
          localStorage.removeItem('auth_token');
          window.location.href = '/login';
        }
        
        return Promise.reject(error);
      }
    );
  }

  // Generic GET request
  async get<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResult<T>> {
    try {
      const response = await this.axiosInstance.get<ApiResult<T>>(url, config);
      return response.data;
    } catch (error: any) {
      return this.handleError(error);
    }
  }

  // Generic POST request
  async post<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> {
    try {
      const response = await this.axiosInstance.post<ApiResult<T>>(url, data, config);
      return response.data;
    } catch (error: any) {
      return this.handleError(error);
    }
  }

  // Generic PUT request
  async put<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> {
    try {
      const response = await this.axiosInstance.put<ApiResult<T>>(url, data, config);
      return response.data;
    } catch (error: any) {
      return this.handleError(error);
    }
  }

  // Generic DELETE request
  async delete<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResult<T>> {
    try {
      const response = await this.axiosInstance.delete<ApiResult<T>>(url, config);
      return response.data;
    } catch (error: any) {
      return this.handleError(error);
    }
  }

  // Generic PATCH request
  async patch<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> {
    try {
      const response = await this.axiosInstance.patch<ApiResult<T>>(url, data, config);
      return response.data;
    } catch (error: any) {
      return this.handleError(error);
    }
  }

  private handleError(error: any): ApiResult {
    console.error('API Error:', error);
    
    if (error.response?.data) {
      // API returned an error response with ApiResult structure
      return error.response.data;
    }
    
    if (error.response) {
      // HTTP error status
      return {
        success: false,
        message: `HTTP Error ${error.response.status}: ${error.response.statusText}`,
        statusCode: error.response.status,
        timestamp: new Date().toISOString(),
      };
    }
    
    if (error.request) {
      // Network error
      return {
        success: false,
        message: 'Network error: Unable to connect to the server',
        statusCode: 0,
        timestamp: new Date().toISOString(),
      };
    }
    
    // Unknown error
    return {
      success: false,
      message: error.message || 'An unexpected error occurred',
      statusCode: 500,
      timestamp: new Date().toISOString(),
    };
  }

  // Health check
  async healthCheck(): Promise<boolean> {
    try {
      const response = await this.get('/Test/health');
      return response.success;
    } catch (error) {
      return false;
    }
  }

  // Set auth token
  setAuthToken(token: string) {
    localStorage.setItem('auth_token', token);
  }

  // Clear auth token
  clearAuthToken() {
    localStorage.removeItem('auth_token');
  }

  // Get current auth token
  getAuthToken(): string | null {
    return localStorage.getItem('auth_token');
  }
}

// Export singleton instance
export const apiService = new ApiService();
export default apiService;