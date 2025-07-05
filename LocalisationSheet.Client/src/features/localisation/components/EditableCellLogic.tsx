import { useState, useRef, useEffect, useCallback } from 'react';
import { focusAndSelect } from '../utils/domUtils';

interface EditableCellLogicProps {
  value: string;
  onChange: (value: string) => Promise<void>;
  onEditStart?: () => void;
  onEditEnd?: () => void;
}

export const useEditableCellLogic = ({
  value,
  onChange,
  onEditStart,
  onEditEnd,
}: EditableCellLogicProps) => {
  const [isEditing, setIsEditing] = useState(false);
  const [editValue, setEditValue] = useState(value);
  const cellRef = useRef<HTMLTableCellElement>(null);

  useEffect(() => {
    setEditValue(value);
  }, [value]);

  const focusCell = useCallback(() => {
    if (isEditing) {
      focusAndSelect(cellRef.current);
    }
  }, [isEditing]);

  useEffect(() => {
    focusCell();
  }, [isEditing, focusCell]);

  useEffect(() => {
    focusCell();
  }, [editValue, focusCell]);

  const handleClick = useCallback(() => {
    setIsEditing(true);
    onEditStart?.();
  }, [onEditStart]);

  const handleBlur = useCallback(async () => {
    setIsEditing(false);
    onEditEnd?.();
    if (editValue !== value) {
      await onChange(editValue);
    }
  }, [editValue, value, onChange, onEditEnd]);

  const handleKeyDown = useCallback(async (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') {
      setIsEditing(false);
      onEditEnd?.();
      if (editValue !== value) {
        await onChange(editValue);
      }
    } else if (e.key === 'Escape') {
      setIsEditing(false);
      onEditEnd?.();
      setEditValue(value);
    }
  }, [editValue, value, onChange, onEditEnd]);

  const handleInput = useCallback((e: React.FormEvent<HTMLTableCellElement>) => {
    const newValue = e.currentTarget.innerText;
    setEditValue(newValue);
  }, []);

  return {
    isEditing,
    editValue,
    cellRef,
    handleClick,
    handleBlur,
    handleKeyDown,
    handleInput,
  };
}; 