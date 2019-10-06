using UnityEngine;

namespace LD45 {
	[RequireComponent(typeof(TheCube))]
	public class CubeModule : MonoBehaviour {

		protected TheCube cube;

		private void Awake() {
			cube = GetComponent<TheCube>();
			cube.RegisterModule(this);
		}

		public virtual void OnKill() { }

	}
}
