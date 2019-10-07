using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD45 {
	public class PauseManager : MonoBehaviour {
		
		public Material whiteMat;
		public Material blackMat;
		public Material whiteGlowMat;
		public Material blackGlowMat;
		public Color textColorWhite;
		public Color textColorBlack;

		public bool isPaused = false;

		private Transform buttonRoot;
		private Transform hangingUp;
		private Transform hangingDown;
		private Transform hangTarget;
		private PhysicalButton backToMenu;
		private bool isGoingBack = false;

		private void Awake() {
			buttonRoot = transform.Find("HangingButton");
			hangingUp = transform.Find("HangingUp");
			hangingDown = transform.Find("HangingDown");
			hangTarget = transform.Find("HangTarget");

			backToMenu = buttonRoot.Find("BackToMenu").GetComponent<PhysicalButton>();
			backToMenu.onClick += BackToMenuClicked;
		}

		private void Start() {
			Level.current.OnDimensionChange += OnDimensionChange;
		}

		private void OnDestroy() {
			if (isPaused) {
				Unpause();
			}
		}

		private void BackToMenuClicked() {
			if (isGoingBack) return;
			isGoingBack = true;
			TransitionManager.I.FadeToBlack(1f, BackToMenu, 0f, 1f, true);
		}

		private void BackToMenu() {
			SceneManager.LoadScene("Menu");
		}

		public void Trigger() {
			if (!isPaused) {
				Pause();
			} else {
				Unpause();
			}
		}

		private void Pause() {
			isPaused = true;
			Time.timeScale = 0.01f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			//TransitionManager.I.FadeToBlack(0.5f, null, 0f, 0.75f);
			hangTarget.position = hangingDown.position;
		}

		private void Unpause() {
			if (isGoingBack) return;
			isPaused = false;
			Time.timeScale = 1f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			//TransitionManager.I.FadeFromBlack(0.5f, null);
			hangTarget.position = hangingUp.position;
		}

		private void OnDimensionChange(Dimension nDim) {
			Material[] mats = new Material[2] { (nDim == Dimension.White) ? whiteMat : blackMat, (nDim == Dimension.White) ? whiteGlowMat : blackGlowMat };
			backToMenu.GetComponent<Renderer>().materials = mats;
			buttonRoot.Find("Text1").GetComponent<TextMesh>().color = (nDim == Dimension.White) ? textColorWhite : textColorBlack;
			buttonRoot.Find("Text2").GetComponent<TextMesh>().color = (nDim == Dimension.White) ? textColorWhite : textColorBlack;
		}

	}
}
