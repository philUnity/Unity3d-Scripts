using UnityEngine;

//this has to attached to the object like torch, box etc...
//you need to add a emptyobject to the player and name it HoldPoint, this will be add at the script empty field.

public class PickupObject : MonoBehaviour, IInteractable
{
    private bool isHeld = false;
    private bool isMoving = false;

    private Transform originalParent;
    private Rigidbody rb;

    public Transform holdPoint;
    public float throwForce = 500f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;
    }

    void Update()
    {
        if (isHeld)
        {
            transform.position = holdPoint.position;
            transform.rotation = holdPoint.rotation;

            if (Input.GetMouseButtonDown(1))
            {
                Throw();
            }
        }
    }

    public void Interact()
    {
        if (isMoving)
            return;

        if (!isHeld)
            Pickup();
    }

    public string GetPromptText()
    {
        return isMoving ? "" : isHeld ? "[Right Mouse] Throw" : "[E] Pick up";
    }

    private void Pickup()
    {
        isMoving = true;

        rb.useGravity = false;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        transform.SetParent(holdPoint);
        transform.position = holdPoint.position;
        transform.rotation = holdPoint.rotation;

        isHeld = true;
        isMoving = false;
    }

    private void Throw()
    {
        isMoving = true;

        transform.SetParent(originalParent);
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(cam.transform.forward * throwForce);

        isHeld = false;
        isMoving = false;
    }
}
