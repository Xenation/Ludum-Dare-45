using UnityEngine;

namespace LD45 {
	public enum Dimension {
		White,
		Black
	}

	public static class DimensionExt {

		public static Dimension Other(this Dimension d) {
			switch (d) {
				case Dimension.White:
					return Dimension.Black;
				case Dimension.Black:
					return Dimension.White;
				default:
					return Dimension.White;
			}
		}

		public static Color GetBGColor(this Dimension d) {
			switch (d) {
				case Dimension.White:
					return Color.black;
				case Dimension.Black:
					return Color.white;
				default:
					return Color.black;
			}
		}

	}
}
