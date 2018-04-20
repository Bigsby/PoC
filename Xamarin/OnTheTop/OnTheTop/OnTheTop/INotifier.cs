using System;

namespace OnTheTop
{
    public interface INotifier
    {
        void Notify(NotifyViewModel viewModel);

        string Phrase { get; }
    }

    public sealed class NotifyViewModel
    {
        public string Text { get; set; }
        public Action Action { get; set; }
        public string ImageUrl { get; set; }
    }
}
