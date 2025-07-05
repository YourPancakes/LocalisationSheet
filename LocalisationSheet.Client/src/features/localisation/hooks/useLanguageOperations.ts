import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useCallback } from 'react';
import { createLanguage, deleteLanguage as deleteLanguageApi } from '../api';
import type { ILanguageOperations, CreateLanguageDto, Language } from '../types';

const INVALIDATE_KEYS = ['languages', 'availableLanguages', 'translations'];

export const useLanguageOperations = (availableLanguages: Language[]): ILanguageOperations => {
  const queryClient = useQueryClient();

  const addLanguageMutation = useMutation({
    mutationFn: createLanguage,
    onSuccess: () => {
      INVALIDATE_KEYS.forEach(key => {
        queryClient.invalidateQueries({ queryKey: [key] });
      });
    },
  });

  const addLanguage = useCallback(async (data: CreateLanguageDto) => {
    return addLanguageMutation.mutateAsync(data);
  }, [addLanguageMutation]);

  const deleteLanguageMutation = useMutation({
    mutationFn: deleteLanguageApi,
    onSuccess: () => {
      INVALIDATE_KEYS.forEach(key => {
        queryClient.invalidateQueries({ queryKey: [key] });
      });
    },
  });

  const deleteLanguage = useCallback(async (id: string) => {
    return deleteLanguageMutation.mutateAsync(id);
  }, [deleteLanguageMutation]);

  const getAvailableLanguages = useCallback(() => {
    return availableLanguages;
  }, [availableLanguages]);

  return {
    addLanguage,
    deleteLanguage,
    getAvailableLanguages,
  };
}; 