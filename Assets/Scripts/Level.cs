using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD45 {
	public class Level : MonoBehaviour {

		public static Level current;

		public string nextLevel = "";

		private void Awake() {
			current = this;
		}

		public void ReloadLevel() {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void NextLevel() {
			SceneManager.LoadScene(nextLevel);
		}

	}
}
