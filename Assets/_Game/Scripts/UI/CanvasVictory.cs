using System.Collections;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] TextMeshProUGUI score;

    public void SetBestScore(int _coin)
    {
        score.text = _coin.ToString();
    }
    public void MainMenuButton()
    {
        CloseDirectly();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
