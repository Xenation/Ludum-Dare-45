using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD45 {
	public class Level : MonoBehaviour {

		public static Level current;

		public float killY = -5f;
		public string nextLevel = "";
		public Dimension activeDimension = Dimension.White;

		private List<TheCube> activeCubes = new List<TheCube>();

		private void Awake() {
			current = this;
		}

		private void Start() {
			Shader.SetGlobalFloat("_DepthFadeStart", -1f);
			Shader.SetGlobalFloat("_DepthFadeEnd", -5f);
			Shader.SetGlobalVector("_BGColor", activeDimension.GetBGColor());
			Camera.main.backgroundColor = activeDimension.GetBGColor();
		}

		private void Update() {
			//Shader.SetGlobalVector("_BGColor", Camera.main.backgroundColor);
			if (Input.GetKeyDown(KeyCode.R)) {
				ReloadLevel();
				return;
			}

			if (activeCubes.Count == 0) {
				ReloadLevel();
				return;
			}

			foreach (TheCube cube in activeCubes) {
				if (cube.transform.position.y < killY) {
					Destroy(cube.gameObject);
				}
			}
		}

		private void OnDestroy() {
			Shader.SetGlobalFloat("_RevealRadius", 1000f);
		}

		public void RegisterCube(TheCube cube) {
			activeCubes.Add(cube);
		}

		public void UnregisterCube(TheCube cube) {
			activeCubes.Remove(cube);
		}

		public void ReloadLevel() {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void NextLevel() {
			SceneManager.LoadScene(nextLevel);
		}

		public void SetDimension(Dimension dim) {
			activeDimension = dim;
			Camera.main.cullingMask = ~LayerMask.GetMask("Ground" + activeDimension.Other());
			Shader.SetGlobalVector("_BGColor", activeDimension.GetBGColor());
			Camera.main.backgroundColor = activeDimension.GetBGColor();
		}

	}
}
