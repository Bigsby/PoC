using System;
using System.Drawing;
using System.IO;

namespace ImageCropper
{
    class Program
    {
        const string folder = @"C:\Users\bbernardo\Downloads\Suff\Imgs";
        const string sourceFile = "img.png";
        const string targetFile = "cropped.jpg";
        const string targetBase64 = "base64JPG.txt";

        static void Main(string[] args)
        {
            var cropRect = new Rectangle(0, 364, 720, 720);
            var source = Image.FromFile(Path.Combine(folder, sourceFile));
            var target  =new Bitmap(cropRect.Width, cropRect.Height);

            using (var g = Graphics.FromImage(target))
                g.DrawImage(source, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);

            target.Save(Path.Combine(folder, targetFile), System.Drawing.Imaging.ImageFormat.Jpeg);

            var base64 = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(folder, targetFile)));
            File.WriteAllText(Path.Combine(folder, targetBase64), base64);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
