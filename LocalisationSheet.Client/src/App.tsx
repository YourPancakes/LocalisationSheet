import { SearchInput, Pagination, LocalizationTable } from './features/localisation/components';
import { useAppState } from './features/localisation/hooks/useAppState';

function App() {
  const {
    searchQuery,
    isLoading,
    handleSearchChange,
    tableProps,
    paginationProps,
  } = useAppState();

  return (
    <div className="bg-gray-100 min-h-screen">
      <div className="main-container">
        <div className="mb-4">
          <SearchInput
            value={searchQuery}
            onChange={handleSearchChange}
          />
        </div>
        <div className="mb-4">
          <Pagination {...paginationProps} />
        </div>
        <div className="table-wrap">
          {isLoading ? (
            <div className="text-center py-10">Loading...</div>
          ) : (
            <LocalizationTable {...tableProps} />
          )}
        </div>
      </div>
    </div>
  );
}

export default App;
