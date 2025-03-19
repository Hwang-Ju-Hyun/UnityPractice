using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
private
    Rigidbody rigid;
    MeshRenderer m_mesh;
    Material mat;
public
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        m_mesh = GetComponent<MeshRenderer>();
        mat = m_mesh.material;
    }

    // Update is called once per frame
    void Update()
    {        
    }

    void OnCollisionEnter(Collision _player)
    {
        if(_player.gameObject.name=="Player")
        {
            mat.color = new Color(0, 0, 0);
        }
    }
}
