import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import type { QueryKey, QueryFunction, MutationFunction } from '@tanstack/react-query';

export interface QueryOptions<T> {
  queryKey: QueryKey;
  queryFn: QueryFunction<T>;
  staleTime?: number;
  refetchOnWindowFocus?: boolean;
  initialData?: T;
}

export interface MutationOptions<TData, TVariables> {
  mutationFn: MutationFunction<TData, TVariables>;
  onSuccess?: (data: TData, variables: TVariables) => void;
  onError?: (error: Error, variables: TVariables) => void;
}

export const useCustomQuery = <T>(options: QueryOptions<T>) => {
  return useQuery(options);
};

export const useCustomMutation = <TData, TVariables>(options: MutationOptions<TData, TVariables>) => {
  return useMutation(options);
};

export const useCustomQueryClient = () => {
  return useQueryClient();
}; 