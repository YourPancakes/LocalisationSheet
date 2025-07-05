import { useCustomQuery } from './useQueryClient';
import { getLanguages, getAvailableLanguages } from '../api';
import type { Language } from '../types';

export const useLanguages = () => {
  const {
    data: languages = [],
    isLoading: isLanguagesLoading,
    error: languagesError,
    refetch: refetchLanguages,
  } = useCustomQuery<Language[]>({
    queryKey: ['languages'],
    queryFn: getLanguages,
  });

  const {
    data: availableLanguages = [],
    isLoading: isAvailableLanguagesLoading,
    error: availableLanguagesError,
  } = useCustomQuery<Language[]>({
    queryKey: ['availableLanguages'],
    queryFn: getAvailableLanguages,
    staleTime: 0,
    refetchOnWindowFocus: true,
  });

  return {
    languages,
    availableLanguages,
    isLoading: isLanguagesLoading || isAvailableLanguagesLoading,
    error: languagesError || availableLanguagesError,
    refetchLanguages,
  };
}; 