using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false;

    public virtual void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float ratio = (float) Screen.width / (float) Screen.height;

        //fixed align for Notch(rabbit ear) on Iphone 
        if (ratio > 2.1f)
        {
            Vector2 leftBottom = rect.offsetMin;
            Vector2 rightTop = rect.offsetMax;
            
            leftBottom.y = 0f;
            rightTop.y = -100f;

            rect.offsetMin = leftBottom;
            rect.offsetMax = rightTop;
        }
    }

    //Called before cavas is activated
    public virtual void Setup()
    {

    }
    //Called after activated
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    //Close canvas after _time
    public virtual void Close(float _time)
    {
        Invoke(nameof(CloseDirectly), _time);
    }
    //Close cavas immediately
    public virtual void CloseDirectly()
    {
        if (isDestroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
