using System.Collections.Generic;
using UnityEngine;

namespace LD45 {
	[RequireComponent(typeof(Rigidbody))]
	public class TheCube : MonoBehaviour {

		public static readonly Vector3[] corners = {
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, -0.5f)
		};

		public struct Face {
			public Vector3 center;
			public Vector3 normal;

			public Face(Vector3 normal) {
				this.center = normal * 0.5f;
				this.normal = normal;
			}
		}

		public static readonly Face[] faces = {
			new Face(Vector3.down),
			new Face(Vector3.up),
			new Face(Vector3.left),
			new Face(Vector3.right),
			new Face(Vector3.back),
			new Face(Vector3.forward)
		};

		public float revealRadius = 2f;
		public float killY = -5f;

		public Rigidbody rb;

		private List<CubeModule> modules = new List<CubeModule>();

		private void Start() {
			rb = GetComponent<Rigidbody>();
			Shader.SetGlobalFloat("_RevealRadius", revealRadius);
			Shader.SetGlobalFloat("_RevealFade", 1f);
			Shader.SetGlobalFloat("_DepthFadeStart", -2f);
			Shader.SetGlobalFloat("_DepthFadeEnd", -4f);
		}

		private void Update() {
			Shader.SetGlobalVector("_BGColor", Camera.main.backgroundColor);
			Shader.SetGlobalVector("_RevealCenter", transform.position);

			if (transform.position.y < killY) {
				Level.current.ReloadLevel();
			}
		}

		private void OnEnable() {
			foreach (CubeModule module in modules) {
				if (!module.enabled) {
					module.enabled = true;
				}
			}
		}

		private void OnDisable() {
			foreach (CubeModule module in modules) {
				if (module.enabled) {
					module.enabled = false;
				}
			}
		}

		private void OnDestroy() {
			Shader.SetGlobalFloat("_RevealRadius", 1000f);
		}

		public void RegisterModule(CubeModule module) {
			modules.Add(module);
		}

		public bool isOnGround() {
			for (int i = 0; i < 8; i++) {
				if (Physics.CheckBox(transform.TransformPoint(corners[i]), Vector3.one * 0.05f, transform.rotation, LayerMask.GetMask("Ground"))) {
					return true;
				}
			}
			return false;
		}

		public bool GroundContact(out Vector3 contact) {
			contact = Vector3.zero;
			uint contacts = 0;
			for (int i = 0; i < 8; i++) {
				Vector3 cornerPos = transform.TransformPoint(corners[i]);
				if (Physics.CheckBox(cornerPos, Vector3.one * 0.05f, transform.rotation, LayerMask.GetMask("Ground"))) {
					contact += cornerPos;
					contacts++;
				}
			}
			if (contacts != 0) {
				contact /= contacts;
				return true;
			}
			return false;
		}

		public Face GetLowestFace() {
			float lowestY = float.PositiveInfinity;
			Face lowestFace = new Face();
			foreach (Face face in faces) {
				Vector3 worldCenter = transform.TransformPoint(face.center);
				if (worldCenter.y < lowestY) {
					lowestY = worldCenter.y;
					lowestFace = face;
				}
			}
			return lowestFace;
		}

	}
}
