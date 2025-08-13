namespace GeekQuiz.Models.DTOs
{
    public class CategoriesDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? Status  { get; set; }
    }
    
    public class CategoriesByStatusDTO
    {
        public List<CategoriesDTO> ActivesCategories { get; set; }
        public List<CategoriesDTO> InactivesCategories { get; set; }

    }
}
