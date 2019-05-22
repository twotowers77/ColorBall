using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_B : Enemy_A
{
    GameObject enemy_A;
   // public Material[] _material;
    
	protected override void OnCollisionEnter(Collision collision)
	{		
		if (collision.gameObject.tag == "ColorBallPrefabs")
		{
			ballCount++;
			//    if (ballCount == 1)
			//    {
			//        this.GetComponent<Renderer>().material = _material[0];
			//    }
			//    if (ballCount == 2)
			//    {
			//        this.GetComponent<Renderer>().material = _material[1];
			//    }

			if (ballCount == 3)
			{
				Destroy(this.gameObject);
				ballCount = 0;
			}
			
		}
	}
	void OnCollisionStay(Collision collision)
	{
		Debug.Log("Collision Stay: " + collision.gameObject.tag+"\n"+
			       "collision : "+this.gameObject.tag);
	}
}
