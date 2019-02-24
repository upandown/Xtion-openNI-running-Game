using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonGame : MonoBehaviour {

    // Use this for initialization
    public GameObject Interface;

    public void EnterGame(int i) {
        StartCoroutine(MyMethod(i));
      
    }

    IEnumerator MyMethod(int i)
    {
       
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(i);
    }
    public void AwakeInterface()
    {
        if (Interface.activeSelf)
            Interface.SetActive(false);
        else
            Interface.SetActive(true);
    }
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
