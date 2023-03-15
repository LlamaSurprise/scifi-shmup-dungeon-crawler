using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 7;
    public float reticleRadius = 5;

    public GameObject reticle;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject/
    }

    // Update is called once per frame
    void Update()
    {
        //Define the speed at which the object moves.
        float horizontalMove = Input.GetAxis("Move Horizontal");
        float verticalMove = Input.GetAxis("Move Vertical");

        transform.Translate(new Vector3(horizontalMove, verticalMove, 0) * moveSpeed * Time.deltaTime);
        //Move the object to XYZ coordinates defined as horizontalInput, 0, and verticalInput respectively.
        reticle = transform.Find("reticle").gameObject;
        //https://answers.unity.com/questions/1350081/xbox-one-controller-mapping-solved.html
        float horizontalLook = Input.GetAxis("Look Horizontal");
        float verticalLook = Input.GetAxis("Look Vertical");
        // Debug.Log(horizontalLook);
        // set to the reticleRadius * horizontal / vertical
    }
}
