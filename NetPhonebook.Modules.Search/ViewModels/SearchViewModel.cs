using Netphonebook.Modules.Common.ViewModels;
using NetPhonebook.Core;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NetPhonebook.Modules.Search.ViewModels
{
    public class SearchViewModel : BindableBase
    {
        private IDataProvider _dataProvider { get; set; }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set 
            { 
                SetProperty(ref searchText, value);
                SearchForRecords(searchText);
            }
        }

        private RecordPresenterViewModel presenterInstance;
        public RecordPresenterViewModel PresenterInstance
        {
            get { return presenterInstance; }
            set { SetProperty(ref presenterInstance, value); }
        }
        
        public SearchViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            PresenterInstance = new RecordPresenterViewModel();
        }

        private void SearchForRecords(string searchedText)
        {
            var dataBase = _dataProvider.GetVirtualModelsDataWithCellData();
            for (int i = 0; i < dataBase.Count; i++)
            {
                var record = dataBase[i];
                if (!IsRecordContains(record, searchedText)) dataBase.Remove(record);
            }
            AddDbToPresenter(dataBase);
        }


        private bool IsRecordContains(VirtualModelsData entry, string searchedText)
        {
            if (entry.DisplayedNumber.Contains(searchedText) || entry.SerialNumber.Contains(searchedText)) return true;
            foreach (var cell in entry.CellDatas)
            {
                if (cell.FirstText.Contains(searchedText) || cell.SecondText.Contains(searchedText)) return true;
                if (cell.extraInfo == null) return false;
                if (cell.extraInfo.Name.Contains(searchedText)) return true;
            }
            return false;
        }
        private void AddDbToPresenter(ObservableCollection<VirtualModelsData> database)
        {
            presenterInstance.CleanCollection();
            foreach (var data in database)
            {
                var cellCount = data.CellDatas.Count;
                var model = _dataProvider.GetVirtualModelsWithCustomization().First(x => x.Id == data.ModelBaseId);
                var textes = new string[cellCount];
                var borderColors = new SolidColorBrush[cellCount];
                var foregroundColors = new SolidColorBrush[cellCount];
                var backgroundColors = new SolidColorBrush[cellCount];
                var fontSizes = new int[cellCount];
                var cornerRadiuses = new int[cellCount];
                var borderSizes = new int[cellCount];

                foreach(var dataCell in data.CellDatas)
                {
                    var cellId = dataCell.CellId;
                    textes[cellId] = dataCell.FirstText + " " + dataCell.SecondText;
                }

                foreach(var modelCell in model.CustomizationCells)
                {
                    var cellId = modelCell.CellId;
                    borderColors[cellId] = HexColorConverter.ToSolidColor(modelCell.BorderColor);
                    foregroundColors[cellId] = HexColorConverter.ToSolidColor(modelCell.ForegroundColor);
                    backgroundColors[cellId] = HexColorConverter.ToSolidColor(modelCell.BackgroundColor);
                    fontSizes[cellId] = Convert.ToInt32(modelCell.FontSize);
                    cornerRadiuses[cellId] = Convert.ToInt32(modelCell.CornerRadius);
                    borderSizes[cellId] = Convert.ToInt32(modelCell.BorderSize);

                }

                presenterInstance.AddToCollection(data.DisplayedNumber, data.SerialNumber, 
                    HexColorConverter.ToSolidColor(model.BackgroundColor), HexColorConverter.ToSolidColor(model.BorderColor),HexColorConverter.ToSolidColor(model.ForegroundColor),
                    Convert.ToInt32(model.FontSize), Convert.ToInt32(model.CornerRadius), Convert.ToInt32(model.BorderSize),
                    textes, backgroundColors, foregroundColors, borderColors, borderSizes, cornerRadiuses, fontSizes);
            }
        }
    }
}
