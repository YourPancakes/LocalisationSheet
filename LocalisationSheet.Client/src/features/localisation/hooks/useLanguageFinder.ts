import { useCallback } from 'react';
import type { Language } from '../types';

export const useLanguageFinder = (languages: Language[]) => {
  const findLanguageByCode = useCallback((code: string): Language | undefined => {
    return languages.find(l => l.code === code);
  }, [languages]);

  const findLanguageById = useCallback((id: string): Language | undefined => {
    return languages.find(l => l.id === id);
  }, [languages]);

  return {
    findLanguageByCode,
    findLanguageById,
  };
}; 