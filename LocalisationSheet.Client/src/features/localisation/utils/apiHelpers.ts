import type { AxiosResponse } from 'axios';
import { useQueryClient } from '@tanstack/react-query';

export const extractData = <T>(response: AxiosResponse<T>): T => response.data;

export const createApiCall = <T>(apiCall: Promise<AxiosResponse<T>>): Promise<T> => 
  apiCall.then(extractData);

export const createMutationConfig = (queryKeys: string[]) => {
  const queryClient = useQueryClient();
  return {
    onSuccess: () => {
      queryKeys.forEach(key => {
        queryClient.invalidateQueries({ queryKey: [key] });
      });
    },
  };
}; 