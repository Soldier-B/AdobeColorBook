using AdobeColorBook.Enum;
using AdobeColorBook.IO;

namespace AdobeColorBook.Components {

    public class ComponentLab : BaseColorComponent {

        public byte L { get; set; }
        public sbyte a { get; set; }
        public sbyte b { get; set; }        
        public override ColorSpace ColorSpace { get { return Enum.ColorSpace.Lab; } }

        internal static IColorComponents Load(ColorBookReader reader) {
            ComponentLab lab = new ComponentLab();

            lab.L = (byte)(reader.ReadByte() / 2.55d + 0.5d);
            lab.a = (sbyte)(reader.ReadByte() - 128);
            lab.b = (sbyte)(reader.ReadByte() - 128);
            
            return lab;
        }

        public override string ToString() {
            return string.Format("Lab [ {0} {1} {2} ]", this.L, this.a, this.b);
        }

		internal override void Write(ColorBookWriter writer)
		{
			writer.Write((byte)(this.L * 2.55d));
			writer.Write((byte)(this.a + 128));
			writer.Write((byte)(this.b + 128));
		}

    }

}
