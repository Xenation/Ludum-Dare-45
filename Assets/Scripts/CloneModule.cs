using UnityEngine;

namespace LD45 {
	public class CloneModule : CubeModule {

		public Material whiteMat;
		public Material blackMat;

		[System.NonSerialized]
		public CloneModule other = null;

		private void Update() {
			if (other == null && Input.GetKeyDown(KeyCode.LeftShift)) {
				SpawnClone();
			}

			if (other != null && Input.GetKeyDown(KeyCode.Tab)) {
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
			other = otherGO.GetComponent<CloneModule>();
			other.other = this;
			other.cube.dimension = cube.dimension.Other();
			otherGO.GetComponent<Renderer>().sharedMaterial = (other.cube.dimension == Dimension.White) ? whiteMat : blackMat;
			other.cube.Disable();
		}

		public void Switch() {
			FindObjectOfType<Follow>().toFollow = other.transform;
			other.cube.Enable();
			cube.Disable();
			Level.current.SetDimension(other.cube.dimension);
		}

	}
}
