using System;
using System.Windows.Input;

namespace HC
{
    public interface ISupportIncrementalLoading
    {
        int PageSize { get; set; }

        bool HasMoreItems { get; set; }

        bool IsLoadingIncrementally { get; set; }

        ICommand LoadMoreItemsCommand { get; set; }
    }
}
