import { useState, useCallback } from 'react';
import type { IPaginationOperations, PaginationState } from '../types';

export const usePaginationOperations = (initialState: PaginationState): IPaginationOperations => {
  const [pagination, setPagination] = useState<PaginationState>(initialState);

  const setCurrentPage = useCallback((page: number) => {
    if (page >= 1) {
      setPagination((prev: PaginationState) => ({ ...prev, currentPage: page }));
    }
  }, []);

  const setRowsPerPage = useCallback((rowsPerPage: number) => {
    setPagination((prev: PaginationState) => ({ ...prev, rowsPerPage, currentPage: 1 }));
  }, []);

  const getPaginationState = useCallback((): PaginationState => {
    return pagination;
  }, [pagination]);

  return {
    setCurrentPage,
    setRowsPerPage,
    getPaginationState,
  };
}; 