using System.Collections;
using UnityEngine;

public class CanvasSettings : UICanvas
{
    [SerializeField] GameObject[] buttons;
    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void SetState(UICanvas canvas)
    {
        //hide all buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        if (canvas is CanvasMainMenu)
        {
            buttons[2].SetActive(true);//Button - Close
        }
        else if (canvas is CanvasGamePlay) 
        {
            buttons[0].SetActive(true);//Button - MainMenu
            buttons[1].SetActive(true);//Button - Continue
        }
    }
}
