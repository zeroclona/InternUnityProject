using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternProject
{
  public class PngDiscolor
  {
    public PngDiscolor()
    {
      var imagePath = string.Empty;
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "png files(*.png)|*.png|All files(*.*)|*.*";
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() == DialogResult.OK){
        imagePath = openFileDialog.FileName;
        var imageIn = new Bitmap(imagePath);
        var width = imageIn.Width;
        var height = imageIn.Height;
        var imageOut = new Bitmap(width, height);

        var inData = imageIn.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, imageIn.PixelFormat);
        var outData = imageOut.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, imageIn.PixelFormat);

        var ptr = inData.Scan0;
        var byteCount = inData.Stride * height;
        var pixels = new byte[byteCount];

        Marshal.Copy(ptr, pixels, 0, byteCount);
        Parallel.For(0, (pixels.Length)/3, i => {
          int colorSum = pixels[3*i] + pixels[3*i + 1] + pixels[3*i + 2];
          byte greyColor = (Byte)(colorSum / 3);
          pixels[3*i] = greyColor;
          pixels[3*i + 1] = greyColor;
          pixels[3*i + 2] = greyColor;
        });

        Marshal.Copy(pixels, 0, outData.Scan0, byteCount);
        imageIn.UnlockBits(inData);
        imageOut.UnlockBits(outData);
        imageOut.Save("out.png", ImageFormat.Png);
      }
    }
  }
}
