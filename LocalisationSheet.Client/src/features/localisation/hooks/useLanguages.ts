import { useQuery } from '@tanstack/react-query';
import { getLanguages, getAvailableLanguages } from '../api';
import type { Language } from '../types';

export const useLanguages = () => {
  const {
    data: languages = [],
    isLoading: isLanguagesLoading,
    error: languagesError,
    refetch: refetchLanguages,
  } = useQuery<Language[]>({
    queryKey: ['languages'],
    queryFn: getLanguages,
  });

  const {
    data: availableLanguages = [],
    isLoading: isAvailableLanguagesLoading,
    error: availableLanguagesError,
  } = useQuery<Language[]>({
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