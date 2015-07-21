using AdobeColorBook.Enum;
using AdobeColorBook.IO;
using System;

namespace AdobeColorBook.Components {

    public abstract class BaseColorComponent : IColorComponents {

        public abstract ColorSpace ColorSpace { get; }

		internal abstract void Write(ColorBookWriter writer);

        internal static IColorComponents LoadComponents(ColorBookReader reader, ColorSpace colorSpace) {
            switch (colorSpace) {
                case ColorSpace.RGB:
                    return ComponentRGB.Load(reader);
                case ColorSpace.CMYK:
                    return ComponentCMYK.Load(reader);
                case ColorSpace.Lab:
                    return ComponentLab.Load(reader);
                default:
                    throw new NotSupportedException();
            }
        }

    }

}
