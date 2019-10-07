using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD45 {
	public class LevelSelectionManager : Singleton<LevelSelectionManager> {

		public Material whiteMat;
		public Material blackMat;
		public Material whiteGlowMat;
		public Material blackGlowMat;
		public Color textColorWhite;
		public Color textColorBlack;

		public Transform cameraTarget;

		private PhysicalButton[] levelBtns;
		private PhysicalButton backBtn;
		private GameObject titleObj;
		private Dimension dim = Dimension.White;
		private string levelToLoad = "";

		private void Awake() {
			levelBtns = new PhysicalButton[10]; // unsafe
			foreach (Transform child in transform) {
				if (!child.gameObject.name.StartsWith("Level")) continue;
				PhysicalButton btn = child.GetComponent<PhysicalButton>();
				levelBtns[int.Parse(child.gameObject.name.Substring(5)) - 1] = btn;
				btn.onClickRef += ButtonClicked;
			}
			backBtn = transform.Find("Back").GetComponent<PhysicalButton>();
			backBtn.onClick += BackClicked;

			titleObj = transform.Find("SelectionTitle").Find("default").gameObject;
		}

		private void Update() {
			if (Input.GetButtonDown("Switch")) {
				dim = dim.Other();
				Material[] mats = new Material[2] { (dim == Dimension.White) ? whiteMat : blackMat, (dim == Dimension.White) ? whiteGlowMat : blackGlowMat };
				for (int i = 0; i < levelBtns.Length; i++) {
					levelBtns[i].GetComponent<Renderer>().material = (dim == Dimension.White) ? whiteMat : blackMat;
					levelBtns[i].transform.Find("Text").GetComponent<TextMesh>().color = (dim == Dimension.White) ? textColorWhite : textColorBlack;
				}
				backBtn.GetComponent<Renderer>().materials = mats;
				titleObj.GetComponent<Renderer>().material = (dim == Dimension.White) ? whiteMat : blackMat;
				Camera.main.cullingMask = ~LayerMask.GetMask("Ground" + dim.Other());
				Shader.SetGlobalVector("_BGColor", dim.GetBGColor());
				Camera.main.backgroundColor = dim.GetBGColor();
			}
		}

		private void ButtonClicked(PhysicalButton btn) {
			if (levelToLoad != "") return;
			TransitionManager.I.FadeToBlack(1f, FadeEnded);
			levelToLoad = btn.gameObject.name;
		}

		private void FadeEnded() {
			SceneManager.LoadScene(levelToLoad);
		}

		private void BackClicked() {
			cameraTarget.position = Vector3.zero;
		}

	}
}
