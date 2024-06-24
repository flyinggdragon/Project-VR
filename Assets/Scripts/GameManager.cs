using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int score = 0;
    public static GameObject scoreObject;
    //public static FishInterface fishInterface;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public static void ToggleCollection()
    //{
        //if (fishInterface != null)
        //{
            //fishInterface.Toggle();
        //}
    //}
}
