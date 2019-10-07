using System.Collections.Generic;
using UnityEngine;

namespace LD45 {
	
	public static class InputManager {

		private struct AxisOverride {
			public string name;
			public KeyCode positive;
			public KeyCode negative;

			public AxisOverride(string n, KeyCode pos, KeyCode neg) {
				name = n;
				positive = pos;
				negative = neg;
			}
		}

		private struct ButtonOverride {
			public string name;
			public KeyCode code;

			public ButtonOverride(string n, KeyCode c) {
				name = n;
				code = c;
			}
		}

		public enum Mode {
			Classic,
			ForceQWERTY,
			ForceAZERTY
		}

		public static Mode mode = Mode.Classic;

		private static AxisOverride[] azertyAxisOverrides = new AxisOverride[3] {
			new AxisOverride("Horizontal", KeyCode.D, KeyCode.Q),
			new AxisOverride("Vertical", KeyCode.Z, KeyCode.S),
			new AxisOverride("Rotate", KeyCode.E, KeyCode.A)
		};
		
		private static AxisOverride[] qwertyAxisOverrides = new AxisOverride[3] {
			new AxisOverride("Horizontal", KeyCode.D, KeyCode.A),
			new AxisOverride("Vertical", KeyCode.W, KeyCode.S),
			new AxisOverride("Rotate", KeyCode.E, KeyCode.Q)
		};

		public static float GetAxisRaw(string name) {
			switch (mode) {
				default:
				case Mode.Classic:
					return Input.GetAxisRaw(name);
				case Mode.ForceAZERTY:
					return GetOverridenAxis(name, azertyAxisOverrides);
				case Mode.ForceQWERTY:
					return GetOverridenAxis(name, qwertyAxisOverrides);
			}
		}

		private static float GetOverridenAxis(string name, AxisOverride[] overrides) {
			for (int i = 0; i < overrides.Length; i++) {
				if (overrides[i].name != name) continue;
				if (Input.GetKey(overrides[i].positive)) {
					return 1f;
				} else if (Input.GetKey(overrides[i].negative)) {
					return -1f;
				} else {
					return 0f;
				}
			}
			return 0f;
		}

	}
}
