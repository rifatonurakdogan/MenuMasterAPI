namespace MenuMasterAPI.Domain.Entities.Abstracts
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
