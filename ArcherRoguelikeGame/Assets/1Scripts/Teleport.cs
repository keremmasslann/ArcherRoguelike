using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private bool swapMode = false;
    private RaycastHit raycastHit;
    private GameObject highlightedObject;
    [SerializeField] GameObject imagexd;

    void Update()
    {
        SwapModeInput();

        if (swapMode)
        {
            SwapPositions();
            imagexd.SetActive(true);
        }
        else
        {
            imagexd.SetActive(false);
        }

        if (highlightedObject != null)
            Debug.Log(highlightedObject.name);
    }

    private void FixedUpdate()
    {
        if (swapMode)
        {
            CheckHightlighted();
        }

    }

    void SwapPositions()
    {
        if (Input.GetMouseButtonDown(0) && highlightedObject != null)
        {
            //   StartCoroutine(SwapPositionsCr());

            Vector3 playerPosition = transform.position;
            Vector3 highlightedObjectPosition = highlightedObject.transform.position;

            transform.position = highlightedObjectPosition;

            highlightedObject.transform.position = playerPosition;

            highlightedObject.transform.position = new Vector3(highlightedObject.transform.position.x, highlightedObject.GetComponent<ISwappable>().teleportPosY, highlightedObject.transform.position.z);
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);

            // Reset selected object
            highlightedObject = null;
            swapMode = false;

        }
    }


    IEnumerator SwapPositionsCr()
    {
        swapMode = false;

        Vector3 playerPosition = transform.position;
        Vector3 highlightedObjectPosition = highlightedObject.transform.position;

        transform.position = highlightedObjectPosition;
        yield return new WaitForEndOfFrame();
        highlightedObject.transform.position = playerPosition;

        // Reset selected object
        highlightedObject = null;

    }
    void CheckHightlighted()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 1000))  //Highlighting
        {
            ISwappable swappable = raycastHit.collider.GetComponent<ISwappable>();
            if (swappable != null)
            {
                highlightedObject = raycastHit.collider.gameObject;
            }
            else
            {
                highlightedObject = null; //sonra aç
            }
        }


        // else hightlighted = null gerekbilir
    }



    void SwapModeInput()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (swapMode == false)
            {
                swapMode = true;
            }
            else
            {
                swapMode = false;
            }

        }
    }


}
