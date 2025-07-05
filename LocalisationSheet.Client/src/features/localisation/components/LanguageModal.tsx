import React, { useState } from 'react';
import { useLanguages } from '../hooks/useLanguages';
import { useLanguageOperations } from '../hooks/useLanguageOperations';
import type { Language } from '../types';

interface LanguageModalProps {
  isOpen: boolean;
  onClose: () => void;
  position: { top: number; left: number };
}

export const LanguageModal: React.FC<LanguageModalProps> = ({
  isOpen,
  onClose,
  position,
}) => {
  const { availableLanguages } = useLanguages();
  const { addLanguage } = useLanguageOperations(availableLanguages);
  const [selectedLanguages, setSelectedLanguages] = useState<string[]>([]);

  const handleConfirm = async () => {
    for (const code of selectedLanguages) {
      const lang = availableLanguages.find((l: Language) => l.code === code);
      if (lang) {
        await addLanguage({ name: lang.name, code: lang.code });
      }
    }
    setSelectedLanguages([]);
    onClose();
  };

  const handleCancel = () => {
    setSelectedLanguages([]);
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div 
      id="col-popup"
      className="bg-white p-4 rounded-lg shadow-lg"
      style={{
        position: 'absolute',
        top: position.top,
        left: position.left,
        zIndex: 1000,
        width: '200px',
        display: 'block',
      }}
    >
      <h3 className="text-lg mb-2">Select Languages</h3>
      <select
        id="col-language"
        multiple
        className="w-full border border-gray-300 rounded mb-2 p-2 h-[80px]"
        value={selectedLanguages}
        onChange={(e) => {
          const selected = Array.from(e.target.selectedOptions, option => option.value);
          setSelectedLanguages(selected);
        }}
      >
        {availableLanguages.map((language: Language) => (
          <option key={language.code} value={language.code}>
            {language.name}
          </option>
        ))}
      </select>
      <div className="flex justify-end gap-2">
        <button
          id="cancel-add-col"
          onClick={handleCancel}
          className="px-3 py-1 bg-gray-200 rounded"
        >
          Cancel
        </button>
        <button
          id="confirm-add-col"
          onClick={handleConfirm}
          disabled={selectedLanguages.length === 0}
          className="px-3 py-1 bg-blue-500 text-white rounded disabled:opacity-50 disabled:cursor-not-allowed"
        >
          Add
        </button>
      </div>
    </div>
  );
}; 