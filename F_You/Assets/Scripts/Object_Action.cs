using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Action : MonoBehaviour {

    public float speed = 60f;
    public float DestroyTime = 2.0f;
    private Transform _transform;
    GameObject player;
    Rigidbody rigdbody;
    Vector3 forceDirection;

    ////////////////////[Override function]/////////////////////////
    void Awake()
    {
        _transform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigdbody = GetComponent<Rigidbody>();
        Destroy(gameObject, DestroyTime);
    }
    void Update()
    {
        if (_transform.transform.position.z <= -150f)
            Destroy(gameObject, 0.0f);
    }
    void OnTriggerStay(Collider other) //衝突体が重なっているときに発生する
    {   //Occurs when the collider continues to overlap.
        if (other.gameObject == player) {
            forceDirection = transform.position; // playerの x,y,z値を forceDirectionに入れる。

            forceDirection.x = player.transform.position.x > forceDirection.x ? -3f : 3f;// 基本形式   (条件) ? (条件がTrueの時実行) : (条件がfalseのとき実行) 区分は？と：を使う
            forceDirection.y = 0;
            forceDirection.z = player.transform.position.z > forceDirection.z ? -3f : 3f;


            rigdbody.AddForce(forceDirection * speed, ForceMode.Impulse);
        }
    }
}
