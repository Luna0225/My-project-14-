using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float moveSpeed = 3f; //�ړ����x
    [SerializeField, Range(0, 10)] private float jumpPower = 3f;�@//�W�����v��
    private CharacterController characterController;  //CharacterController�̃L���b�V��

    private Transform cachedtransform; //transform�̃L���b�V��
    private Vector3 moveVelocity; //�L�����N�^�[�̈ړ����x�̏��
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //���ׂ������邽�߂ɃL���b�V�����Ă���
        cachedtransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("�}�E�X���W:" + Input.mousePosition);
        Debug.Log(characterController.isGrounded ? "�n��ɂ��܂�" : "�󒆂ɂ��܂�");
        //���͎��ɂ��ړ�����(�����𖳎����Ă���̂ł��т��ѓ���)
        moveVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;
        //�ړ�����������
        cachedtransform.LookAt(transform.position + new Vector3(moveVelocity.x, 0, moveVelocity.z));
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                //�W�����v����
                Debug.Log("�W�����v�I");
                moveVelocity.y = jumpPower; //�W�����v�̍ۂ͏�����Ɉړ�������
            }
        }
        else
        {
            //�d�͂ɂ�����
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        //�I�u�W�F�N�g�𓮂���
        characterController.Move(moveVelocity * Time.deltaTime);
    }
}