using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Xenon;
using Xenon.Processes;

namespace LD45 {
	public class Level : MonoBehaviour {

		public static Level current;

		public bool hasUpgraded = false;
		public float killY = -5f;
		public string nextLevel = "";
		public Dimension activeDimension = Dimension.White;
		
		private List<TheCube> activeCubes = new List<TheCube>();
		private bool reloadQueued = false;

		private void Awake() {
			current = this;
		}

		private void Start() {
			TransitionManager.I.FadeFromBlack(1f, StartFadeEnded, 0.5f);

			Shader.SetGlobalFloat("_DepthFadeStart", -1f);
			Shader.SetGlobalFloat("_DepthFadeEnd", -5f);
			Shader.SetGlobalVector("_BGColor", activeDimension.GetBGColor());
			Camera.main.backgroundColor = activeDimension.GetBGColor();
		}

		private void StartFadeEnded() {
			if (hasUpgraded && activeCubes.Count > 0) {
				EffectManager.I.PlayEffect(Effect.Upgrade, activeCubes[0].transform);
			}
		}

		private void Update() {
			if (!reloadQueued) {
				if (Input.GetKeyDown(KeyCode.R)) {
					reloadQueued = true;
					TransitionManager.I.FadeToBlack(1f, ReloadFadeEnded);
					return;
				}
				if (activeCubes.Count == 0) {
					reloadQueued = true;
					TransitionManager.I.FadeToBlack(1f, ReloadFadeEnded);
					return;
				}
			}

			foreach (TheCube cube in activeCubes) {
				if (cube.transform.position.y < killY) {
					Destroy(cube.gameObject);
					cube.Kill();
				}
			}
		}

		private void ReloadFadeEnded() {
			ReloadLevel();
		}

		public void TriggerNextLevel() {
			reloadQueued = true;
			TransitionManager.I.FadeToBlack(1f, NextLevel);
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
