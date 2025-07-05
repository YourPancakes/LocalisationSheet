import axios from 'axios';
import { createApiCall } from './utils/apiHelpers';
import type { Language, KeyDto, TranslationDto, CreateKeyDto, CreateLanguageDto, UpdateKeyDto, UpsertTranslationDto } from './types.js';

const isDev = typeof window !== 'undefined' && window.location && window.location.hostname === 'localhost';
export const api = axios.create({
  baseURL: isDev ? 'http://localhost:5000/api/v1.0' : 'http://localhost:5000/api/v1.0',
});

export const getLanguages = (): Promise<Language[]> => 
  createApiCall(api.get('/languages'));

export const getAvailableLanguages = (): Promise<Language[]> => 
  createApiCall(api.get('/languages/available'));

export const createLanguage = (data: CreateLanguageDto): Promise<Language> => 
  createApiCall(api.post('/languages', data));

export const deleteLanguage = (id: string): Promise<void> => 
  createApiCall(api.delete(`/languages/${id}`));

export const getKeys = (): Promise<KeyDto[]> => 
  createApiCall(api.get('/keys'));

export const createKey = (data: CreateKeyDto): Promise<KeyDto> => 
  createApiCall(api.post('/keys', data));

export const updateKey = (id: string, data: UpdateKeyDto): Promise<KeyDto> => 
  createApiCall(api.put(`/keys/${id}`, data));

export const deleteKey = (id: string): Promise<void> => 
  createApiCall(api.delete(`/keys/${id}`));

export const getTranslations = (params: { 
  page?: number; 
  pageSize?: number; 
  filterKey?: string; 
  filterLanguage?: string 
}): Promise<TranslationDto[]> => 
  createApiCall(api.get('/translations', { params }));

export const upsertTranslation = (keyId: string, languageId: string, data: UpsertTranslationDto): Promise<TranslationDto> => 
  createApiCall(api.put(`/translations/${keyId}/${languageId}`, data));

export const deleteTranslation = (keyId: string, languageId: string): Promise<void> => 
  createApiCall(api.delete(`/translations/${keyId}/${languageId}`)); 