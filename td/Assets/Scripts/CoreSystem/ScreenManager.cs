using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{
    // Delegate
    // Events

    // Definitions
    // Properties
    // Fields
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float moveSpeed = 5f;

    private Ray2D ray;
    private RaycastHit2D hit;

    private SpriteRenderer previousSelectedRenderer;

    // Unity Messages
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("클릭한 오브젝트의 태그: " + hit.collider.gameObject.tag);
                Debug.Log("클릭한 오브젝트의 이름: " + hit.collider.gameObject.name);

                SpriteRenderer currentRenderer = hit.collider.GetComponent<SpriteRenderer>();
                if (currentRenderer != null)
                {
                    if (previousSelectedRenderer != null)
                    {
                        previousSelectedRenderer.color = Color.black;
                    }

                    currentRenderer.color = Color.green;

                    previousSelectedRenderer = currentRenderer;
                }
            }
        }
    }

    // Methods
    public void MoveToStory()
    {
        if (mainCamera != null)
        {
            StartCoroutine(MoveCamera(new Vector3(8, 0, -10)));
        }
    }

    public void MoveToDefence()
    {
        if (mainCamera != null)
        {
            StartCoroutine(MoveCamera(new Vector3(0, 0, -10)));
        }
    }

    // Functions
    // Event Handlers

    // Unity Coroutine
    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        Vector3 startPosition = mainCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
    }
}
