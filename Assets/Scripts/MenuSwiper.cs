using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 panelLocation;
    RectTransform rect;
    public float percentThreshold = 0.05f;
    float easing;
    float rectTransformWidth = 0f;
    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        panelLocation = GetComponent<RectTransform>().anchoredPosition;
        rectTransformWidth = GetComponent<RectTransform>().rect.width;

    #if (UNITY_IOS || UNITY_ANDROID)
        easing = 0.5f;
    #else
        easing = 0.1f;
    #endif
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;

        rect.anchoredPosition = panelLocation - new Vector3(difference * speed, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
       float percentage = (data.pressPosition.x - data.position.x) / Screen.width; 
       if (Mathf.Abs(percentage) >= percentThreshold)
       {
            Vector3 newLocation = panelLocation;
            
            if (percentage > 0)
            {
                newLocation += new Vector3(-rectTransformWidth, 0, 0);
                StartCoroutine(SmoothScroll(rect.anchoredPosition, newLocation, easing));
                transform.parent.GetComponent<SelectMenu>().ChangePage(1);

            }
            else if (percentage < 0)
            {
                newLocation += new Vector3(rectTransformWidth, 0, 0);
                StartCoroutine(SmoothScroll(rect.anchoredPosition, newLocation, easing));
                transform.parent.GetComponent<SelectMenu>().ChangePage(-1);
            }
            
            panelLocation = newLocation;
       }
       else
       {
            StartCoroutine(SmoothScroll(rect.anchoredPosition, panelLocation, easing));
       }
    }

    IEnumerator SmoothScroll(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            rect.anchoredPosition = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void PageRight()
    {
        Vector3 newLocation = panelLocation + new Vector3(-rectTransformWidth, 0, 0);
        StartCoroutine(SmoothScroll(rect.anchoredPosition, newLocation, easing));
        panelLocation = newLocation;
        transform.parent.GetComponent<SelectMenu>().ChangePage(1);
    }

    public void PageLeft()
    {
        Vector3 newLocation = panelLocation + new Vector3(rectTransformWidth, 0, 0);
        StartCoroutine(SmoothScroll(rect.anchoredPosition, newLocation, easing));
        panelLocation = newLocation;
        transform.parent.GetComponent<SelectMenu>().ChangePage(-1);
    }
}
