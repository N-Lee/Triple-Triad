using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag : MonoBehaviour
{

    private bool isDragging = false;
    private bool isDroppable = false;
    public bool fixOffset = false;
    private int siblingIndex;
    private Vector3 startPos;
    private GameObject canvas, startParent, endParent;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    public AudioClip invalidClip, cursorClip;
    GameEngine gameEngine;
    
    public void StartDrag()
    {
        if (transform.parent.tag != "DropZone")
        {
            isDragging = true;
            audioSource.clip = cursorClip;
            audioSource.Play();
            startParent = transform.parent.gameObject;
            siblingIndex = transform.GetSiblingIndex();
            transform.SetParent(gameEngine.canvas.transform, false);
        }
    }

    public void EndDrag(){
        if (isDroppable)
        {
            transform.SetParent(endParent.transform, false);
            transform.SetSiblingIndex(0);
            transform.position = endParent.transform.position;

            if (isDragging)
            {
                audioSource.clip = cursorClip;
                audioSource.Play();
                endParent.GetComponent<DropZone>().PlaceCardObj(gameObject);
            }
        }
        else
        {
            transform.position = startPos;
            audioSource.clip = invalidClip;
            audioSource.Play();
            transform.SetParent(startParent.transform, false);
            transform.SetSiblingIndex(siblingIndex);
            fixOffset = true;
        }
        gameEngine.DeactivateInfoHighlight();
        isDragging = false;
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        canvas = GameObject.Find("Canvas");
        audioSource = GetComponent<AudioSource>();
        gameEngine = GameObject.Find("GameEngine").GetComponent<GameEngine>();
    }

    void Update()
    {
        if (isDragging){
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DropZone" && other.gameObject.GetComponent<DropZone>().isEmpty)
        {
            endParent = other.gameObject;
            isDroppable = true;
        }
    }

   void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "DropZone")
        {
            isDroppable = false;
        }
    } 
}
