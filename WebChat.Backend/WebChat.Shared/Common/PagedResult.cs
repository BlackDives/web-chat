namespace WebChat.Shared.Common;

public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int PageSize { get; set; }
    public int TotalSize { get; set; }
}