using GenericWcfClient;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            action.Click += async (s, e) =>
            {
                var syncResponse = Caller<Service.Contract.IServiceContract>.Call(service => service.GetInt());
                AddResultToDisplay(syncResponse);

                var asyncResponse = await Caller<Service.Contract.IServiceContract>.CallAsync(service => service.GetInt());
                AddResultToDisplay(asyncResponse);

                var syncItem = Caller<Service.Contract.IServiceContract>.Call(service => service.GetItem("Item Name", 5));
                AddResultToDisplay(syncItem.Name);

                var asyncItem = await Caller<Service.Contract.IServiceContract>.CallAsync(service => service.GetItem("Async Item Name", 5));
                AddResultToDisplay(asyncItem.Name);
            };
        }

        private void AddResultToDisplay(object result)
        {
            resultDisplay.Text += result + Environment.NewLine;
        }
        
    }

    //public static class DispatcherTaskExtensions
    //{
    //    public static async Task<T> RunTaskAsync<T>(this CoreDispatcher dispatcher,
    //        Func<Task<T>> func, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
    //    {
    //        var taskCompletionSource = new TaskCompletionSource<T>();
    //        await dispatcher.RunAsync(priority, async () =>
    //        {
    //            try
    //            {
    //                taskCompletionSource.SetResult(await func());
    //            }
    //            catch (Exception ex)
    //            {
    //                taskCompletionSource.SetException(ex);
    //            }
    //        });
    //        return await taskCompletionSource.Task;
    //    }

    //    // There is no TaskCompletionSource<void> so we use a bool that we throw away.
    //    public static async Task RunTaskAsync(this CoreDispatcher dispatcher,
    //        Func<Task> func, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal) =>
    //        await RunTaskAsync(dispatcher, async () => { await func(); return false; }, priority);
    //}
}
