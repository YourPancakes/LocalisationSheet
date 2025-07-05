import { useState, useCallback, useMemo, useEffect } from 'react';
import { useLocalizationTable } from './useLocalizationTable';
import { useKeyOperations } from './useKeyOperations';
import { useLanguageOperations } from './useLanguageOperations';
import { useTranslationOperations } from './useTranslationOperations';
import { usePaginationOperations } from './usePaginationOperations';
import { useSearchOperations } from './useSearchOperations';
import type { PaginationState } from '../types';

export const useAppState = () => {
  const [pagination, setPagination] = useState<PaginationState>({
    currentPage: 1,
    rowsPerPage: 10,
    totalRows: 0,
  });

  const searchOperations = useSearchOperations('');
  const searchQuery = searchOperations.getSearchQuery();

  const {
    rows,
    languages,
    availableLanguages,
    totalRows,
    isLoading,
    error,
    keys,
  } = useLocalizationTable({ pagination, searchQuery });

  const keyOperations = useKeyOperations(keys);
  const languageOperations = useLanguageOperations(availableLanguages);
  const translationOperations = useTranslationOperations(languages);

  const paginationOperations = usePaginationOperations({
    currentPage: pagination.currentPage,
    rowsPerPage: pagination.rowsPerPage,
    totalRows,
  });

  useEffect(() => {
    const totalPages = Math.max(1, Math.ceil(totalRows / pagination.rowsPerPage));
    if (pagination.currentPage > totalPages && totalRows > 0) {
      setPagination(prev => ({ ...prev, currentPage: totalPages }));
    }
  }, [totalRows, pagination.currentPage, pagination.rowsPerPage]);

  const handleSearchChange = useCallback((query: string) => {
    searchOperations.updateSearchQuery(query);
    setPagination(prev => ({ ...prev, currentPage: 1 }));
  }, [searchOperations]);

  const handlePageChange = useCallback((page: number) => {
    paginationOperations.setCurrentPage(page);
    setPagination(prev => ({ ...prev, currentPage: page }));
  }, [paginationOperations]);

  const handleRowsPerPageChange = useCallback((rowsPerPage: number) => {
    paginationOperations.setRowsPerPage(rowsPerPage);
    setPagination(prev => ({ ...prev, rowsPerPage, currentPage: 1 }));
  }, [paginationOperations]);

  const tableProps = useMemo(() => ({
    rows,
    languages,
    keyOperations,
    languageOperations,
    translationOperations,
    error,
  }), [
    rows,
    languages,
    keyOperations,
    languageOperations,
    translationOperations,
    error,
  ]);

  const paginationProps = useMemo(() => ({
    currentPage: pagination.currentPage,
    totalRows,
    rowsPerPage: pagination.rowsPerPage,
    onPageChange: handlePageChange,
    onRowsPerPageChange: handleRowsPerPageChange,
  }), [pagination, totalRows, handlePageChange, handleRowsPerPageChange]);

  return {
    searchQuery,
    isLoading,
    handleSearchChange,
    tableProps,
    paginationProps,
  };
}; 