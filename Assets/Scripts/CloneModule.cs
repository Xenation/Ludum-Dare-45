using UnityEngine;

namespace LD45 {
	public class CloneModule : CubeModule {

		private TheCube otherCube = null;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.LeftShift)) {
				GameObject otherGO = Instantiate(gameObject);
				transform.position += Vector3.up;
				otherCube = otherGO.GetComponent<TheCube>();
				otherCube.GetComponent<CloneModule>().otherCube = cube;
				otherCube.enabled = false;
			}

			if (otherCube != null && Input.GetKeyDown(KeyCode.Tab)) {
				FindObjectOfType<Follow>().toFollow = otherCube.transform;
				otherCube.enabled = true;
				cube.enabled = false;
			}
		}

	}
}
