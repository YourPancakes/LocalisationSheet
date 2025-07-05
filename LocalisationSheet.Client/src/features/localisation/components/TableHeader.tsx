import React from 'react';
import type { Language } from '../types';

interface TableHeaderProps {
  languages: Language[];
  availableLanguages: Language[];
  showLangSelector: boolean;
  onRemoveLanguage: (code: string) => void;
  onAddLanguage: () => void;
  onSelectLanguage: (e: React.ChangeEvent<HTMLSelectElement>) => void;
  onLangSelectorBlur: () => void;
}

export const TableHeader: React.FC<TableHeaderProps> = ({
  languages,
  availableLanguages,
  showLangSelector,
  onRemoveLanguage,
  onAddLanguage,
  onSelectLanguage,
  onLangSelectorBlur,
}) => {
  return (
    <thead>
      <tr id="header-row">
        <th className="font-bold" data-lang="key">
          Key
        </th>
        {Array.isArray(languages) && languages.map(language => (
          <th key={language.code} data-lang={language.name}>
            <div className="flex items-center justify-between">
              <span>{language.name}</span>
              <button
                onClick={() => onRemoveLanguage(language.code)}
                className="ml-2 text-[#ef4444] cursor-pointer text-[18px]"
                title={`Remove ${language.name}`}
              >
                &times;
              </button>
            </div>
          </th>
        ))}
        <th
          id="add-lang-header"
          className={`cursor-pointer text-[18px] font-bold relative ${
            availableLanguages.length && !showLangSelector ? 'cursor-pointer' : 'cursor-not-allowed'
          }`}
          onClick={availableLanguages.length && !showLangSelector ? onAddLanguage : undefined}
          title={availableLanguages.length && !showLangSelector ? "Add Language" : "No available languages"}
        >
          {!showLangSelector && '+'}
          {showLangSelector && (
            <select
              autoFocus
              onBlur={onLangSelectorBlur}
              onChange={onSelectLanguage}
              className="absolute top-full left-0 z-10"
              defaultValue=""
            >
              <option value="" disabled>Select language</option>
              {availableLanguages.map(lang => (
                <option key={lang.code} value={lang.code}>{lang.name}</option>
              ))}
            </select>
          )}
        </th>
        <th>Delete</th>
      </tr>
    </thead>
  );
}; 