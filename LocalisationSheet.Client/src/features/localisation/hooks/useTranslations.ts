import { useQuery } from '@tanstack/react-query';
import { getTranslations } from '../api';
import type { TranslationDto } from '../types';

interface UseTranslationsParams {
  page: number;
  pageSize: number;
  filterKey?: string;
}

export const useTranslations = ({ page, pageSize, filterKey }: UseTranslationsParams) => {
  const {
    data: translations = [],
    isLoading,
    error,
    refetch,
  } = useQuery<TranslationDto[]>({
    queryKey: ['translations', page, pageSize, filterKey],
    queryFn: () => getTranslations({
      page,
      pageSize,
      filterKey: filterKey || undefined,
    }),
    initialData: [],
  });

  return {
    translations,
    isLoading,
    error,
    refetch,
  };
}; 