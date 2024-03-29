﻿using UnityEngine;

namespace LD45 {
	public class CloneModule : CubeModule {

		public Material whiteMat;
		public Material blackMat;

		[System.NonSerialized]
		public CloneModule other = null;

		private void Update() {
			if (other == null && Input.GetButtonDown("Clone")) {
				SpawnClone();
			}

			if (other != null && Input.GetButtonDown("Switch")) {
				Switch();
			}
		}

		public override void OnKill() {
			if (other == null) return;
			other.other = null;
			if (!other.enabled) {
				Switch();
			}
		}

		public void SpawnClone() {
			GameObject otherGO = Instantiate(gameObject);
			transform.position += Vector3.up;
			otherGO.layer = LayerMask.NameToLayer("Player" + cube.dimension.Other());
			otherGO.transform.SetParent(transform.parent);
			other = otherGO.GetComponent<CloneModule>();
			other.other = this;
			other.cube.dimension = cube.dimension.Other();
			otherGO.GetComponent<Renderer>().sharedMaterial = (other.cube.dimension == Dimension.White) ? whiteMat : blackMat;
			other.cube.Disable();
		}

		public void Switch() {
			transform.parent.Find("CameraFollow").GetComponent<Follow>().toFollow = other.transform;
			other.cube.Enable();
			cube.Disable();
			Level.current.SetDimension(other.cube.dimension);
		}

	}
}
