using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowMenus : MonoBehaviour
{
    public GameObject menuBook;
    public GameObject workerInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void StartInfo()
    {
        SceneManager.LoadScene("Info");
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToBook()
    {
        menuBook.SetActive(true);
        workerInfo.SetActive(false);
    }

    public void GoToInfo()
    {
        menuBook.SetActive(false);
        workerInfo.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
