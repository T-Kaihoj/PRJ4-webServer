namespace MVC.ViewModels
{
    public class OutcomeView
    {
        public OutcomeView(string title, long id)
        {
            Title = title;
            Id = id;
        }

        public OutcomeView()
        {
            
        }

        public string Title;
        public long Id;
    }
}