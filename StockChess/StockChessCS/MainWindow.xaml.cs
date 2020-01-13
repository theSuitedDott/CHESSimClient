using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace StockChessCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool isBoardFlipped;
        private RotateTransform boardRotateTx;
        private DataTemplate boardTemplate;

        public MainWindow()
        {
            InitializeComponent();
            boardRotateTx = new RotateTransform();
        }        

        private void FlipBoard(object sender, RoutedEventArgs e)
        {
            if (!isBoardFlipped)
            {
                boardRotateTx.Angle = 180;
                boardTemplate = (DataTemplate)FindResource("FlippedBoardTemplate");
                isBoardFlipped = true;
            }
            else
            {
                boardRotateTx.Angle = 0;
                boardTemplate = (DataTemplate)FindResource("BoardTemplate");
                isBoardFlipped = false;
            }
            BoardListBox.RenderTransform = boardRotateTx;
            BoardListBox.ItemTemplate = boardTemplate;
        }

        private void ItemChecked(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;
            var itemParent = (MenuItem)item.Parent;
            itemParent.Items.OfType<MenuItem>().Where(i => i.Name != item.Name).ToList().ForEach(m => m.IsChecked = false);
        }
    }
}
