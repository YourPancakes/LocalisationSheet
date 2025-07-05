import React from 'react';
import { useEditableCellLogic } from './EditableCellLogic';

interface EditableCellProps {
  value: string;
  onChange: (value: string) => Promise<void>;
  placeholder?: string;
  className?: string;
}

export const EditableCell: React.FC<EditableCellProps> = ({
  value,
  onChange,
  placeholder = '',
  className = '',
}) => {
  const {
    isEditing,
    editValue,
    cellRef,
    handleClick,
    handleBlur,
    handleKeyDown,
    handleInput,
  } = useEditableCellLogic({ value, onChange });

  return (
    <td
      ref={cellRef}
      className={`editable ${className}`}
      contentEditable={isEditing}
      onClick={handleClick}
      onBlur={handleBlur}
      onKeyDown={handleKeyDown}
      onInput={handleInput}
      suppressContentEditableWarning={true}
    >
      {isEditing ? editValue : (value !== undefined && value !== null ? value : placeholder)}
    </td>
  );
}; 