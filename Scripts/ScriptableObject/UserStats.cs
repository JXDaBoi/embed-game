using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "UserStats", menuName = "ScriptableObjects/UserStats", order = 1)]
public class UserStats : ScriptableObject
{
    public bool[] passedLevel = new bool[2];

    public bool GetUserProgress(int level)
    {
        return passedLevel[level];
    }

    public void ResetUserProgress()
    {
        for (int i = 0; i < passedLevel.Length; i++)
        {
            passedLevel[i] = false;
        }
    }

    public void levelPassed()
    {

        if (SceneManager.GetActiveScene().name == "firstlv")
        {
            passedLevel[0] = true;
        }
        else
        {
            passedLevel[1] = true;
        }
    }
}
