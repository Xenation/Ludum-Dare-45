﻿using System.Collections.Generic;
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
			titleObj = transform.Find("Title").Find("default").gameObject;

			TransitionManager.I.FadeFromBlack(1.5f, null, .5f);
		}

		private void Update() {
			if (Input.GetButtonDown("Switch")) {
				dim = dim.Other();
				Material[] mats = new Material[2] { (dim == Dimension.White) ? whiteMat : blackMat, (dim == Dimension.White) ? whiteGlowMat : blackGlowMat };
				playBtn.GetComponent<Renderer>().materials = mats;
				selectBtn.GetComponent<Renderer>().materials = mats;
				quitBtn.GetComponent<Renderer>().materials = mats;
				titleObj.GetComponent<Renderer>().material = (dim == Dimension.White) ? whiteMat : blackMat;
				Camera.main.cullingMask = ~LayerMask.GetMask("Ground" + dim.Other());
				Shader.SetGlobalVector("_BGColor", dim.GetBGColor());
				Camera.main.backgroundColor = dim.GetBGColor();
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

	}
}
