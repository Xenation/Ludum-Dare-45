using UnityEngine;

namespace LD45 {
	public class Hint : MonoBehaviour {

		public Hint next;
		public string trigInput = "";

		private void Awake() {
			if (next != null) {
				next.gameObject.SetActive(false);
			}
		}

		private void Update() {
			if (next == null) return;
			if (Input.GetButtonDown(trigInput)) {
				next.gameObject.SetActive(true);
			}
		}

	}
}
