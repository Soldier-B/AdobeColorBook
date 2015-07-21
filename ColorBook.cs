using AdobeColorBook.Enum;
using AdobeColorBook.IO;
using AdobeColorBook.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdobeColorBook
{

	public class ColorBook : List<ColorRecord>
	{

		public short Version { get; set; }
		public short Identifier { get; set; }
		public string Title { get; set; }
		public string Prefix { get; set; }
		public string Postfix { get; set; }
		public string Description { get; set; }
		public short PageSize { get; set; }
		public short PageSelectorOffset { get; set; }
		public ColorSpace ColorSpace { get; set; }
		public SpotProcess SpotProcess { get; set; }

		public ColorBook()
			: base()
		{
		}

		public static ColorBook Load(string colorBookPath)
		{
			ColorBook colorbook = new ColorBook();

			using (FileStream file = new FileStream(colorBookPath, FileMode.Open, FileAccess.Read))
			{
				using (ColorBookReader reader = new ColorBookReader(file))
				{
					if (reader.ReadInt32() != Constants.Signature)
						throw new InvalidOperationException("Invalid signature");

					short count = 0;

					colorbook.Version = reader.ReadInt16();
					colorbook.Identifier = reader.ReadInt16();
					colorbook.Title = StringUtil.ReadValue(reader.ReadString());
					colorbook.Prefix = StringUtil.ReadValue(reader.ReadString());
					colorbook.Postfix = StringUtil.ReadValue(reader.ReadString());
					colorbook.Description = StringUtil.ReadValue(reader.ReadString());
					count = reader.ReadInt16();
					colorbook.PageSize = reader.ReadInt16();
					colorbook.PageSelectorOffset = reader.ReadInt16();
					colorbook.ColorSpace = (ColorSpace)reader.ReadInt16();

					for (int i = count; i > 0; i--)
					{
						ColorRecord color = ColorRecord.Load(reader, colorbook.ColorSpace);

						if (string.IsNullOrEmpty(color.Name))
							continue;

						color.Prefix = colorbook.Prefix;
						color.Postfix = colorbook.Postfix;
						colorbook.Add(color);
					}

					colorbook.SpotProcess = SpotProcess.None;

					if (reader.BaseStream.Position < reader.BaseStream.Length)
					{
						// spot/process identifier
						string spotproc = Encoding.ASCII.GetString(reader.ReadBytes(8));
						if (spotproc == "spflspot")
							colorbook.SpotProcess = SpotProcess.Spot;
						if (spotproc == "spflproc")
							colorbook.SpotProcess = SpotProcess.Process;
					}
				}
			}

			return colorbook;
		}

		public void Save(string colorBookPath)
		{
			using (FileStream file = new FileStream(colorBookPath, FileMode.OpenOrCreate, FileAccess.Write))
			{
				this.Save(file);
			}
		}

		public void Save(Stream colorBook)
		{
			using (ColorBookWriter writer = new ColorBookWriter(colorBook))
			{
				writer.Write(Constants.Signature);
				writer.Write(this.Version);
				writer.Write(this.Identifier);
				writer.Write(this.Title);
				writer.Write(this.Prefix);
				writer.Write(this.Postfix);
				writer.Write(this.Description);
				writer.Write((short)this.Count);
				writer.Write(this.PageSize);
				writer.Write(this.PageSelectorOffset);
				writer.Write((short)this.ColorSpace);

				this.ForEach(c => c.Write(writer));

				switch (this.SpotProcess)
				{
					case SpotProcess.Spot:
						writer.Write(Encoding.ASCII.GetBytes("spflspot"));
						break;
					case SpotProcess.Process:
						writer.Write(Encoding.ASCII.GetBytes("spflproc"));
						break;
				}				
			}
		}

	}

}
