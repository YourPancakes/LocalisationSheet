import React from 'react';

interface SearchInputProps {
  value: string;
  onChange: (value: string) => void;
}

export const SearchInput: React.FC<SearchInputProps> = ({ 
  value, 
  onChange
}) => {
  return (
    <input
      id="search-input"
      type="text"
      value={value}
      onChange={(e) => onChange(e.target.value)}
      className="search-nice"
      placeholder="🔍 Search keys..."
      style={{ width: 260 }}
    />
  );
}; 