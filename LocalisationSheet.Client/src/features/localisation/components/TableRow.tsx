import React from 'react';
import { EditableCell } from './EditableCell';
import type { LocalizationRow, Language } from '../types';

interface TableRowProps {
  row: LocalizationRow;
  languages: Language[];
  onUpdateKey: (id: string, key: string) => Promise<void>;
  onDeleteRow: (id: string) => Promise<void>;
  onTranslationChange: (rowId: string, languageCode: string, value: string) => Promise<void>;
}

export const TableRow: React.FC<TableRowProps> = ({
  row,
  languages,
  onUpdateKey,
  onDeleteRow,
  onTranslationChange,
}) => {
  return (
    <tr key={row.id || `row-${row.key}`}>
      <EditableCell
        value={row.key}
        onChange={(value) => onUpdateKey(row.id, value)}
        placeholder=""
      />
      {Array.isArray(languages) && languages.map(language => (
        <EditableCell
          key={`${row.id || row.key}-${language.code}`}
          value={row.translations[language.code] ?? ''}
          onChange={(value) => onTranslationChange(row.id, language.code, value)}
          placeholder=""
        />
      ))}
      <td className="editable"></td>
      <td className="text-center">
        <button
          onClick={() => onDeleteRow(row.id)}
          className="delete-btn"
          title="Delete row"
        >
          &minus;
        </button>
      </td>
    </tr>
  );
}; 