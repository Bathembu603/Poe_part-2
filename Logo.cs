using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace voice_greeting
{
    public class Logo
    {
        //constuctor
        public Logo()
        {
            //get the full path
            string path_project = AppDomain.CurrentDomain.BaseDirectory;

            //then replace the bin\\Deburg\\
            string new_path_project = path_project.Replace("bin\\Debug\\", "");

            //then combine the project full path and the the image name with the
            //format
            string full_path = Path.Combine(new_path_project, "logo (2).jpg");

            //then start working on the logo
            //with the Ascii
            Bitmap image = new Bitmap(full_path);
            image = new Bitmap(image, new Size(210, 200));


            //for loop , for inner and the outer
            //nested
            for (int height = 0; height < image.Height; height++)
            {
                //then now work on width
                for (int width = 0; width < image.Width; width++)
                {
                    //now lets work on the asci design
                    Color pixelColor = image.GetPixel(width, height);
                    int color = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    //now make use of the char
                    char ascii_design = color > 200 ? '.' : color > 150 ? '*' : color > 100 ? 'O' : color > 50 ? '#' : '@';
                    Console.Write(ascii_design);//output the design
                }//end of the for loop for inner
                Console.WriteLine();//skip the line
            }//end of the for loop outer


        }

        internal static void DisplayLogo()
        {
            throw new NotImplementedException();
        }
    }
}