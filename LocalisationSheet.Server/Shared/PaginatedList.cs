﻿namespace LocalisationSheet.Server.Utils
{
    public sealed class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}