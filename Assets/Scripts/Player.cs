using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 7;
    public float reticleRadius = 5;
    
    public float threshold = 0.1f;

    public float inputClampAmount = 0.5f;
    [SerializeReference] GameObject reticle;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject/
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }

    void Move() {
        //Define the speed at which the object moves.
        float horizontalMove = Input.GetAxis("Move Horizontal");
        float verticalMove = Input.GetAxis("Move Vertical");

        //Move the object to XYZ coordinates defined as horizontalInput, 0, and verticalInput respectively.
        transform.Translate(new Vector3(horizontalMove, verticalMove, 0) * moveSpeed * Time.deltaTime);
    }

    void Look() {
   
        // reticle = transform.Find("reticle").gameObject;
        //https://answers.unity.com/questions/1350081/xbox-one-controller-mapping-solved.html
        // float horizontalLook = Input.GetAxis("Look Horizontal");
        // float verticalLook = Input.GetAxis("Look Vertical");
        float horizontalLook = Mathf.Lerp(-inputClampAmount, inputClampAmount, Input.GetAxis("Look Horizontal"));
        float verticalLook = Mathf.Lerp( -inputClampAmount, inputClampAmount, Input.GetAxis("Look Vertical"));
        
        Debug.Log($"Raw: {new Vector2(horizontalLook, verticalLook)}");
        if (Mathf.Abs(horizontalLook) > threshold || Mathf.Abs(verticalLook) > threshold){
            Vector3 dir = (Vector2.right * horizontalLook) + (Vector2.up * verticalLook);
            Debug.Log(dir);
            Debug.DrawLine(transform.position, transform.position + (dir * reticleRadius), Color.green, 0);
            if (dir.sqrMagnitude > 0.0f){
                Quaternion rot = Quaternion.LookRotation(dir, Vector3.back);
                
            }
        }

        // float angle = 0;
        // // work out the angle
        // if (horizontalLook != 0.0f || verticalLook != 0.0f) {
        //     // angle = Mathf.Atan2(horizontalLook, verticalLook) * Mathf.Rad2Deg;
        //     angle = Mathf.Atan2(verticalLook, horizontalLook) * Mathf.Rad2Deg;
        // }
        // Debug.Log(angle);
        // Input.GetAxis("Look Vertical"),
        // Vector3 aimDirection = new Vector3(horizontalLook, verticalLook).normalized;
        // Debug.DrawLine(transform.position, transform.position + (aimDirection * reticleRadius), Color.green, 0);

        
        // Debug.Log(horizontalLook);
        // Debug.Log(verticalLook);
        // set to the reticleRadius * horizontal / vertical

        // float reticleX = Mathf.Lerp(transform.position.x, transform.position.x + reticleRadius, horizontalLook);
        // float reticleY = Mathf.Lerp(transform.position.y, transform.position.y + reticleRadius, verticalLook);
        // Debug.Log(reticleX);
        // Debug.Log(reticleY);
        // // Vector3 radiusMaxPos
        // // Vector3 retPosX = Vector3.Lerp(transform.position, )
        // reticle.transform.position.Set(reticleX, reticleY, reticle.transform.position.z);
    }

}
