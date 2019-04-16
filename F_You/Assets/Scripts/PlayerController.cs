using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;
    private float _horizontal = 0.0f; //初期化
    private float _vertical = 0.0f;　//初期化

    [SerializeField] private MouseRotate m_MouseRotate;//MouseRotate.csファイルを見る
    [SerializeField] private Camera m_Camera;
    [SerializeField] private BtnController BC;
    Rigidbody rigdbody;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float walkSpeed = 10.0f;
    [SerializeField] public float runSpeed = 30.0f;

    public float rotateSpeed = 100.0f;
    public float range = 100.0f;
    public float jumpForce = 60f;
    private int jumpCount;

    //状態変数
    private bool isGround;
    private bool isRun;

    public GameObject ColorBallPrefabs;
    public GameObject player;
    public GameObject AimPoint;

    void Start()
    {
        _transform = GetComponent<Transform>(); //コンポーネントを変数に割当
        rigdbody = GetComponent<Rigidbody>();
        jumpCount = 1;　//JumpMax
        isGround = true; //地面にいる時
        isRun = false; //Run
        _animator = GetComponentInChildren<Animator>(); // Animatorコンポーネントはplayer objectの下位に存在するため、InChildren使用
        player = GameObject.FindGameObjectWithTag("Player");
        AimPoint = GameObject.FindGameObjectWithTag("AimPoint");
        m_Camera = Camera.main;//このCameraをMainCameraにする
        m_MouseRotate.Init(transform, m_Camera.transform);//MouseRotate.cs参照
        BtnController BC = GameObject.Find("Main Camera").GetComponent<BtnController>();
        //別シーン、別スクリプトの中にあるオブジェクトや変数などを使えるようになる。
        BC.paused = false;
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        if (BC.paused == false)
        {
            
            //AimPointのforwardの方向にレーザーを発射する
            Debug.DrawRay(AimPoint.transform.position, AimPoint.transform.forward * range, Color.red);
            _horizontal = Input.GetAxis("Horizontal"); //Input Managerに指定された値を受け入れる
            _vertical = Input.GetAxis("Vertical"); //変位の値を計算するためのVector3変数、前・後と左・右移動値を保存
            Vector3 moveDirect = (Vector3.forward * _vertical) + (Vector3.right * _horizontal);
            //ゲームオブジェクトの移動処理を楽にする関数
            _transform.Translate(moveDirect.normalized * Time.deltaTime * moveSpeed, Space.Self);
            //キャラクターの回転関数
            _transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed * Input.GetAxis("Mouse X"));

            if (_vertical >= 0.1f) //アニメイシヨン実装
            {
                _animator.SetBool("isWalk", true);
            }
            else if (_vertical <= -0.1f)
            {
                _animator.SetBool("isWalk", true);
            }
            else if (_horizontal >= 0.1f)
            {
                _animator.SetBool("isWalk", true);
            }
            else if (_horizontal <= -0.1f)
            {
                _animator.SetBool("isWalk", true);
            }
            else
            {
                _animator.SetBool("isWalk", false);
            }
            if (Input.GetMouseButtonDown(0))
            {
                ColorBallShoot();
            }
            if (Input.GetMouseButton(1))
            {
                ColorBallShoot();
            }
        }
        if (BC.paused == false)
        {
            m_MouseRotate.UpdateCursorLock();
            if (isGround)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    isRun = !isRun;
                    Run();
                }   
                jumpCount = 1;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (jumpCount == 1)
                    {
                        rigdbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange); // 점프
                        isGround = false;
                        jumpCount = 0;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        RotateView();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (BC.paused == false)
        {
            if (col.gameObject.tag == "Ground")
            {
                isGround = true;
                jumpCount = 1;
            }
        }
    }

    private void ColorBallShoot()
    {
        //Instantiate( prefab object, 生成位置、生成回転方向）
        GameObject instantItem = (GameObject)Instantiate(ColorBallPrefabs, AimPoint.transform.position, AimPoint.transform.rotation);

        Rigidbody rigidbody = instantItem.GetComponent<Rigidbody>();
        rigidbody.AddForce(AimPoint.transform.forward * 100f, ForceMode.Impulse);
        /*if (Physics.Raycast(AimPoint.transform.position, AimPoint.transform.forward, out hit, range))
        {
            GameObject CBE = (GameObject)Instantiate(ColorBallPrefabs, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(CBE, 3.0f);
        }*/
    }

    private void Run()
    {
        if (isRun == false)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void RotateView()
    {
        //MouseRotate.cs参照
        m_MouseRotate.LookRotation(transform, m_Camera.transform);
    }
}