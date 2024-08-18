// References:
// https://fistfullofshrimp.com/unity-drag-things-around/
// Unity docs
// Pokemon Go Throwing in Unity (https://www.youtube.com/watch?v=eKhmcPa_Fjg) for calculating based on last 10 frames

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private Plane draggingPlane;
    private Vector3 offset;
    private Camera mainCamera;
    private Rigidbody rb;

    private int deltaHistorySize = 10;
    private readonly Queue<Vector3> deltaHistory = new();

    private float zForce = 0.5f;
    private bool thrown = false;
    private bool landed = false;
    private bool spawned = false;
    private double epsilon = 0.05;
    [SerializeField] private float spawnDelay = 0.5f;

    private Vector3 startPosition = new Vector3(0, 0.55f, 0);
    private Quaternion startRotation = Quaternion.identity;
    [SerializeField] private bool reset = false;

    private GameObject Block;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        Block = GameObject.Find("Cube Source");
    }

    // Update is called once per frame
    void Update()
    {
        if (landed) {
            return;
        }

        // for testing in editor
        if (reset) {
            transform.position = startPosition;
            transform.rotation = startRotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            reset = false;
            thrown = false;
            landed = false;
            rb.constraints = RigidbodyConstraints.None;
        }

        if (thrown) {
            if (!spawned) {
                spawned = true;
                StartCoroutine(SpawnBlockDelay());
            }
            if (Mathf.Abs(rb.velocity.magnitude) <= epsilon && Mathf.Abs(rb.angularVelocity.magnitude) <= epsilon)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                landed = true;
            }
        }
    }

    IEnumerator SpawnBlockDelay() {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(Block, startPosition, startRotation);
    }

    void OnCollisionEnter(Collision collision) {
        if (thrown) {
            if (collision.gameObject.tag == "Despawn") {
                // add some particle effects later..?
                Destroy(gameObject); // potentially has race condition w/ instantiation
            }

        }
    }

    void OnMouseDown()
    {
        if (thrown) {
            return;
        }

        deltaHistory.Enqueue(Input.mousePosition);

        draggingPlane = new Plane(mainCamera.transform.forward, transform.position);
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDistance;
        draggingPlane.Raycast(camRay, out planeDistance);
        offset = transform.position - camRay.GetPoint(planeDistance);
    }

    void OnMouseDrag()
    {
        if (thrown) {
            return;
        }

        deltaHistory.Enqueue(Input.mousePosition);
        if (deltaHistory.Count > deltaHistorySize) {
            deltaHistory.Dequeue();
        }

        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDistance;
        draggingPlane.Raycast(camRay, out planeDistance);
        transform.position = camRay.GetPoint(planeDistance) + offset;
    }

    void OnMouseUp()
    {
        if (thrown) {
            return;
        }

        Vector3 releasePosition = Input.mousePosition;

        Vector3 startPosition = deltaHistory.Peek();

        Vector3 difference = releasePosition - startPosition;
        
        if (difference.magnitude < 70) {
            deltaHistory.Clear();
            return;
        }

        Vector3 newThrow = new Vector3(difference.x, difference.y, difference.magnitude * zForce);
        
        rb.AddForce(newThrow);
        thrown = true;
    }

}
