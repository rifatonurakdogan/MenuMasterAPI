using MenuMasterAPI.Domain.Entities.Abstracts;

namespace MenuMasterAPI.Domain.Entities
{
    public class User : IBaseEntity, ISoftDelete,IAuditable
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password {  get; set; }
        public string Email { get; set; }
        public int Age {  get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string? Role { get; set; }
        public Gender Gender {  get; set; }
        public Activity ActivityStatus {  get; set; }
        //public DietType DietType { get; set; }
        public bool IsDeleted {  get; set; }
        public DateTime? LastLogin {  get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual ICollection<FoodRecipe>? FoodRecipes { get; set; }
        public List<string>? CuisineNames { get; set; }
        public List<DietType> DietTypes {  get; set; }
    }
}
