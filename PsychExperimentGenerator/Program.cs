using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace PsychExperimentGenerator
{
    class Program
    {
        private static int height = 3000;
        private static int width = 4000;
        private static int overlaySize = 1000;

        private static Bitmap myBitmap;
        static void Main(string[] args)
        {


            Enum.GetValues(typeof(Slide.Gender)).OfType<Slide.Gender>().ToList().ForEach(gender =>
            {
                Enum.GetValues(typeof(Slide.BodyType)).OfType<Slide.BodyType>().ToList().ForEach(bodyType =>
                {
                    Enumerable.Range(1, 16).ToList().ForEach(number =>
                    {
                        var imageToCompose = Image.FromFile(@"in\" + string.Format("{0}{1} {2}.jpg", (gender == Slide.Gender.Male ? "M" : "F"), number, (bodyType == Slide.BodyType.Body ? "Body" : "Face")));

                        Enum.GetValues(typeof(Slide.ImageLocation)).OfType<Slide.ImageLocation>().ToList().ForEach(location =>
                        {
                            Enum.GetValues(typeof(Slide.ShapeType)).OfType<Slide.ShapeType>().ToList().ForEach(shape =>
                            {
                                var slide = new Slide
                                {
                                    ImageGender = gender,
                                    ImageNumber = number,
                                    Location = location,
                                    Shape = shape,
                                    Body = bodyType
                                };

                                int imageXoffset = 0;
                                int imageYoffset = 0;
                                int shapeXoffset = 0;
                                int shapeYoffset = 0;

                                switch (location)
                                {
                                    case Slide.ImageLocation.TopLeft:
                                        shapeXoffset = width - overlaySize;
                                        shapeYoffset = height - overlaySize;
                                        break;
                                    case Slide.ImageLocation.TopRight:
                                        imageXoffset = width - overlaySize;
                                        shapeYoffset = height - overlaySize;
                                        break;

                                    case Slide.ImageLocation.BottomLeft:
                                        imageYoffset = height - overlaySize;
                                        shapeXoffset = width - overlaySize;
                                        break;
                                    case Slide.ImageLocation.BottomRight:
                                        imageXoffset = width - overlaySize;
                                        imageYoffset = height - overlaySize;
                                        break;
                                }

                                Image myBitmap = new Bitmap(width, height);

                                using (Graphics g = Graphics.FromImage(myBitmap))
                                {
                                    g.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                                    if (shape == Slide.ShapeType.Circle)
                                        g.FillEllipse(Brushes.Black, new Rectangle(shapeXoffset, shapeYoffset, overlaySize, overlaySize));
                                    else
                                        g.FillRectangle(Brushes.Black, new Rectangle(shapeXoffset, shapeYoffset, 1000, 1000));

                                    g.DrawImage(imageToCompose, new Rectangle(imageXoffset, imageYoffset, overlaySize, overlaySize));

                                    myBitmap.Save(@"img\" + slide.FileName, ImageFormat.Jpeg);

                                    myBitmap.Dispose();
                                }
                            });
                        });
                    });
                });
            });


        }
    }
}
