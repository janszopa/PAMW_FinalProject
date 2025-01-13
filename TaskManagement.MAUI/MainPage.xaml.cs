namespace TaskManagement.MAUI
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Children.Add(new TasksPage { Title = "Tasks" });
            Children.Add(new PeoplePage { Title = "People" });
            Children.Add(new CategoriesPage { Title = "Categories" });
        }
    }
}