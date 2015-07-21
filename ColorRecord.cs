using AdobeColorBook.Components;
using AdobeColorBook.Enum;
using AdobeColorBook.IO;
using AdobeColorBook.Util;
using System.Text;

namespace AdobeColorBook {

    public class ColorRecord {

        public string Name { get; set; }
        public string Code { get; set; }
        public IColorComponents Components { get; set; }
        public ColorSpace ColorSpace {
            get {
                return this.Components.ColorSpace;
            }
        }
        internal string Prefix { get; set; }
        internal string Postfix { get; set; }

        public string FullName {
            get {
                return this.Prefix + this.Name + this.Postfix;
            }
        }

        public ColorRecord() {
        }

        public ColorRecord(string name, string code, IColorComponents components) {
            this.Name = name;
            this.Code = code;
            this.Components = components;
        }

        internal static ColorRecord Load(ColorBookReader reader, ColorSpace colorSpace) {
            ColorRecord color = new ColorRecord();

			color.Name = StringUtil.ReadValue(reader.ReadString());
            color.Code = Encoding.ASCII.GetString(reader.ReadBytes(6));
            color.Components = BaseColorComponent.LoadComponents(reader, colorSpace);

            return color;
        }
        
        internal void Write(ColorBookWriter writer) {
			writer.Write(this.Name);
			writer.Write(Encoding.ASCII.GetBytes(this.Code));
			writer.Write((BaseColorComponent)this.Components);
        }

    }

}
