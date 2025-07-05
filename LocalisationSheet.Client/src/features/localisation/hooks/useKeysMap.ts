import { useMemo } from 'react';
import type { KeyDto } from '../types';

export const useKeysMap = (keys: KeyDto[]) => {
  return useMemo(() => {
    const map = new Map<string, KeyDto>();
    keys.forEach(key => {
      map.set(key.id, key);
      map.set(key.name, key);
    });
    return map;
  }, [keys]);
}; 