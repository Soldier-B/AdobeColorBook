using System.Text.RegularExpressions;

namespace AdobeColorBook.Util {

    internal class StringUtil {

        public static string UnescapeMarks(string text) {
            return Regex.Replace(text, @"\^[CR]", match => match.Value == "^R" ? "\u00ae" : "\u00a9");
        }

        public static string EscapeMarks(string text) {
            return Regex.Replace(text, @"\u00ae|\u00a9", match => match.Value == "\u00ae" ? "^R" : "^C");
        }

        public static string ReadNamespace(string text) {
            if (string.IsNullOrEmpty(text))
                return text;			
            return text.StartsWith("$$$") ? text.Split('/')[2] : "";
        }

        public static string ReadValue(string text) {
            if (string.IsNullOrEmpty(text))
                return text;
			return text.StartsWith("$$$") ? text.Split('=')[1] : text;
        }

    }

}
