using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerTest : MonoBehaviour {

	private LineRenderer pointer;

	private void Start () {
		pointer = this.GetComponent<LineRenderer>();
	}
}
