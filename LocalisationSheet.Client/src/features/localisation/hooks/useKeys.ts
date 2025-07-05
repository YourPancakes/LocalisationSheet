import { useQuery } from '@tanstack/react-query';
import { getKeys } from '../api';
import type { KeyDto } from '../types';

export const useKeys = () => {
  const {
    data: keys = [],
    isLoading,
    error,
    refetch,
  } = useQuery<KeyDto[]>({
    queryKey: ['keys'],
    queryFn: getKeys,
  });

  const sortedKeys = keys.sort((a, b) => a.name.localeCompare(b.name));

  return {
    keys: sortedKeys,
    isLoading,
    error,
    refetch,
  };
}; 