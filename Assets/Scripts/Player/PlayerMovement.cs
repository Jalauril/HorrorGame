using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5f;
    public float jumpForce = 5f;
    public float camMaxRotation = 90f;

    private Vector3 movement;
    private Rigidbody playerRigidbody;
    private Transform cam;


    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>().transform;

        //Lukitaan kursori
        Cursor.lockState = CursorLockMode.Locked;
    }

	
	void FixedUpdate () //Liikkuminen ja hyppääminen
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        
        if(Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, Vector3.down, 1f + 0.001f))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Move(h, v);

	}


    void Update()  //Luetaan hiiren liike ja käännetään sen perusteella kameraa ja pelaajaa.
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        playerRigidbody.transform.Rotate(new Vector3(0f, mouseX, 0f));
        
        cam.Rotate(new Vector3(-mouseY, 0f, 0f)); //Käännetään kameraa
        cam.localEulerAngles = new Vector3(ClampRotation(cam.localEulerAngles.x, -camMaxRotation, camMaxRotation), 0f, 0f); //Korjataan kameran kulma.
    }


    void Move(float h, float v) //Määrää liikkeen suuruuden ja suunnan inputin perusteella
    {
        movement.Set(h, 0f, v);

        //Käytetään quaternionia liikevektorin oikeaan suuntaan kääntämiseen
        movement = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f)) * movement * speed * Time.fixedDeltaTime; 

        playerRigidbody.MovePosition(transform.position + movement);
    }

    float ClampRotation(float value, float min, float max)
    {
        //Muunnetaan negatiivinen kulma ja muunnetaan se vastaavaksi positiiviseksi kulmaksi.
        min = 360f + min;

        //Rajoitetaan arvo halutun minimin ja maksimin väliin.
        if (value < min && value > max)
            value = min;
        if (value > max && value < min)
            value = max;

        return value;
    }
}
