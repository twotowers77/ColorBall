using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : Enemy_A
{
	public Material[] _material;

	protected override void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "ColorBallPrefabs")
		{
			ballCount++;
			if (ballCount == 1)
			{
				this.GetComponent<Renderer>().material = _material[0];
			}
			if (ballCount == 2)
			{
				this.GetComponent<Renderer>().material = _material[1];
			}

			if (ballCount == 3)
			{
				ballCount = 0;
			}
			Debug.Log(ballCount);
		}
	}
}
