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
    [SerializeField]
    private float currentCameraRotationX;
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
        Movemonet();
        CameraRotation();

    }   

    void Movemonet()
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
    }

    void CameraRotation()
    {        
        float mouseX = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        //Debug.Log("MouseX: " + mouseX + ", MouseY: " + mouseY);
        
        
        currentCameraRotationX -= mouseY;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

     
        rotationY += mouseX;
        rotationY = Mathf.Clamp(rotationY, -cameraRotationLimit, cameraRotationLimit);
        
        //transform.rotation = Quaternion.Euler(0f, rotationY, 0f); // 캐릭터 몸통 회전
        //localEulerAngles : 유니티에서 내부적으로 쿼터니언을 적용해줌
        //프로그래머는 오일러각만 신경써도 됨 (짐벌락 현상을 유니티내부에서 쿼터니언을 적용)
        //개꿀
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, rotationY, 0f); // 카메라 X축 회전
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
