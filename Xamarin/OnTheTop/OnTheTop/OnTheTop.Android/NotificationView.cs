using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnTheTop.Droid
{
    public class NotificationView : GridLayout
    {
        private readonly IWindowManager _windowManager;
        private readonly NotifyViewModel _viewModel;
        private const int TotalWidth = 800;
        private const int TotalHeigth = 300;

        public NotificationView(Context context, IWindowManager windowManager, NotifyViewModel viewModel)
            : base(context)
        {
            _windowManager = windowManager;
            _viewModel = viewModel;
            Initialize();
        }

        private void Initialize()
        {
            
            

            Alpha = 0;

            //SetPadding(20, 20, 20, 20);

            SetBackgroundColor(Android.Graphics.Color.DarkSlateGray);
            var hasImage = !string.IsNullOrEmpty(_viewModel.ImageUrl);
            if (hasImage)
            {
                ColumnCount = 2;
                var imageView = new ImageView(Context);
                imageView.LayoutParameters = new ViewGroup.LayoutParams(TotalHeigth, TotalHeigth);
                SetImageBitmap(imageView);

                AddView(imageView);
            }

            var text = new TextView(Context)
            {
                Text = _viewModel.Text,
                TextSize = 20,
            };
            text.SetPadding(20, 20, 20, 20);
            
            
            AddView(text, new ViewGroup.LayoutParams(hasImage ? TotalWidth - TotalHeigth : TotalWidth, TotalHeigth));

            Touch += (s, e) =>
            {
                var x = e.Event.GetX();
                var y = e.Event.GetY();
                if (e.Event.Action == MotionEventActions.Up
                    &&
                    (x >= 0 && x <= Width)
                    &&
                    (y >= 0 && y <= Height))
                    Dismiss();
            };

            var alphaAnimator = Animate();
            alphaAnimator.SetDuration(200);
            alphaAnimator.Alpha(1);
            alphaAnimator.Start();
        }

        private void Dismiss()
        {
            var animator = Animate();
            animator.TranslationXBy(Width);
            animator.SetDuration(500);
            animator.SetInterpolator(new Android.Views.Animations.AccelerateInterpolator(2));
            animator.WithEndAction(new Runnable(() =>
            {
                Visibility = ViewStates.Gone;
                _windowManager.RemoveView(this);
            }));
            animator.Start();

            _viewModel.Action?.Invoke();
        }

        private void SetImageBitmap(ImageView imageView)
        {
            Task.Run(async () => {
                var bitmap = await GetImageBitmapFromUrl(_viewModel.ImageUrl);
                if (bitmap != null)
                    Device.BeginInvokeOnMainThread(() => imageView.SetImageBitmap(bitmap));
            });
        }

        private async Task<Bitmap> GetImageBitmapFromUrl(string url)
        {
            try
            {
                Bitmap imageBitmap = null;

                using (var webClient = new WebClient())
                {
                    var imageBytes = await webClient.DownloadDataTaskAsync(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

                return imageBitmap;
            }
            catch
            {
                return null;
            }
        }
    }
}