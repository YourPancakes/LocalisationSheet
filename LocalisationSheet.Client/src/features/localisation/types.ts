export interface LocalizationRow {
  id: string;
  key: string;
  translations: Record<string, string>;
}

export interface Language {
  id: string;
  code: string;
  name: string;
}

export interface KeyDto {
  id: string;
  name: string;
}

export interface TranslationDto {
  keyId: string;
  keyName: string;
  translations: Array<{
    code: string;
    value: string;
  }>;
}

export interface CreateKeyDto {
  Name: string;
}

export interface UpdateKeyDto {
  Name: string;
}

export interface CreateLanguageDto {
  name: string;
  code: string;
}

export interface UpsertTranslationDto {
  keyId: string;
  languageId: string;
  value: string;
}

export interface PaginationState {
  currentPage: number;
  rowsPerPage: number;
  totalRows: number;
}

export interface TableState {
  rows: LocalizationRow[];
  languages: Language[];
  searchQuery: string;
  pagination: PaginationState;
}

export interface IKeyOperations {
  addKey(data: CreateKeyDto): Promise<KeyDto>;
  deleteKey(id: string): Promise<void>;
  updateKey(id: string, key: string): Promise<KeyDto>;
}

export interface ILanguageOperations {
  addLanguage(data: CreateLanguageDto): Promise<Language>;
  deleteLanguage(id: string): Promise<void>;
  getAvailableLanguages(): Language[];
}

export interface ITranslationOperations {
  upsertTranslation(id: string, languageCode: string, value: string): Promise<TranslationDto>;
  deleteTranslation(id: string, languageCode: string): Promise<void>;
}

export interface IPaginationOperations {
  setCurrentPage(page: number): void;
  setRowsPerPage(rowsPerPage: number): void;
  getPaginationState(): PaginationState;
}

export interface ISearchOperations {
  updateSearchQuery(query: string): void;
  getSearchQuery(): string;
}

export interface ISearchOperationsConfig {
  onSearchChange?: () => void;
} 