namespace AdobeColorBook.Util {

    internal static class EndianUtil {

        public static short swap(short value) {
            return (short)((((int)value & 0xff) << 8) | (int)((value >> 8) & 0xff));
        }

        public static int swap(int value) {
            return (((int)swap((short)value) & 0xffff) << 16) | ((int)swap((short)(value >> 16)) & 0xffff);
        }

        public static long swap(long value) {
            return (((long)swap((int)value) & 0xffffffff) << 32) | ((long)swap((int)(value >> 32)) & 0xffffffff);
        }

    }

}
