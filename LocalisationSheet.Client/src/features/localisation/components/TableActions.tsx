import { useCallback } from 'react';
import { handleApiError, handleUserConfirmation, handleUserInput } from '../utils/errorHandler';
import type { IKeyOperations } from '../types';

interface TableActionsProps {
  keyOperations: IKeyOperations;
  onLanguageAdd: () => void;
}

export const useTableActions = ({
  keyOperations,
  onLanguageAdd,
}: TableActionsProps) => {
  const handleAddRow = useCallback(async () => {
    const key = handleUserInput('Enter localization key:');
    if (key && key.trim()) {
      try {
        await keyOperations.addKey({ Name: key.trim() });
      } catch (err) {
        handleApiError(err, 'add key');
      }
    }
  }, [keyOperations]);

  const handleDeleteRow = useCallback(async (id: string) => {
    if (!handleUserConfirmation('Are you sure you want to delete this key?')) return;
    try {
      await keyOperations.deleteKey(id);
    } catch (err) {
      handleApiError(err, 'delete key');
    }
  }, [keyOperations]);

  const handleUpdateKey = useCallback(async (id: string, key: string) => {
    if (!key || !id) return;
    try {
      await keyOperations.updateKey(id, key);
    } catch (err) {
      handleApiError(err, 'update key');
    }
  }, [keyOperations]);

  return {
    handleAddRow,
    handleDeleteRow,
    handleUpdateKey,
    handleAddLanguage: onLanguageAdd,
  };
}; 