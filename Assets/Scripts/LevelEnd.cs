using UnityEngine;

namespace LD45 {
	public class LevelEnd : MonoBehaviour {

		private void OnTriggerEnter(Collider other) {
			if (((1 << other.gameObject.layer) & LayerMask.GetMask("PlayerWhite", "PlayerBlack")) == 0) return;
			
			Level.current.NextLevel();
		}

	}
}
