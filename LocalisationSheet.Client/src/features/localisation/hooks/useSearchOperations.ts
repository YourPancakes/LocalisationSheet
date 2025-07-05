import { useState, useCallback } from 'react';
import type { ISearchOperations, ISearchOperationsConfig } from '../types';

export const useSearchOperations = (
  initialQuery: string = '',
  config?: ISearchOperationsConfig
): ISearchOperations => {
  const [searchQuery, setSearchQuery] = useState(initialQuery);

  const updateSearchQuery = useCallback((query: string) => {
    setSearchQuery(query);
    if (config?.onSearchChange) {
      config.onSearchChange();
    }
  }, [config?.onSearchChange]);

  const getSearchQuery = useCallback((): string => {
    return searchQuery;
  }, [searchQuery]);

  return {
    updateSearchQuery,
    getSearchQuery,
  };
}; 