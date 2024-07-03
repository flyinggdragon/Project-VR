using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlInterface : MonoBehaviour
{

    public void clicarBotaoTestar()
    {

        SceneManager.LoadScene("TestScene");
        
    }

    public void clicarBotaoJogar()
    {

        SceneManager.LoadScene("CenaryScene");

    }

    public void clicarBotaoSair()
    {

        Application.Quit();
        
    }
}
