﻿using UnityEngine;

namespace LD45 {
	public class Follow : MonoBehaviour {

		public Transform toFollow;
		public float lerpFactor = .8f;

		private void Start() {
			transform.position = toFollow.position;
		}

		private void LateUpdate() {
			if (toFollow == null) return;
			transform.position = Vector3.Lerp(transform.position, toFollow.position, lerpFactor);
		}

	}
}
