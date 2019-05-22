using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_A : MonoBehaviour
{
    protected int ballCount = 0;
    public GameObject enemy_B;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ColorBallPrefabs")
        {
            ballCount++;
			if (ballCount == 1)
			{
				Change();
			}
        }
    }
	protected virtual void Change()
	{

		Destroy(this.gameObject);
		Vector3 enV = this.gameObject.transform.position;
		Instantiate(this.enemy_B, enV, Quaternion.identity);
	}
    
}
