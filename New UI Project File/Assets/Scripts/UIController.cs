using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class UIController : MonoBehaviour {

    Camera mainCamera;
    Transform planeTransform;

    public Action actionBlock;
    public Container containerBlock;

    public LayerMask collisionMask;

	// Use this for initialization
	void Start () {

        mainCamera = FindObjectOfType<Camera>();
        planeTransform = this.transform;

	}
	
	// Update is called once per frame
	void Update () {

        //print(cameraToUI());

       if (Input.GetMouseButtonDown(0))
        {

            rayToObject();
           
        }

    }

    public Vector3 cameraToUI()
    {

        Vector3 point = Vector3.zero;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        //TODO: Switch the plane normal to the UIPlane normal
        Plane groundPlane = new Plane(Vector3.back, this.transform.position); //abstract ground plane looking up at the origin T
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            point = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, point, Color.blue);
            //transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        return point;

    }

    private void rayToObject()
    {


        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Plane groundPlane = new Plane(Vector3.back, Vector3.zero); //abstract ground plane looking up at the origin
        //float rayLength;

        if (Physics.Raycast(cameraRay, out hit, 1000, collisionMask, QueryTriggerInteraction.Collide))
        {

            onHitObject(hit);
            Debug.DrawLine(cameraRay.origin, hit.collider.gameObject.transform.position, Color.blue);

            //print(hit.collider.gameObject.name);
            //Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            //return hit;
        }

    }


    public void onHitObject(RaycastHit hit)
    {

        //print(hit.collider.gameObject.name);

        Transform objectHit = hit.collider.gameObject.transform;

        if(hit.collider.gameObject.tag == "Action" || hit.collider.gameObject.tag == "Container")
        {
            hit.collider.gameObject.GetComponent<Block>().dragEnable();
            if (!hit.collider.gameObject.GetComponent<Block>().parentIsNull())
            {
                //remove from parent
                int index = hit.collider.gameObject.GetComponent<Block>().getIndex();
                hit.collider.gameObject.GetComponent<Block>().getParentContainer().GetComponent<Container>().removeElement(index);

                hit.collider.gameObject.GetComponent<Block>().setParentContainerNull();

            }
        }

        switch (hit.collider.gameObject.name) {

            case "createAction":
                Block newAction = Instantiate(actionBlock, objectHit.position, objectHit.rotation) as Action;
                break;
            case "createContainer":
                Block newContainer = Instantiate(containerBlock, objectHit.position, objectHit.rotation) as Container;
                break;
            default:
                break;
        }

    }

}
