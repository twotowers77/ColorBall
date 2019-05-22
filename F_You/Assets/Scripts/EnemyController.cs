using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public enum CurrentState { idle, runAway, attack, dead }; //列挙型"状態"変数
    public CurrentState curState = CurrentState.idle;
    GameObject[] ExitPoints;
    Transform[] ExitPointTrans;
    float[] Dist;


    private Transform _transform;
    private Transform targetTransform;
    private NavMeshAgent nvAgent;

    public bool isDead = false;//死亡有無
    private int targetInt = 0;
    private int LifeCount = 0; 
    
    void Start()
    {
        ExitPoints = GameObject.FindGameObjectsWithTag("ExitPoint");
        ExitPointTrans = new Transform[ExitPoints.Length];
        Dist = new float[ExitPoints.Length];
        
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        for( int i = 0; i < ExitPoints.Length; i++)
        {
            ExitPointTrans[i] = ExitPoints[i].GetComponent<Transform>();
        }
        targetTransform = ExitPointTrans[0];
        StartCoroutine(SearchTarget());

        //アニメーション適用後に使用することそれぞれの状態をアニメーション設定する必要がある。
        //StartCoroutine(this.CheckState());
        //StartCoroutine(this.CheckStateForAction());
    }
    void FixedUpdate()
    {
        //追跡対象の位置を設定するとすぐ追跡スタート (変わる前）
       // nvAgent.destination = targetTransform.position;
    }

    IEnumerator SearchTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < ExitPoints.Length; i++)
            {
                Dist[i] = Mathf.Abs(Vector3.Distance(ExitPointTrans[i].position, transform.position));
            }
            targetTransform = ExitPointTrans[0];

            if (ExitPoints.Length == 1) { }
            else
            {
                for (int i = 0; i < ExitPoints.Length - 1; i++)
                {
                    if (Dist[targetInt] <= Dist[i + 1])
                    {
                        targetTransform = ExitPointTrans[targetInt];
                    }
                    else
                    {
                        targetInt = i + 1;
                        targetTransform = ExitPointTrans[targetInt];
                    }
                }
            }
            nvAgent.destination = targetTransform.position;
            targetInt = 0;
        }
    }
    /*
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.5f);

            //Vector3.Distance(Vector3 a, Vector3 b);
            //a, bの間の距離を測定して返す関数で私たちが作成したコードと
            //プレーヤの現在の位置とモンスターの現在の位置は同じ位置を持っている
            float dist = Vector3.Distance(targetTransform.position, _transform.position);


            if (dist <= traceDist)
            {
                curState = CurrentState.runAway;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
    }
    /*IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    nvAgent.Stop(); //NavMeshAgent Component Stop 逃亡中止
                    break;
                case CurrentState.runAway:
                    nvAgent.destination = targetTransform.position;
                    nvAgent.Resume(); //NavMeshAgent Component Resume 初位置に戻る.
                    break;
                case CurrentState.attack:
                    break;
            }
            yield return null;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "ColorBallPrefabs")
        {
            LifeCount++;
        }
        if (LifeCount == 3 || col.gameObject.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
            LifeCount = 0;
        }
    }
}
