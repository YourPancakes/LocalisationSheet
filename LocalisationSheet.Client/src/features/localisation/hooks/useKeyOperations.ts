import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useCallback } from 'react';
import { createKey, deleteKey as deleteKeyApi, updateKey as updateKeyApi } from '../api';
import { validateGuid } from '../utils/validation';
import { useKeysMap } from './useKeysMap';
import type { IKeyOperations, CreateKeyDto, KeyDto } from '../types';

export const useKeyOperations = (keys: KeyDto[]): IKeyOperations => {
  const queryClient = useQueryClient();

  const addKeyMutation = useMutation({
    mutationFn: createKey,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['keys'] });
      queryClient.invalidateQueries({ queryKey: ['translations'] });
    },
  });

  const keysMap = useKeysMap(keys);

  const findKeyId = useCallback((id: string): string | null => {
    if (!id) return null;
    const keyData = keysMap.get(id);
    return keyData?.id || null;
  }, [keysMap]);

  const addKey = useCallback(async (data: CreateKeyDto) => {
    return addKeyMutation.mutateAsync(data);
  }, [addKeyMutation]);

  const deleteKeyMutation = useMutation({
    mutationFn: deleteKeyApi,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['keys'] });
      queryClient.invalidateQueries({ queryKey: ['translations'] });
    },
  });

  const deleteKey = useCallback(async (id: string) => {
    const actualKeyId = findKeyId(id);
    if (!actualKeyId) {
      throw new Error('Key not found or invalid key ID provided.');
    }
    if (validateGuid(actualKeyId)) {
      return deleteKeyMutation.mutateAsync(actualKeyId);
    } else {
      throw new Error('Deletion is only possible for keys that have an id (Guid).');
    }
  }, [deleteKeyMutation, findKeyId]);

  const updateKeyMutation = useMutation({
    mutationFn: ({ id, key }: { id: string; key: string }) => updateKeyApi(id, { Name: key }),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['keys'] });
      queryClient.invalidateQueries({ queryKey: ['translations'] });
    },
  });

  const updateKey = useCallback(async (id: string, key: string) => {
    const actualKeyId = findKeyId(id);
    if (!actualKeyId) {
      throw new Error('Key not found or invalid key ID provided.');
    }
    if (validateGuid(actualKeyId)) {
      return updateKeyMutation.mutateAsync({ id: actualKeyId, key });
    } else {
      throw new Error('Update is only possible for keys that have an id (Guid).');
    }
  }, [updateKeyMutation, findKeyId]);

  return {
    addKey,
    deleteKey,
    updateKey,
  };
}; 