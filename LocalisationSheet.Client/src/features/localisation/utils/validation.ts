export const validateGuid = (id: string): boolean => {
  return /^[0-9a-fA-F-]{36}$/.test(id);
};