using UnityEngine;
using UnityEngine.UI;
using Xenon;
using Xenon.Processes;

namespace LD45 {
	public class TransitionManager : AutoBehaviourSingleton<TransitionManager> {

		private ProcessManager pm;
		private Graphic fadeGraphic;

		protected override void Initialize() {
			pm = new ProcessManager();

			GameObject canvasObj = new GameObject("Canvas");
			canvasObj.transform.SetParent(transform, false);
			Canvas canvas = canvasObj.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvasObj.AddComponent<CanvasScaler>();
			GameObject fader = new GameObject("Fader");
			fader.transform.SetParent(canvasObj.transform, false);
			fader.AddComponent<CanvasRenderer>();
			fadeGraphic = fader.AddComponent<RawImage>();
			fadeGraphic.color = Color.black;
			RectTransform fadeRect = fader.GetComponent<RectTransform>();
			fadeRect.anchorMin = Vector2.zero;
			fadeRect.anchorMax = Vector2.one;
			fadeRect.offsetMin = Vector2.zero;
			fadeRect.offsetMax = Vector2.zero;

			DontDestroyOnLoad(gameObject);
		}

		private void Update() {
			pm.UpdateProcesses(Time.deltaTime);
		}

		public void FadeToBlack(float duration, Process.OnTerminateCallback endCallback, float delay = 0f) {
			FadeOutProcess fadeOut = new FadeOutProcess(duration, fadeGraphic);
			if (endCallback != null) {
				fadeOut.TerminateCallback += endCallback;
			}
			if (delay != 0f) {
				TimedProcess delayProcess = new TimedProcess(delay);
				delayProcess.Attach(fadeOut);
				pm.LaunchProcess(delayProcess);
			} else {
				pm.LaunchProcess(fadeOut);
			}
		}

		public void FadeFromBlack(float duration, Process.OnTerminateCallback endCallback, float delay = 0f) {
			FadeInProcess fadeIn = new FadeInProcess(duration, fadeGraphic);
			if (endCallback != null) {
				fadeIn.TerminateCallback += endCallback;
			}
			if (delay != 0f) {
				TimedProcess delayProcess = new TimedProcess(delay);
				delayProcess.Attach(fadeIn);
				pm.LaunchProcess(delayProcess);
			} else {
				pm.LaunchProcess(fadeIn);
			}
		}

	}
}
