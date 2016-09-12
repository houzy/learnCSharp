using UnityEngine;
using System;
using System.Collections;

public class Subscriber {

	public void Receiver (object sender, EventArgs e) {
		Debug.Log ("sender == " + sender.ToString());
	}
}
