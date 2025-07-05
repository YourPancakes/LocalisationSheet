import { useMemo, useCallback } from 'react';
import type { LocalizationRow, KeyDto, TranslationDto } from '../types';

export const useDataMapping = (translations: TranslationDto[], keys: KeyDto[]) => {
  const keysMap = useMemo(() => {
    const map = new Map<string, KeyDto>();
    keys.forEach(key => {
      map.set(key.id, key);
      map.set(key.name, key);
    });
    return map;
  }, [keys]);

  const rows: LocalizationRow[] = useMemo(() => 
    translations.map(translation => {
      const keyData = keysMap.get(translation.keyName);
      return {
        id: keyData?.id || translation.keyId,
        key: translation.keyName,
        translations: Object.fromEntries(
          translation.translations.map(t => [t.code, t.value])
        ),
      };
    }).sort((a, b) => a.key.localeCompare(b.key)),
    [translations, keysMap]
  );

  const findKeyId = useCallback((id: string): string => {
    const keyData = keysMap.get(id);
    return keyData?.id || id;
  }, [keysMap]);

  return {
    rows,
    findKeyId,
  };
}; 