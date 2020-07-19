using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfApp1.Windows.Main
{
    class ViewModel : INotifyPropertyChanged
    {
        #region On Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        string _SearchContent;
        public string SearchContent
        {
            get
            {
                return _SearchContent;
            }
            set
            {
                _SearchContent = value;
                OnPropertyChanged("ItemClasses");
            }
        }

        public Models.Item.DbClass SelectedItemClass { get; set; }

        public ObservableCollection<Models.Item.DbClass> ItemClasses
        {
            get
            {
                using var db = new DatabaseContext();

                if (string.IsNullOrEmpty(SearchContent))
                {
                    var query = from itemClass in db.DbClasses orderby itemClass.Name select itemClass;

                    return new ObservableCollection<Models.Item.DbClass>(query);
                }
                else
                {
                    var query = from itemClass in db.DbClasses where itemClass.Name.Contains(SearchContent) select itemClass;

                    return new ObservableCollection<Models.Item.DbClass>(query);
                }
            }
        }

        public ObservableCollection<Models.Item.DbInstance> ItemInstances
        {
            get
            {
                using var db = new DatabaseContext();

                var query = from itemInstance in db.DbInstances orderby itemInstance.ID select itemInstance;

                return new ObservableCollection<Models.Item.DbInstance>(query);
            }
        }

        public ICommand AddItemInstance { get; set; }
        public ICommand CommitItemInstance { get; set; }
        public ICommand CreateItemClass { get; set; }
        public ICommand DeleteItemClass { get; set; }
        public ICommand EditItemClass { get; set; }

        public ViewModel()
        {
            AddItemInstance = new Commands.AddItemInstance(this);
            CommitItemInstance = new Commands.CommitItemInstance(this);
            CreateItemClass = new Commands.CreateItemClass(this);
            DeleteItemClass = new Commands.DeleteItemClass(this);
            EditItemClass = new Commands.EditItemClass(this);
        }

        public void ShowWindow(View view)
        {
            view.DataContext = this;

            view.ShowDialog();
        }
    }
}
