using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD45 {
	public class MenuManager : Singleton<MenuManager> {

		public string firstLevel = "Level0";

		public Material whiteMat;
		public Material blackMat;
		public Material whiteGlowMat;
		public Material blackGlowMat;

		public Transform cameraTarget;
		public Transform levelSelectRoot;

		private PhysicalButton playBtn;
		private PhysicalButton selectBtn;
		private PhysicalButton quitBtn;
		private PhysicalButton qwertyBtn;
		private PhysicalButton azertyBtn;
		private GameObject titleObj;
		private Dimension dim = Dimension.White;
		private bool clicked = false;

		private void Awake() {
			Shader.SetGlobalFloat("_DepthFadeStart", -1f);
			Shader.SetGlobalFloat("_DepthFadeEnd", -5f);
			Shader.SetGlobalVector("_BGColor", Color.black);
			Shader.SetGlobalFloat("_RevealRadius", 1000f);
			Camera.main.backgroundColor = Color.black;

			playBtn = transform.Find("Play").GetComponent<PhysicalButton>();
			playBtn.onClick += PlayClicked;
			selectBtn = transform.Find("LevelSelect").GetComponent<PhysicalButton>();
			selectBtn.onClick += SelectClicked;
			quitBtn = transform.Find("Quit").GetComponent<PhysicalButton>();
			quitBtn.onClick += QuitClicked;
			qwertyBtn = transform.Find("QWERTY").GetComponent<PhysicalButton>();
			qwertyBtn.onClick += QwertyClicked;
			azertyBtn = transform.Find("AZERTY").GetComponent<PhysicalButton>();
			azertyBtn.onClick += AzertyClicked;
			titleObj = transform.Find("Title").Find("default").gameObject;

			TransitionManager.I.FadeFromBlack(1.5f, null, .5f);

			if (InputManager.mode == InputManager.Mode.ForceAZERTY) {
				azertyBtn.transform.position -= Vector3.up * 0.25f;
			} else if (InputManager.mode == InputManager.Mode.ForceQWERTY) {
				qwertyBtn.transform.position -= Vector3.up * 0.25f;
			}
		}

		private void Update() {
			if (Input.GetButtonDown("Switch")) {
				dim = dim.Other();
				Material[] mats = new Material[2] { (dim == Dimension.White) ? whiteMat : blackMat, (dim == Dimension.White) ? whiteGlowMat : blackGlowMat };
				playBtn.GetComponent<Renderer>().materials = mats;
				selectBtn.GetComponent<Renderer>().materials = mats;
				quitBtn.GetComponent<Renderer>().materials = mats;
				qwertyBtn.GetComponent<Renderer>().materials = mats;
				SetSubTextsColor(qwertyBtn.transform, (dim == Dimension.White) ? Color.white : Color.black);
				azertyBtn.GetComponent<Renderer>().materials = mats;
				SetSubTextsColor(azertyBtn.transform, (dim == Dimension.White) ? Color.white : Color.black);
				titleObj.GetComponent<Renderer>().material = (dim == Dimension.White) ? whiteMat : blackMat;
				Camera.main.cullingMask = ~LayerMask.GetMask("Ground" + dim.Other());
				Shader.SetGlobalVector("_BGColor", dim.GetBGColor());
				Camera.main.backgroundColor = dim.GetBGColor();
			}
		}

		private void SetSubTextsColor(Transform root, Color color) {
			foreach (Transform child in root) {
				TextMesh text = child.GetComponent<TextMesh>();
				if (text == null) continue;
				text.color = color;
			}
		}

		private void PlayClicked() {
			if (clicked) return;
			clicked = true;
			TransitionManager.I.FadeToBlack(1f, Play);
		}

		private void Play() {
			SceneManager.LoadScene(firstLevel);
		}

		private void SelectClicked() {
			cameraTarget.position = levelSelectRoot.position;
		}

		private void QuitClicked() {
			if (clicked) return;
			clicked = true;
			TransitionManager.I.FadeToBlack(1f, Quit);
		}

		private void Quit() {
			Application.Quit();
		}

		private void QwertyClicked() {
			if (InputManager.mode == InputManager.Mode.ForceQWERTY) return;
			qwertyBtn.transform.position -= Vector3.up * 0.25f;
			if (InputManager.mode == InputManager.Mode.ForceAZERTY) {
				azertyBtn.transform.position += Vector3.up * 0.25f;
			}
			InputManager.mode = InputManager.Mode.ForceQWERTY;
		}

		private void AzertyClicked() {
			if (InputManager.mode == InputManager.Mode.ForceAZERTY) return;
			azertyBtn.transform.position -= Vector3.up * 0.25f;
			if (InputManager.mode == InputManager.Mode.ForceQWERTY) {
				qwertyBtn.transform.position += Vector3.up * 0.25f;
			}
			InputManager.mode = InputManager.Mode.ForceAZERTY;
		}

	}
}
