using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public float doorSpeed = 5f;

    public Transform door;
    Vector3 direction;
    Vector3 closedPosition;
    Vector3 openPosition;
    Vector3 movement;
    bool opening;
    bool closing;

	void Awake ()
    {
        
        closedPosition = door.position;
        movement = (closedPosition + (new Vector3(0, 0, 1) * door.localScale.z)) - closedPosition;
        movement = movement / movement.magnitude;
        direction = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f)) * new Vector3(0, 0, 1);
        openPosition = closedPosition + (direction * door.localScale.z);
    }
	
	void Update ()
    {
        if (opening && Vector3.Distance(openPosition, door.position) >= 0.05f)
        {
            door.Translate(movement * Time.deltaTime * doorSpeed);
        }

        if (closing && Vector3.Distance(closedPosition, door.position) >= 0.05f)
        {
            door.Translate(-movement * Time.deltaTime * doorSpeed);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Open();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Close();
        }
    }

    public void Open()
    {
        opening = true;
        closing = false;
    }

    public void Close()
    {
        opening = false;
        closing = true;
    }
}
