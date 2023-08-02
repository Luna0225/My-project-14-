using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float moveSpeed = 5f; //�ړ����x
    [SerializeField, Range(0, 10)] private float jumpPower = 3f;�@//�W�����v��
    private CharacterController characterController;  //CharacterController�̃L���b�V��

    private Transform cachedtransform; //transform�̃L���b�V��
    private Vector3 moveVelocity; //�L�����N�^�[�̈ړ����x�̏��

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //���ׂ������邽�߂ɃL���b�V�����Ă���
        cachedtransform = transform;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("�}�E�X���W:" + Input.mousePosition);
        //Debug.Log(characterController.isGrounded ? "�n��ɂ��܂�" : "�󒆂ɂ��܂�");
        //���͎��ɂ��ړ�����(�����𖳎����Ă���̂ł��т��ѓ���)
        //moveVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        //moveVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;

        //�J�����̑O�����̃x�N�g��
        Vector3 cameraForwardVector = Camera.main.transform.forward * Input.GetAxis("Vertical") * moveSpeed;
        //�J�����̉E�����̃x�N�g��
        Vector3 cameraRightVector = Camera.main.transform.right * Input.GetAxis("Horizontal") * moveSpeed;

        //�㉺�Ɉړ����Ȃ��悤��y�������O�ɂ���
        //cameraForwardVector.y = 0;
        //cameraRightVector.y = 0;

        //WS���͂̎��̓J�����̑O�����ɑ��x����������
        //moveVelocity = cameraForwardVector * Input.GetAxis("Vertical") * moveSpeed;
        moveVelocity.x = cameraForwardVector.x + cameraRightVector.x;
        //AD���͂̎��̓J�����̉������ɑ��x����������
        //moveVelocity += cameraRightVector * Input.GetAxis("Horizontal") * moveSpeed;
        moveVelocity.z = cameraForwardVector.z + cameraRightVector.z;
        this.animator.SetBool("isRun", Input.GetAxis("Vertical") != 0);
        this.animator.SetBool("isRun2", Input.GetAxis("Horizontal") != 0);
        //Debug.Log(moveVelocity.y);
        //Debug.Log(moveVelocity.z);
        //�ړ�����������
        cachedtransform.LookAt(transform.position + new Vector3(moveVelocity.x, 0, moveVelocity.z));
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                this.animator.SetBool("isJump", true);
                //�W�����v����
                Debug.Log("�W�����v�I");
                moveVelocity.y = jumpPower; //�W�����v�̍ۂ͏�����Ɉړ�������
            }
            else
            {
                this.animator.SetBool("isJump", false);
            }
        }
        else
        {
            //�d�͂ɂ�����
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
            //Debug.Log(Physics.gravity.y);
        }
        
        //�I�u�W�F�N�g�𓮂���
        characterController.Move(moveVelocity * Time.deltaTime);

       
    }
}