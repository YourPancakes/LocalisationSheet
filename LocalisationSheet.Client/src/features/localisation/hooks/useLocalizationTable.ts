import { useLanguages } from './useLanguages';
import { useKeys } from './useKeys';
import { useTranslations } from './useTranslations';
import { useDataMapping } from './useDataMapping';
import { useDataFiltering } from './useDataFiltering';
import type { PaginationState } from '../types';

interface UseLocalizationTableParams {
  pagination: PaginationState;
  searchQuery: string;
}

export const useLocalizationTable = ({ pagination, searchQuery }: UseLocalizationTableParams) => {
  const { languages, availableLanguages, isLoading: isLanguagesLoading, error: languagesError } = useLanguages();
  const { keys, isLoading: isKeysLoading, error: keysError } = useKeys();
  const { translations, isLoading: isTranslationsLoading, error: translationsError } = useTranslations({
    page: pagination.currentPage,
    pageSize: pagination.rowsPerPage,
    filterKey: searchQuery,
  });

  const { totalRows } = useDataFiltering(keys, searchQuery);
  const { rows, findKeyId } = useDataMapping(translations, keys);

  return {
    rows,
    languages,
    availableLanguages,
    totalRows,
    isLoading: isLanguagesLoading || isKeysLoading || isTranslationsLoading,
    error: languagesError || keysError || translationsError,
    findKeyId,
    keys,
  };
}; 