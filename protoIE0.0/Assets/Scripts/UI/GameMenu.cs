using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public Camera3rd myCam;
   
	// Use this for initialization
	public void Toggle_Changed(bool newVal) {
        myCam.ReverseSideLookJoystick = newVal;

    }

    public void OnClickExitToMain(){;
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}
