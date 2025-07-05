export const validateGuid = (id: string): boolean => {
  return /^[0-9a-fA-F-]{36}$/.test(id);
};

export const validateKeyName = (name: string): boolean => {
  return name.trim().length > 0;
};

export const validateLanguageCode = (code: string): boolean => {
  return /^[a-z]{2}(-[A-Z]{2})?$/.test(code);
}; 