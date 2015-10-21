using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychExperimentGenerator
{
    public class Slide
    {
        public enum Gender
        {
            Male,
            Female
        }

        public enum ImageLocation
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        public enum ShapeType
        {
            Square,
            Circle
        }

        public enum BodyType
        {
            Face,
            Body
        }

        public ShapeType Shape { get; set; }
        public Gender ImageGender { get; set; }
        public ImageLocation Location { get; set; }
        public BodyType Body { get; set; }
        public int ImageNumber { get; set; }

        public string FileName
        {
            get { return string.Format("{0}.{1}{2} ({3}) {4}.jpg", (Shape == ShapeType.Circle ? "C" : "S"), (ImageGender == Slide.Gender.Male ? "M" : "F"), (Body == BodyType.Body) ? "B" : "F", ImageNumber, GetCaps(Location)); }
        }

        private string GetCaps(ImageLocation location)
        {
            return String.Concat(Enum.GetName(typeof(ImageLocation), location).Where(l => char.IsUpper(l)));
        }
    }
}
