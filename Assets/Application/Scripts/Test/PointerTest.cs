using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PointerTest : MonoBehaviour {

	private LineRenderer pointer;
	private Point selectPoint;
	public Camera head;

	private bool isMove = false;
	// use standerd assets.
	private Blur blur;

	private void Start () {
		blur = head.GetComponent<Blur> ();
		pointer = this.GetComponent<LineRenderer>();
		pointer.numPositions = 2;
	}

	private void Update () {
		pointer.SetPosition (0, this.transform.position);
		print (selectPoint);
		RaycastHit hit;
		if (Physics.Raycast (this.transform.position, this.transform.forward * 10f, out hit, 100)) {
			if (hit.collider.tag == "Point") {
				pointer.SetPosition (1, hit.point);
				EnablePointer ();
				SetPoint (hit.collider.GetComponent<Point> ());
			} else {
				DisEnablePointer ();
				initPoint ();
			}
		} else {
			DisEnablePointer ();
			initPoint ();
		}

		if (Input.GetMouseButtonUp (0) && selectPoint != null) {
			StartCoroutine (MoveAction (hit.point));
		}

		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");

		head.gameObject.transform.Rotate (dx, dy, 0);

		if (isMove) {
			head.fieldOfView -= 1;
		} else {
			head.fieldOfView = 60f;
		}
	}

	private void EnablePointer () {
		pointer.enabled = true;
	}

	private void DisEnablePointer () {
		pointer.enabled = false;
	}

	private void SetPoint (Point p) {
		this.selectPoint = p;
	}

	private void initPoint() {
		this.selectPoint = null;
	}

	public IEnumerator MoveAction (Vector3 v) {
		isMove = true;
		blur.enabled = true;
		yield return new WaitForSeconds (0.5f);
		head.gameObject.transform.position = v;
		isMove = false;
		blur.enabled = false;
	}
}
