public class Conversation
{
    public int Id { get; set; }
    public int StoryId { get; set; }
    public int LineNumber { get; set; }
    public string Text { get; set; }
    public string Choices { get; set; }
    public int? NextIndexYes { get; set; }
    public int? NextIndexNo { get; set; }
}
