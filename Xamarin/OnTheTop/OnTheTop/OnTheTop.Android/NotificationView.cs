using Android.Content;
using Android.Graphics;
using Android.Support.V4.View;
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
        private GestureDetector _detector;

        public NotificationView(Context context, IWindowManager windowManager, NotifyViewModel viewModel)
            : base(context)
        {
            _windowManager = windowManager;
            _viewModel = viewModel;
            Initialize();
        }

        private void Initialize()
        {
            //_detector = new GestureDetector(Context, listener);
            //var listener = new SwipeListener(Context);
            //listener.Click += (s, e) => Dismiss("click");
            //listener.Swipe += (s, e) => Dismiss("swipe");
            //SetOnTouchListener(listener);
            initialX = GetX();
            Alpha = 0;

            SetBackgroundColor(Android.Graphics.Color.DarkSlateGray);
            var hasImage = !string.IsNullOrEmpty(_viewModel.ImageUrl);
            var hasTitle = !string.IsNullOrEmpty(_viewModel.Title);

            var mainRow = new TableRow(Context);

            var textsTable = new TableLayout(Context);

            if (hasTitle)
            {
                var titleRow = new TableRow(Context);
                var title = new TextView(Context)
                {
                    Text = _viewModel.Title,
                    TextSize = 20,
                };
                title.SetPadding(20, 20, 20, 20);
                title.SetTypeface(null, TypefaceStyle.Bold);
                titleRow.AddView(title);
                textsTable.AddView(titleRow);
            }

            var textRow = new TableRow(Context);
            var text = new TextView(Context)
            {
                Text = _viewModel.Text,
                TextSize = 18,
            };
            text.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            text.SetPadding(20, 20, 20, 20);
            textRow.AddView(text);
            textsTable.AddView(textRow);

            mainRow.AddView(textsTable, new TableRow.LayoutParams(hasImage ? 1 : 0));
            AddView(mainRow);

            //Touch += (s, e) =>
            //{
            //    var x = e.Event.GetX();
            //    var y = e.Event.GetY();
            //    if (e.Event.Action == MotionEventActions.Up
            //        &&
            //        (x >= 0 && x <= Width)
            //        &&
            //        (y >= 0 && y <= Height))
            //        Dismiss();
            //};

            var alphaAnimator = Animate();
            alphaAnimator.SetDuration(200);
            alphaAnimator.Alpha(1);
            alphaAnimator.Start();
        }

        float initialX;
        float lastX;
        //float lastY;
        int pointerId;
        float distance;
        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    lastX = e.GetX();
                    //lastY = e.GetY();
                    pointerId = e.GetPointerId(e.ActionIndex);
                    break;
                case MotionEventActions.Move:
                    e.FindPointerIndex(pointerId);
                    var newX = e.GetX();
                    //var newY = e.GetY();

                    distance = newX - lastX;

                    if (distance > 0)
                        SetX(distance);
                    break;
                case MotionEventActions.Cancel:
                    break;
                case MotionEventActions.Up:
                    if (System.Math.Abs(distance) < 20)
                        Dismiss("click");
                    else if (distance > Width / 4)
                        Dismiss("swipe");
                    else
                        SetX(initialX);
                    break;
                case MotionEventActions.PointerUp:
                    break;
            }
            return true;
        }

        private void Dismiss(string action)
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
            Task.Run(async () =>
            {
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

        public class SwipeListener : Java.Lang.Object, IOnTouchListener
        {
            private readonly Context _context;
            private GestureDetector _gestureDetector;

            public event EventHandler Swipe;
            public event EventHandler Click;

            public SwipeListener(Context context)
            {
                _context = context;
                var listener = new GestureListener();
                listener.Click += (s, e) => Click?.Invoke(this, EventArgs.Empty);
                listener.Swipe += (s, e) => Swipe?.Invoke(this, EventArgs.Empty);
                _gestureDetector = new GestureDetector(_context, listener);
            }

            public bool OnTouch(Android.Views.View v, MotionEvent e)
            {
                return _gestureDetector.OnTouchEvent(e);
            }

            private class GestureListener : GestureDetector.SimpleOnGestureListener
            {
                private const int SWIPE_THRESHOLD = 100;
                private const int SWIPE_VELOCITY_THRESHOLD = 100;

                public event EventHandler Swipe;
                public event EventHandler Click;

                public override bool OnSingleTapUp(MotionEvent e)
                {
                    Click?.Invoke(this, EventArgs.Empty);
                    return true;
                    //return base.OnSingleTapUp(e);
                }

                public override bool OnDown(MotionEvent e)
                {
                    return true;
                }

                public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
                {
                    var result = false;
                    try
                    {
                        float diffY = e2.GetY() - e1.GetY();
                        float diffX = e2.GetX() - e1.GetX();
                        if (System.Math.Abs(diffX) > System.Math.Abs(diffY))
                        {
                            if (System.Math.Abs(diffX) > SWIPE_THRESHOLD && System.Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
                            {
                                if (diffX > 0)
                                {
                                    Swipe?.Invoke(this, EventArgs.Empty);
                                }
                                result = true;
                            }
                        }
                    }
                    catch
                    {
                        return false;
                    }
                    return result;
                }
            }
        }


    }
}