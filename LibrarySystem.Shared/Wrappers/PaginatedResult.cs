using LibrarySystem.Shared.Helpers;

namespace LibrarySystem.Shared.Wrappers;

public class PaginatedResult<T>
{
	public IEnumerable<T> Items { get; }
	public int TotalCount { get; }
	public int Page { get; }
	public int PageSize { get; }
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
	public SortByEnum? SortBy { get; }
	public bool Descending { get; } = false;
	public PaginatedResult(IEnumerable<T> items, int totalCount, int page, int pageSize,SortByEnum? sortBy,bool descending)
	{
		Items = items;
		TotalCount = totalCount;
		Page = page;
		PageSize = pageSize;
		SortBy = sortBy;
		Descending = descending;
	}
}
