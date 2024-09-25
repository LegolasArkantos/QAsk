namespace CrudApp.Models
{


    public class Answer
    {
        public int Id { get; set; } 
        public string Text { get; set; } 
        public int FileQuestionId { get; set; } 
        public FileQuestion? FileQuestion { get; set; } 
    }
}
