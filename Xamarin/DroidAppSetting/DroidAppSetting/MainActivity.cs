using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DroidAppSetting
{
    [Activity(Label = "DroidAppSetting", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string _settingsFileName = "settings.json";
        private static string _fileLocation = Android.OS.Environment.GetExternalStoragePublicDirectory("").AbsolutePath;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var e = Android.OS.Environment.GetExternalStoragePublicDirectory("Documents");
            var text = FindViewById<TextView>(Resource.Id.textView1);
            //CreateFile();
            ReadFile();
            try
            {
                var client = new HttpClient();
                var result = await client.GetStringAsync("http://ov-dub-ltp-016/OneViewManager/Service/MealOrdering.svc");
                text.Text = result;
            }
            catch (System.Exception ex)
            {
                var m = ex.Message;
                text.Text = m;
            }


        }

        private string[] ED(string path)
        {
            return Directory.EnumerateDirectories(path).ToArray();
        }
        
        private string[] EF(string path)
        {
            return Directory.EnumerateFiles(path).ToArray();
        }

        private void CreateFile()
        {
            //var filePath = Path.Combine(_fileLocation, _settingsFileName);
            //if (File.Exists(filePath))
            //    return;
            //var settings = new Settings
            //{
            //    Values = new[]
            //    {
            //        new Setting { Key = "settingOne", Value = "settingOneValue" },
            //        new Setting { Key = "settingTwo", Value = "settingTwoValue"}
            //    }
            //};

            //var xmlSerializer = new XmlSerializer(typeof(Settings));
            ////var fileStream = File.Create(filePath);
            ////xmlSerializer.Serialize(fileStream, settings);
            ////fileStream.Flush();
            ////fileStream.Close();

            //var memoryStream = new MemoryStream();

            //xmlSerializer.Serialize(memoryStream, settings);
            //memoryStream.Position = 0;

            //var result = new StreamReader(memoryStream).ReadToEnd();
            //var stop = "here";
        }

        private void ReadFile()
        {
            var filePath = Path.Combine(_fileLocation, _settingsFileName);
            if (!File.Exists(filePath))
                return;

            var fileContent = File.ReadAllText(filePath);

            var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileContent);
            var stop = "here";
        }
    }
}

