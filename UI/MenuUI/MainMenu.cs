using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Livia
{


    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        public void QuitGame()
        {
            Application.Quit();
        }
        public void StartMenu()
        {
            SceneManager.LoadScene("StartScreen");
        }
        public void GoToCredits()
        {
            SceneManager.LoadScene("Credits");
        }

        public void set_menu_item(GameObject button)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button);
            //Debug.Log("current button: " + EventSystem.current.currentSelectedGameObject.name);

        }
    }
}
