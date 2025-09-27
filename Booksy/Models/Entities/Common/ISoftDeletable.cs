namespace Booksy.Models.Entities.Common
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
