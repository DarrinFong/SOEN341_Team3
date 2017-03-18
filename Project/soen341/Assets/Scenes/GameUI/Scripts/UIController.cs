using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class UIController : MonoBehaviour {

    Camera mainCamera;
    Transform planeTransform;

    public Action actionBlock;
    public Container containerBlock;
    public Condition conditionBlock;
    public Character characterBlock;

    public Plane UINormal;
    public LayerMask collisionMask;

    private Container[] allContainers;
    private List<Container> topContainers = new List<Container>();
    private List<List<Block>> allElements = new List<List<Block>>();

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
        Plane UINormal = new Plane(Vector3.back , this.transform.position); //abstract ground plane looking up at the origin T
        float rayLength;

        if (UINormal.Raycast(cameraRay, out rayLength))
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

        if (Physics.Raycast(cameraRay, out hit, 1000, collisionMask, QueryTriggerInteraction.Collide))
        {

            onHitObject(hit);
            Debug.DrawLine(cameraRay.origin, hit.collider.gameObject.transform.position, Color.blue);
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
                
                //hit.collider.gameObject.GetComponent<Block>().getParentContainer().GetComponent<Container>().removeElement(index);
                if(hit.collider.gameObject.GetComponent<Block>().getParentContainer().GetComponent<Block>().getContainerType() == 1)
                    hit.collider.gameObject.GetComponent<Block>().getParentContainer().GetComponent<Condition>().removeElement(index);
                else
                    hit.collider.gameObject.GetComponent<Block>().getParentContainer().GetComponent<Character>().removeElement(index);
                hit.collider.gameObject.GetComponent<Block>().setParentContainerNull(); //MAKE BATMAN

            }
        }

        switch (hit.collider.gameObject.name) {

            //tried using enumerator but wasn't working well, so these are the values
            //forward = 0
            //backward = 1
            //left = 2
            //right = 3

            case "moveForward":
                createAction(0, objectHit);
                break;
            case "moveBackward":
                createAction(1, objectHit);
                break;
            case "moveLeft":
                createAction(2, objectHit);
                break;
            case "moveRight":
                createAction(3, objectHit);
                break;
            case "ifCondition":
                createCondition(0, objectHit);
                break;
            case "for2Condition":
                createCondition(1, objectHit);
                break;
            case "for5Condition":
                createCondition(2, objectHit);
                break;
            case "for10Condition":
                createCondition(3, objectHit);
                break;
            case "mainCharacter":
                createCharacter(0, objectHit);
                break;
            case "reset":
                foreach (var item in topContainers)
                {
                    item.RemoveAll();
                }
                CharacterActions charController = GameObject.FindObjectOfType<CharacterActions>();
                charController.clearActions();
                break;
            case "run":
                runGame();
                break;
            case "start":
                print("It actually works");
                //hide start panel
                break;
            default:
                break;
        }

    }


    public void createAction(int type, Transform spawn)
    {
        Block newAction = Instantiate(actionBlock, spawn.position, spawn.rotation) as Action;
        newAction.setType(type);
    }

    public void createCondition(int type, Transform spawn)
    {
        Block newCondition = Instantiate(conditionBlock, spawn.position, spawn.rotation) as Condition;
        newCondition.setType(type);
    }

    public void createCharacter(int type, Transform spawn)
    {
        Block newCharacter = Instantiate(characterBlock, spawn.position, spawn.rotation) as Character;
        newCharacter.setType(type);
    }

    public void runGame()
    {
        getContainers();
       
        foreach (Block b in topContainers)
        {
            b.StartCoroutine("executeBlock");
        }
        
    }

    public void getAllElements()
    {
        foreach (Container c in topContainers)
        {
            allElements.Add(c.getContainerList());
        }
    }

    public void getContainers()
    {
        allContainers = this.gameObject.GetComponentsInChildren<Container>();

        foreach (Container c in allContainers)
            if (c.parentIsNull())
                topContainers.Add(c);

    }


}
