using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoApp.Services;

namespace TodoApp.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICategoryService _categoryService;
        public MainWindow()
        {
            InitializeComponent();
            _categoryService = new CategoryService();

        }
        
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            var categories = _categoryService.GetCategories();
            gridCategories.ItemsSource = categories;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<CategoryReadModel> all = gridCategories.ItemsSource
              .Cast<CategoryReadModel>().ToList();
            var newCate=_categoryService.Add(new Models.CategoryDto(all
                .Max(e=> e.Id)+1, txtCategory.Text, false));

            all.Add(newCate);
            gridCategories.ItemsSource = all;

        }
    }
}