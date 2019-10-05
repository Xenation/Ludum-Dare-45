using UnityEngine;

namespace LD45 {
	public class LevelEnd : MonoBehaviour {

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

			Level.current.NextLevel();
		}

	}
}
