using FIMAE.FIMAS.ExpertSystem;
using FIMAE.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FIMAE.ViewModels
{
    public class ExpertTableViewModel:BaseViewModel
    {
        private ExpertSystemTable _table;

        //private ObservableCollection<List<int>> _tableLists;
        public ObservableCollection<List<int>> TableLists { get; set; }
        //{
        //    get { return _tableLists; }
        //    set
        //    {
        //        _tableLists = value;
        //        foreach(var valuesList in _tableLists)
        //        {
        //            foreach(var val in valuesList)
        //            {
        //                _table.ValuesTable[_tableLists.IndexOf(valuesList), valuesList.IndexOf(val)] = val;
        //            }
        //        }

        //        OnPropertyChanged("TableLists");
        //    }
        //}
        
        public string TableName
        {
            get { return String.Format("{0} - {1}", _table.InputVar.Name, _table.OutputVar.Name); }
        }

        public ExpertTableViewModel(ExpertSystemTable table)
        {
            _table = table;

            TableLists = new ObservableCollection<List<int>>();
            for (int i = 0; i < _table.InputVar.Values.Count; i++)
            {
                TableLists.Add(new List<int>());

                for (int j = 0; j < _table.OutputVar.Values.Count; j++)
                {
                    TableLists[i].Add(_table.ValuesTable[i,j]);
                }
            }
        }

        private ICommand _saveTableCommand;
        public ICommand SaveTableCommand
        {
            get
            {
                return _saveTableCommand ?? (_saveTableCommand = new RelayCommand((obj) => { SaveTable(obj); }));
            }
        }

        public void SaveTable(object o)
        {
            foreach (var valuesList in TableLists)
            {
                foreach (var val in valuesList)
                {
                    _table.ValuesTable[TableLists.IndexOf(valuesList), valuesList.IndexOf(val)] = val;
                
                }
            }

            OnPropertyChanged("TableLists");
        }


    }
}
