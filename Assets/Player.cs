using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Actor : MonoBehaviour
{

    public float lookSensitivity = 5f;
    [SerializeField]
    private float cameraRotationLimit = 80f;
    private float currentCameraRotationX = 0f;
    private float rotationY = 0f;

    [SerializeField]
    private Camera theCamera;    

    void Awake()
    {

    }
    void OnEnable()
    {        
    }    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();        
    }

    public Rigidbody rigid;
    public float fJumpForce = 10f;
    public float moveSpeed = 13f;

    void Update()
    {
        float fMoveDirX = Input.GetAxisRaw("Horizontal");
        float fMoveDirY = Input.GetAxisRaw("Vertical");

        Vector3 vMoveHorizontal = Vector3.right * fMoveDirX;
        Vector3 vMoveVertical = Vector3.forward * fMoveDirY;

        Vector3 vVelocity = (vMoveHorizontal + vMoveVertical).normalized * moveSpeed;
        rigid.velocity = new Vector3(vVelocity.x, rigid.velocity.y, vVelocity.z);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.velocity = new Vector3(rigid.velocity.x, fJumpForce, rigid.velocity.z);
            //Debug.Log("velocity : " + rigid.velocity);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log("velocity : " + rigid.velocity);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 25f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 13f;
        }
        CameraRotation();

    }   
    void CameraRotation()
    {
        // ���콺 �Է�
        float mouseX = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        Debug.Log("MouseX: " + mouseX + ", MouseY: " + mouseY);

        // �¿� ȸ�� (Y��)
        rotationY += mouseX;

        // ���Ʒ� ȸ�� (X��, ���� ����)
        currentCameraRotationX -= mouseY;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        // �÷��̾�(�Ǵ� ī�޶�) ȸ�� ����
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f); // ĳ���� ���� ȸ��
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f); // ī�޶� X�� ȸ��
    }

    void FixedUpdate()
    {

    }

    void LateUpdate()
    {
   
    }

    void OnDisable()
    {
        
    }

    void OnDestroy()
    {

    }
}
