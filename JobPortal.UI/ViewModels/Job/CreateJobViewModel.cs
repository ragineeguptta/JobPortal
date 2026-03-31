namespace JobPortal.UI.ViewModels.Job
{
    public class CreateJobViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
