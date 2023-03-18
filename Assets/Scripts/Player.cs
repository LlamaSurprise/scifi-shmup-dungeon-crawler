using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 7;
    public float recticleDistance = 3;
    public float cameraUnitDistance = 0.5f;

    public float cameraMoveTime = 0.05f;
    
    public float lookThreshold = 0.1f;
    public float moveThreshold = 0.1f;

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

    // Handle player movement.
    void Move() {
        //Define the speed at which the object moves.
        float horizontalMove = Input.GetAxis("Move Horizontal");
        float verticalMove = Input.GetAxis("Move Vertical");

        //Move the object to XYZ coordinates defined as horizontalInput, 0, and verticalInput respectively.
        if (Mathf.Abs(horizontalMove) > moveThreshold || Mathf.Abs(verticalMove) > moveThreshold) {
            transform.Translate(new Vector2(horizontalMove, verticalMove) * (moveSpeed * Time.deltaTime));
        }
    }


    // TODO - could be useful: //https://answers.unity.com/questions/1350081/xbox-one-controller-mapping-solved.html    
    // Handle player look direction and camera movement.
    void Look() {
        reticle = transform.Find("reticle").gameObject;
        SpriteRenderer reticleSprite = reticle.GetComponent<SpriteRenderer>();
        float horizontalLook = Input.GetAxis("Look Horizontal");
        float verticalLook = Input.GetAxis("Look Vertical");
        
        Debug.Log($"Raw: {new Vector2(horizontalLook, verticalLook)}");
        // check that it's over a certain threshold to avoid noise.
        if (Mathf.Abs(horizontalLook) > lookThreshold || Mathf.Abs(verticalLook) > lookThreshold){
            // Calculate where we want to aim using the joystick
            Vector3 aimDir = (Vector2.right * horizontalLook) + (Vector2.up * verticalLook);

            // Show the sprite
            reticleSprite.enabled = true;

            // set its pos
            Vector2 reticlePosition = transform.position + (aimDir * recticleDistance);
            reticle.transform.position = reticlePosition;
            
            // Show our aim direction
            // Debug.DrawLine(transform.position, reticlePosition, Color.green, 0);

            // Smoothly move the camera to some position between the reticle and the player.
            Camera mainCam = Camera.main;
            Vector2 camTargetPos2 = Vector2.Lerp(transform.position, reticlePosition, cameraUnitDistance);
            Vector3 camTargetPos3 = new Vector3(camTargetPos2.x, camTargetPos2.y, mainCam.transform.position.z);

            Vector3 camVelocity = new Vector3();
            mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, camTargetPos3, ref camVelocity, cameraMoveTime);
        } else {
            // Only show the reticle when we're getting player input 
            reticleSprite.enabled = false;
        }
    }

}
