using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HC
{
    public class IncrementalViewModel :  ISupportIncrementalLoading
    {
        public int PageSize { get; set; } = 6;

        public ICommand LoadMoreItemsCommand { get; set; }

        public bool IsLoadingIncrementally { get; set; }

        public bool HasMoreItems { get; set; }

        public IncrementalViewModel()
        {
            LoadMoreItemsCommand = new Command(async () => await LoadMoreItems());
        }

        async Task LoadMoreItems()
        {
            IsLoadingIncrementally = true;

            // Download data from a service, etc.
            // Add the newly download data to a collection

            //HasMoreItems = ...

        IsLoadingIncrementally = false;
        }
    }
}
