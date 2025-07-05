import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useCallback, useMemo } from 'react';
import { upsertTranslation as upsertTranslationApi, deleteTranslation as deleteTranslationApi } from '../api';
import type { ITranslationOperations, Language } from '../types';

export const useTranslationOperations = (languages: Language[]): ITranslationOperations => {
  const queryClient = useQueryClient();

  const languagesMap = useMemo(() => {
    const map = new Map<string, Language>();
    languages.forEach(lang => {
      map.set(lang.code, lang);
    });
    return map;
  }, [languages]);

  const upsertTranslationMutation = useMutation({
    mutationFn: ({ keyId, languageCode, value }: { keyId: string; languageCode: string; value: string }) => {
      const language = languagesMap.get(languageCode);
      if (!language) {
        throw new Error(`Language with code ${languageCode} not found`);
      }
      return upsertTranslationApi(keyId, language.id, { keyId, languageId: language.id, value });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['translations'] });
    },
  });

  const deleteTranslationMutation = useMutation({
    mutationFn: ({ keyId, languageCode }: { keyId: string; languageCode: string }) => {
      const language = languagesMap.get(languageCode);
      if (!language) {
        throw new Error(`Language with code ${languageCode} not found`);
      }
      return deleteTranslationApi(keyId, language.id);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['translations'] });
    },
  });

  const upsertTranslation = useCallback(async (id: string, languageCode: string, value: string) => {
    return upsertTranslationMutation.mutateAsync({ keyId: id, languageCode, value });
  }, [upsertTranslationMutation]);

  const deleteTranslation = useCallback(async (id: string, languageCode: string) => {
    return deleteTranslationMutation.mutateAsync({ keyId: id, languageCode });
  }, [deleteTranslationMutation]);

  return {
    upsertTranslation,
    deleteTranslation,
  };
}; 