using GGKService.Common.Extensions;

namespace GGKService.Common.Utils{

	public static class HtmlUtil{

		/// <summary>
		/// tr for item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string Tr(string item){
			return string.Format("<tr>{0}</tr>", item);
		}

		/// <summary>
		/// td for item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string Td(string item){
			return string.Format("<td>{0}</td>", item);
		}

		/// <summary>
		/// td for item + bold text
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string Tdb(string item){
			return string.Format("<td style=\"text-align:center\"><b>{0}</b></td>", item);
		}

		/// <summary>
		/// td for item + right align
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string Tdr(string item){
			return string.Format("<td style=\"text-align:right\">{0}</td>", item);
		}

		/// <summary>
		/// td for item + right align + bold text
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string Tdbr(string item){
			return string.Format("<td style=\"text-align:right\"><b>{0}</b></td>", item);
		}

		/// <summary>
		/// tag a href and text
		/// </summary>
		/// <param name="url"></param>
		/// <param name="text"></param>
		/// <param name="style"></param>
		/// <returns></returns>
		public static string A(string url, string text, string style){
			return string.Format("<a {0} href=\"{1}\">{2}</a>", style.IsNullOrEmpty() ? "" : "style=\"" + style + "\"", url, text);
		}

	}
}
