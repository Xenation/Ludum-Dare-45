using UnityEngine;

namespace LD45 {
	public class PhysicalButton : MonoBehaviour {

		public bool lowerOnHover = true;

		public delegate void ClickedHandler();
		public event ClickedHandler onClick;
		public delegate void ClickedHandlerRef(PhysicalButton btn);
		public event ClickedHandlerRef onClickRef;

		private void OnMouseEnter() {
			Renderer renderer = GetComponent<Renderer>();
			if (renderer.materials.Length > 1) {
				Material mat = mat = renderer.materials[1];
				if (mat.HasProperty("_Emis")) {
					Color color = mat.GetColor("_Emis");
					color *= 1.5f;
					mat.SetColor("_Emis", color);
				} else if (mat.HasProperty("_EmissionColor")) {
					Color color = mat.GetColor("_EmissionColor");
					color *= 1.25f;
					mat.SetColor("_EmissionColor", color);
				}
			}
			if (lowerOnHover) {
				transform.position -= transform.up * 0.1f;
			}
		}

		private void OnMouseExit() {
			Renderer renderer = GetComponent<Renderer>();
			if (renderer.materials.Length > 1) {
				Material mat = mat = renderer.materials[1];
				if (mat.HasProperty("_Emis")) {
					Color color = mat.GetColor("_Emis");
					color /= 1.5f;
					mat.SetColor("_Emis", color);
				} else if (mat.HasProperty("_EmissionColor")) {
					Color color = mat.GetColor("_EmissionColor");
					color /= 1.25f;
					mat.SetColor("_EmissionColor", color);
				}
			}
			if (lowerOnHover) {
				transform.position += transform.up * 0.1f;
			}
		}

		private void OnMouseDown() {
			onClick?.Invoke();
			onClickRef?.Invoke(this);
		}

	}
}
