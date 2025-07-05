export const handleApiError = (error: unknown, operation: string): void => {
  alert(`Failed to ${operation}. Please try again.`);
  console.error(`Error during ${operation}:`, error);
};

export const handleUserConfirmation = (message: string): boolean => {
  return window.confirm(message);
};

export const handleUserInput = (message: string): string | null => {
  return window.prompt(message);
}; 