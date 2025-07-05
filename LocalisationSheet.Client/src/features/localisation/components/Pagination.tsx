import React from 'react';

interface PaginationProps {
  currentPage: number;
  totalRows: number;
  rowsPerPage: number;
  onPageChange: (page: number) => void;
  onRowsPerPageChange: (rowsPerPage: number) => void;
}

export const Pagination: React.FC<PaginationProps> = ({
  currentPage,
  totalRows,
  rowsPerPage,
  onPageChange,
  onRowsPerPageChange,
}) => {
  const totalPages = Math.max(1, Math.ceil(totalRows / rowsPerPage));
  const startRow = totalRows > 0 ? (currentPage - 1) * rowsPerPage + 1 : 0;
  const endRow = Math.min(currentPage * rowsPerPage, totalRows);
  const hasData = totalRows > 0;

  return (
    <div style={{ display: 'flex', justifyContent: 'flex-end', alignItems: 'center', fontSize: 14 }}>
      <label htmlFor="rows-per-page">Rows per Page:&nbsp;</label>
      <select
        id="rows-per-page"
        value={rowsPerPage}
        onChange={(e) => onRowsPerPageChange(Number(e.target.value))}
        className="border-[1px] border-[solid] border-[#a4a6df] rounded-none [box-shadow:none] outline-[none] [font-family:inherit] text-[16px] bg-[#bbbed8]"
        style={{ marginLeft: 4, marginRight: 16, height: 24 }}
      >
        <option value={10}>10</option>
        <option value={20}>20</option>
        <option value={30}>30</option>
        <option value={40}>40</option>
        <option value={50}>50</option>
      </select>
      <span id="pagination-text" style={{ marginRight: 16 }}>
        {startRow}–{endRow} of {totalRows}
      </span>
      <button
        id="prev-page"
        onClick={() => onPageChange(currentPage - 1)}
        disabled={currentPage === 1 || !hasData}
        className="flex items-center justify-center w-[40px] h-[40px] border-[1px] border-[solid] border-[#a4a6df] bg-[#fff] rounded-full cursor-pointer [transition:box-shadow_0.15s_ease-in-out] [box-shadow:none]"
        style={{ marginRight: 8 }}
      >
        <span className="text-[28px] leading-none">&lt;</span>
      </button>
      <button
        id="next-page"
        onClick={() => onPageChange(currentPage + 1)}
        disabled={currentPage === totalPages || !hasData}
        className="flex items-center justify-center w-[40px] h-[40px] border-[1px] border-[solid] border-[#a4a6df] bg-[#fff] rounded-full cursor-pointer [transition:box-shadow_0.15s_ease-in-out] [box-shadow:none]"
      >
        <span className="text-[28px] leading-none">&gt;</span>
      </button>
    </div>
  );
}; 