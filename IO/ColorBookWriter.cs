using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AdobeColorBook.Util;
using AdobeColorBook.Components;

namespace AdobeColorBook.IO {

    internal class ColorBookWriter : BinaryWriter {

        private bool littleEndian;

        public ColorBookWriter(Stream colorBook)
            : base(colorBook, Encoding.BigEndianUnicode) {
            this.littleEndian = BitConverter.IsLittleEndian;
        }

        public override void Write(short value) {
            base.Write(this.littleEndian ? EndianUtil.swap(value) : value);
        }

        public override void Write(int value) {
            base.Write(this.littleEndian ? EndianUtil.swap(value) : value);
        }

        public override void Write(long value) {
            base.Write(this.littleEndian ? EndianUtil.swap(value) : value);
        }

        public override void Write(string value) {
            string str = StringUtil.EscapeMarks(value);
            
            this.Write(str.Length);

            for (int i = 0; i < str.Length; i++)
                base.Write(str[i]);
        }

		public void Write(BaseColorComponent component)
		{
			if (component != null)
				component.Write(this);
		}

    }

}
