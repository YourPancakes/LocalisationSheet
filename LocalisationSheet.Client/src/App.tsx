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
    <div className="min-h-screen">
      <div className="main-container" style={{ width: '85%', margin: '40px auto 0 auto' }}>
        <div className="mb-4">
          <SearchInput
            value={searchQuery}
            onChange={handleSearchChange}
          />
        </div>
        <div className="mb-4">
          <Pagination {...paginationProps} />
        </div>
        <div className="bg-transparent rounded-none [box-shadow:none] p-0">
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
