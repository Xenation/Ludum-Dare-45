using UnityEngine;

namespace LD45 {
	public class Destructible : MonoBehaviour {

		public float maxHealth = 10f;
		public float regenTime = 2f;
		public Color healledColor = Color.yellow;
		public Color deadColor = Color.red;

		public float health;
		private Material mat;

		private void Start() {
			mat = GetComponent<Renderer>().material;
			health = maxHealth;
		}

		private void Update() {
			if (health < 0f) {
				Destroy(gameObject);
				return;
			}

			health += (maxHealth / regenTime) * Time.deltaTime;

			if (health > maxHealth) {
				health = maxHealth;
			}

			mat.SetColor("_Color", Color.Lerp(deadColor, healledColor, health / maxHealth));
		}

		private void OnCollisionEnter(Collision collision) {
			//Debug.Log("COL: " + collision.impulse.magnitude);
			health -= collision.impulse.magnitude;
		}

	}
}
