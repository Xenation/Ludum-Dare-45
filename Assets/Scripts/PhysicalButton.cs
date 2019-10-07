using UnityEngine;

namespace LD45 {
	public class PhysicalButton : MonoBehaviour {

		public delegate void ClickedHandler();
		public event ClickedHandler onClick;
		public delegate void ClickedHandlerRef(PhysicalButton btn);
		public event ClickedHandlerRef onClickRef;

		private void OnMouseEnter() {
			Renderer renderer = GetComponent<Renderer>();
			if (renderer.materials.Length > 1) {
				Material mat = mat = renderer.materials[1];
				Color color = mat.GetColor("_Emis");
				color *= 1.5f;
				mat.SetColor("_Emis", color);
			}
			transform.position += Vector3.down * 0.1f;
		}

		private void OnMouseExit() {
			Renderer renderer = GetComponent<Renderer>();
			if (renderer.materials.Length > 1) {
				Material mat = mat = renderer.materials[1];
				Color color = mat.GetColor("_Emis");
				color /= 1.5f;
				mat.SetColor("_Emis", color);
			}
			transform.position += Vector3.up * 0.1f;
		}

		private void OnMouseDown() {
			onClick?.Invoke();
			onClickRef?.Invoke(this);
		}

	}
}
