using MenuMasterAPI.Domain.Entities.Abstracts;

namespace MenuMasterAPI.Domain.Entities
{
    public class FoodRecipe : IBaseEntity, IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string Recipe { get; set; }
        public MealType MealType { get; set; }
        public bool IsLiked {  get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
