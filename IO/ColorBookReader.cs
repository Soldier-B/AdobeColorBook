using System.IO;
using System.Text;
using AdobeColorBook.Util;
using System;

namespace AdobeColorBook.IO {

    internal class ColorBookReader : BinaryReader {

		private bool littleEndian;

        public ColorBookReader(Stream colorBook)
            : base(colorBook, Encoding.BigEndianUnicode) {
				this.littleEndian = BitConverter.IsLittleEndian;
        }

        public override short ReadInt16() {						
            return this.littleEndian ? EndianUtil.swap(base.ReadInt16()) : base.ReadInt16();
        }

        public override int ReadInt32() {
			return this.littleEndian ? EndianUtil.swap(base.ReadInt32()) : base.ReadInt32();
        }

        public override long ReadInt64() {
            return this.littleEndian ? EndianUtil.swap(base.ReadInt64()) : base.ReadInt64();
        }

        public override string ReadString() {
            StringBuilder str = new StringBuilder();
            int length = this.ReadInt32();

            while (length-- > 0)
                str.Append(this.ReadChar());            

            return StringUtil.UnescapeMarks(str.ToString());
        }
    }

}
