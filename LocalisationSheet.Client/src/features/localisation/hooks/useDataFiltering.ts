import { useMemo } from 'react';
import type { KeyDto } from '../types';

export const useDataFiltering = (keys: KeyDto[], searchQuery: string) => {
  const filteredKeys = useMemo(() => {
    if (!searchQuery.trim()) return keys;
    
    const query = searchQuery.toLowerCase();
    return keys.filter(key => key.name.toLowerCase().includes(query));
  }, [keys, searchQuery]);

  return {
    filteredKeys,
    totalRows: filteredKeys.length,
  };
}; 