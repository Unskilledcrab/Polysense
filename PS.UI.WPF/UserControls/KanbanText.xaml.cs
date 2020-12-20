using Syncfusion.UI.Xaml.Kanban;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PS.UI.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for KanbanText.xaml
    /// </summary>
    public partial class KanbanText : UserControl
    {
        // Using a DependencyProperty as the backing store for CardDragEndCommand. This enables
        // animation, styling, binding, etc...
        public static readonly DependencyProperty CardDragEndCommandProperty =
            DependencyProperty.Register("CardDragEndCommand", typeof(ICommand), typeof(KanbanText), new PropertyMetadata());

        public KanbanText()
        {
            InitializeComponent();
            NAME_SfKanban.CardDragEnd += CardDragEnd;
        }

        public ICommand CardDragEndCommand
        {
            get { return (ICommand)GetValue(CardDragEndCommandProperty); }
            set { SetValue(CardDragEndCommandProperty, value); }
        }

        private void CardDragEnd(object sender, KanbanDragEndEventArgs e)
        {
            CardDragEndCommand.Execute(e);
        }
    }
}