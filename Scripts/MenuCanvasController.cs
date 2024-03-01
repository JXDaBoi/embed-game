using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCanvasController : MonoBehaviour
{
    public UserStats userStats;
    public GameObject[] buttons;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (userStats.passedLevel[0])
        {
            buttons[0].GetComponent<Button>().enabled = true;
            buttons[0].GetComponent<CanvasGroup>().alpha = 1;
        }

        if (userStats.passedLevel[1])
        {
            buttons[1].GetComponent<Button>().enabled = true;
            buttons[1].GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void lvl1()
    {
        SceneManager.LoadScene("firstlv");
    }

    public void lvl2()
    {
        SceneManager.LoadScene("secondlv");
    }

    public void lvl3()
    {
        SceneManager.LoadScene("thirdlv");
    }
}
