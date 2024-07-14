using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Transform parent;
    Dictionary<System.Type, UICanvas> canvasesActive = new Dictionary<System.Type, UICanvas>();
    Dictionary<System.Type, UICanvas> canvasesPrefab = new Dictionary<System.Type, UICanvas>();

    private void Awake()
    {
        //Load ui prefabs from Resources
        UICanvas[] prefabs = Resources.LoadAll<UICanvas>("UI/");
        for (int i = 0; i < prefabs.Length; i++)
        {
            canvasesPrefab.Add(prefabs[i].GetType(), prefabs[i]);
        }
    }

    //Open canvas
    public T OpenUI<T>() where T : UICanvas
    {
        T canvas = GetUI<T>();
        canvas.Setup();
        canvas.Open();
        return canvas;
    }

    //close canvas after _time
    public void CloseUI<T>(float _time) where T : UICanvas
    {
        if (IsOpened<T>())
        {
            canvasesActive[typeof(T)].Close(_time);
        }
    }

    //close canvas immediately
    public void CloseUIDirectly<T>() where T : UICanvas
    {
        if (IsOpened<T>())
        {
            canvasesActive[typeof(T)].CloseDirectly();
        }
    }

    //Check canvas is created or not
    public bool IsUILoaded<T>() where T:UICanvas
    {
        return canvasesActive.ContainsKey(typeof(T)) && canvasesActive[typeof(T)] != null;

    }

    //Check canvas is opened or not
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsUILoaded<T>() && canvasesActive[typeof(T)].gameObject.activeSelf;
    }

    //Get active Canvas
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsUILoaded<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canvas = Instantiate(prefab, parent);
            canvasesActive[typeof(T)] = canvas;
        }

        return canvasesActive[typeof(T)] as T;
    }

    //Get prefab
    private T GetUIPrefab<T>() where T : UICanvas
    {
        return canvasesPrefab[typeof(T)] as T;
    }

    //Close all canvas
    public void CloseAll()
    {
        foreach (KeyValuePair<System.Type, UICanvas> canvas in canvasesActive)
        {
            if (canvas.Value != null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.CloseDirectly();
            }
        }
    }
}
