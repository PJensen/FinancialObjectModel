using System;

namespace FinancialObjectModel
{
	/// <summary>
	///   ExtensionMethods
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		///   Safes the substring.
		/// </summary>
		/// <param name="obj"> The object. </param>
		/// <param name="start"> The start. </param>
		/// <param name="length"> The length. </param>
		/// <returns> </returns>
		/// <exception cref="System.ArgumentNullException">obj</exception>
		public static string SafeSubstring (this object obj, int start, int length)
		{
			if (obj == null) {
				throw new ArgumentNullException ("obj");
			}

			var str = obj.ToString ();
			var tmp = string.Empty;

			for (var index = start; index < str.Length; index++) {
				if (index >= start && tmp.Length < length) {
					tmp += str [index];
				} else if (tmp.Length >= length) {
					break;
				}
			}

			return tmp;
		}

		/// <summary>
		///   Returns the date code for the specified date.
		///   <remarks>
		///     January = F February = G March = H April = J May = K June = M July = N August = Q September = U October = V November = X December = Z
		///   </remarks>
		/// </summary>
		/// <param name="dt"> The dt. </param>
		/// <returns> </returns>
		public static char ToDateCode (this DateTime dt)
		{
			switch (dt.Month) {
			case 1:
				return 'F';
			case 2:
				return 'G';
			case 3:
				return 'H';
			case 4:
				return 'J';
			case 5:
				return 'K';
			case 6:
				return 'N';
			case 7:
				return 'M';
			case 8:
				return 'Q';
			case 9:
				return 'U';
			case 10:
				return 'V';
			case 11:
				return 'X';
			case 12:
				return 'Z';
			}

			throw new InvalidOperationException ("unhandled month when converting date to date code");
		}
	}
}

