using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaserBeamManager : MonoBehaviour
{
	public GameObject particlePrefab;
	public GameObject laserTarget;
	public GameObject UIbutton;
	public bool primed = false;
	public float cooldown = 1f;
	public float range = 3f;
	public float damage = 50;
	public bool active = false;
	
	TMP_Text btntext;

	private void Start() {
		btntext = UIbutton.GetComponentInChildren<TMP_Text>();
		active = true;
	}

	private Vector3? GetLookPos() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

		if (Physics.Raycast(ray, out hit)) {
			return hit.point;
		}

		return null;
	}

	private List<Enemy> FindEnemiesAtPos(Vector3 pos) {
		List<Enemy> output = new List<Enemy>();
		Collider[] hitColliders = Physics.OverlapSphere(pos, range, 1 << LayerMask.NameToLayer("Enemy"));

		foreach (var hitCollider in hitColliders) {
			GameObject obj = hitCollider.gameObject;

			output.Add(obj.GetComponent<Enemy>());
		}

		return output;
	}

	public void HandleButtonClick() {
		if (!active) return;

		if (!primed) {
			primed = true;
			btntext.text = "Fire orbital beam";
		} else {
			Vector3? lookPos = GetLookPos();

			if (lookPos == null) return;

			btntext.text = $"Prime orbital beam\nCooldown {cooldown}s";
			Fire(lookPos.Value);
		}
	}

	private void Fire(Vector3 pos) {
		primed = false;
		active = false;

		Instantiate(particlePrefab, pos, new Quaternion());

		foreach (var en in FindEnemiesAtPos(pos)) {
			en.Damage(damage);
		}

		// Button btn = UIbutton.GetComponent<Button>();
		// var colors = btn.colors;
        // colors.normalColor = Color.red;
        // btn.colors = colors;

		// UIbutton.GetComponent<Button>().colors.normalColor = new Color(100, 100, 100);
		StartCoroutine(Cooldown());
	}

	private IEnumerator Cooldown() {
		yield return new WaitForSeconds(cooldown);

		// UIbutton.GetComponent<Image>().color = new Color(255, 255, 255);
	}

	private IEnumerator Deselect() {
		yield return new WaitForSeconds(10f);

		if (primed) {
			primed = false;
			btntext.text = $"Prime orbital beam\nCooldown {cooldown}s";
		}
	}

	private void Update() {
		if (primed) {
			if (!laserTarget.activeSelf) {
				laserTarget.SetActive(true);
			}

			Vector3? lookPos = GetLookPos();

			if (lookPos == null) {
				if (laserTarget.activeSelf) {
					laserTarget.SetActive(false);
				}
			} else {
				laserTarget.transform.position = lookPos.Value;
			}
		} else if (laserTarget.activeSelf) {
			laserTarget.SetActive(false);
		}
	}
}
