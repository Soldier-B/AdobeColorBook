using AdobeColorBook.Enum;
using AdobeColorBook.IO;

namespace AdobeColorBook.Components
{

	public class ComponentCMYK : BaseColorComponent
	{

		public byte Cyan { get; set; }
		public byte Magenta { get; set; }
		public byte Yellow { set; get; }
		public byte Key { set; get; }
		public override ColorSpace ColorSpace { get { return Enum.ColorSpace.CMYK; } }

		internal static IColorComponents Load(ColorBookReader reader)
		{
			ComponentCMYK cmyk = new ComponentCMYK();

			cmyk.Cyan = (byte)((255d - reader.ReadByte()) / 2.55d + 0.5d);
			cmyk.Magenta = (byte)((255d - reader.ReadByte()) / 2.55d + 0.5d);
			cmyk.Yellow = (byte)((255d - reader.ReadByte()) / 2.55d + 0.5d);
			cmyk.Key = (byte)((255d - reader.ReadByte()) / 2.55d + 0.5d);

			return cmyk;
		}

		public override string ToString()
		{
			return string.Format("CMYK [ {0} {1} {2} {3} ]", this.Cyan, this.Magenta, this.Yellow, this.Key);
		}

		internal override void Write(ColorBookWriter writer)
		{
			writer.Write((byte)(255 - (this.Cyan * 2.55d)));
			writer.Write((byte)(255 - (this.Magenta * 2.55d)));
			writer.Write((byte)(255 - (this.Yellow * 2.55d)));
			writer.Write((byte)(255 - (this.Key * 2.55d)));
		
		}

	}

}
