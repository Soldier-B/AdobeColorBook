using AdobeColorBook.Enum;
using AdobeColorBook.IO;

namespace AdobeColorBook.Components {

    public class ComponentRGB : BaseColorComponent {

        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public override ColorSpace ColorSpace { get { return ColorSpace.RGB; } }        

        internal static IColorComponents Load(ColorBookReader reader) {
            ComponentRGB rgb = new ComponentRGB();

            rgb.Red = reader.ReadByte();
            rgb.Green = reader.ReadByte();
            rgb.Blue = reader.ReadByte();

            return rgb;
        }

        public override string ToString() {
            return string.Format("RGB [ {0} {1} {2} ]", this.Red, this.Green, this.Blue);
        }

		internal override void Write(ColorBookWriter writer)
		{
			writer.Write(this.Red);
			writer.Write(this.Green);
			writer.Write(this.Blue);
		}

    }

}
