import React, { useState, useCallback } from 'react';
import { TableHeader } from './TableHeader';
import { TableRow } from './TableRow';
import { useTableActions } from './TableActions';
import { handleApiError, handleUserConfirmation } from '../utils/errorHandler';
import type { LocalizationRow, Language, IKeyOperations, ILanguageOperations, ITranslationOperations } from '../types';

interface LocalizationTableProps {
  rows: LocalizationRow[];
  languages: Language[];
  keyOperations: IKeyOperations;
  languageOperations: ILanguageOperations;
  translationOperations: ITranslationOperations;
  error?: Error | null;
}

export const LocalizationTable: React.FC<LocalizationTableProps> = ({
  rows,
  languages,
  keyOperations,
  languageOperations,
  translationOperations,
  error,
}) => {
  const availableLanguages = languageOperations.getAvailableLanguages();
  const [showLangSelector, setShowLangSelector] = useState(false);

  const { handleAddRow, handleDeleteRow, handleUpdateKey } = useTableActions({
    keyOperations,
    onLanguageAdd: () => setShowLangSelector(true),
  });

  const handleAddLanguage = useCallback(() => {
    setShowLangSelector(true);
  }, []);

  const handleSelectLanguage = useCallback(async (e: React.ChangeEvent<HTMLSelectElement>) => {
    const code = e.target.value;
    const lang = availableLanguages.find(l => l.code === code);
    if (lang) {
      try {
        await languageOperations.addLanguage({ name: lang.name, code: lang.code });
        setShowLangSelector(false);
      } catch (err) {
        handleApiError(err, 'add language');
      }
    }
  }, [availableLanguages, languageOperations]);

  const handleRemoveLanguage = useCallback(async (code: string) => {
    const lang = languages.find(l => l.code === code);
    if (lang) {
      if (!handleUserConfirmation(`Are you sure you want to delete language "${lang.name}"?`)) return;
      try {
        await languageOperations.deleteLanguage(lang.id);
      } catch (err) {
        handleApiError(err, 'delete language');
      }
    }
  }, [languages, languageOperations]);

  const handleLangSelectorBlur = useCallback(() => {
    setShowLangSelector(false);
  }, []);

  const handleTranslationChange = useCallback(async (rowId: string, languageCode: string, value: string) => {
    try {
      await translationOperations.upsertTranslation(rowId, languageCode, value);
    } catch (err) {
      handleApiError(err, 'update translation');
    }
  }, [translationOperations]);

  if (error) {
    return (
      <div className="error-message">
        <p>Error loading data: {error.message}</p>
        <button onClick={() => window.location.reload()}>Retry</button>
      </div>
    );
  }

  return (
    <div className="table-wrap relative">
      <div className="table-scroll">
        <table>
          <TableHeader
            languages={languages}
            availableLanguages={availableLanguages}
            showLangSelector={showLangSelector}
            onRemoveLanguage={handleRemoveLanguage}
            onAddLanguage={handleAddLanguage}
            onSelectLanguage={handleSelectLanguage}
            onLangSelectorBlur={handleLangSelectorBlur}
          />
          <tbody id="table-body">
            {rows.map(row => (
              <TableRow
                key={row.id || `row-${row.key}`}
                row={row}
                languages={languages}
                onUpdateKey={handleUpdateKey}
                onDeleteRow={handleDeleteRow}
                onTranslationChange={handleTranslationChange}
              />
            ))}
            <tr id="add-row" className="bg-gray-200">
              <td
                id="add-row-cell"
                colSpan={(Array.isArray(languages) ? languages.length : 0) + 3}
                className="p-0 bg-gray-200"
              >
                <button
                  id="add-button"
                  onClick={handleAddRow}
                  className="flex items-center bg-transparent border-none cursor-pointer p-0"
                >
                  <span className="text-xl mr-2">+</span>
                  <span className="text-sm">Add</span>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
}; 