using UnityEngine;

namespace LD45 {
	public class PhysicalButton : MonoBehaviour {

		public delegate void ClickedHandler();
		public event ClickedHandler onClick;

		private void OnMouseEnter() {
			Material mat = mat = GetComponent<Renderer>().materials[1];
			Color color = mat.GetColor("_Emis");
			color *= 1.5f;
			mat.SetColor("_Emis", color);
			transform.position += Vector3.down * 0.1f;
		}

		private void OnMouseExit() {
			Material mat = mat = GetComponent<Renderer>().materials[1];
			Color color = mat.GetColor("_Emis");
			color /= 1.5f;
			mat.SetColor("_Emis", color);
			transform.position += Vector3.up * 0.1f;
		}

		private void OnMouseDown() {
			onClick?.Invoke();
		}

	}
}
