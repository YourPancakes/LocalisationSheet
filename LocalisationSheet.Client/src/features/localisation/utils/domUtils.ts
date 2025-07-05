export interface TextSelection {
  focus: () => void;
  selectAll: () => void;
}

export const createTextSelection = (element: HTMLElement): TextSelection => {
  return {
    focus: () => {
      element.focus();
    },
    selectAll: () => {
      const range = document.createRange();
      const selection = window.getSelection();
      range.selectNodeContents(element);
      range.collapse(false);
      selection?.removeAllRanges();
      selection?.addRange(range);
    },
  };
};

export const focusAndSelect = (element: HTMLElement | null): void => {
  if (!element) return;
  
  const textSelection = createTextSelection(element);
  textSelection.focus();
  textSelection.selectAll();
}; 