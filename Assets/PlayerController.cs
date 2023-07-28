using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float moveSpeed = 3f; //移動速度
    [SerializeField, Range(0, 10)] private float jumpPower = 3f;　//ジャンプ力
    private CharacterController characterController;  //CharacterControllerのキャッシュ

    private Transform cachedtransform; //transformのキャッシュ
    private Vector3 moveVelocity; //キャラクターの移動速度の情報
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //負荷を下げるためにキャッシュしておく
        cachedtransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("マウス座標:" + Input.mousePosition);
        Debug.Log(characterController.isGrounded ? "地上にいます" : "空中にいます");
        //入力軸による移動処理(完成を無視しているのできびきび動く)
        //moveVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        //moveVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;

        //カメラの前方向のベクトル
        Vector3 cameraForwardVector = Camera.main.transform.forward;
        //カメラの右方向のベクトル
        Vector3 cameraRightVector = Camera.main.transform.right;

        //上下に移動しないようにy成分を０にする
        cameraForwardVector.y = 0;
        cameraRightVector.y = 0;

        //WS入力の時はカメラの前方向に速度を持たせる
        moveVelocity = cameraForwardVector * Input.GetAxis("Vertical") * moveSpeed;
        //AD入力の時はカメラの横方向に速度を持たせる
        moveVelocity += cameraRightVector * Input.GetAxis("Horizontal") * moveSpeed;
        //Debug.Log(moveVelocity);
        //移動方向を向く
        cachedtransform.LookAt(transform.position + new Vector3(moveVelocity.x, 0, moveVelocity.z));
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                //ジャンプ処理
                Debug.Log("ジャンプ！");
                moveVelocity.y = jumpPower; //ジャンプの際は上方向に移動させる
            }
        }
        else
        {
            //重力による加速
            //moveVelocity.y += Physics.gravity.y * Time.deltaTime;
            //Debug.Log(Physics.gravity.y);
        }
        //オブジェクトを動かす
        characterController.Move(moveVelocity * Time.deltaTime);
    }
}