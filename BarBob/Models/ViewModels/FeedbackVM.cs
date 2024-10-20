namespace BarBob.Models.ViewModels
{
    public class FeedbackVM
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime FeedbackDate { get; set; }
        public List<string> Images { get; set; }
    }

}
