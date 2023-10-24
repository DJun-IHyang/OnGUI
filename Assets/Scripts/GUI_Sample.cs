using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_Sample : MonoBehaviour
{

    private void OnGUI()
    {
        //박스는 GUI의 배경(Background)를 작업할 때 많이 사용됨
        GUI.Box(new Rect(10, 10, 100, 90), "MY MENU");

        //버튼에 대한 설계
        //버튼을 눌렀을 경우를 작업할 때 if문에 GUI를 생성합니다 
        if (GUI.Button(new Rect(20, 40, 80, 20), "Scene 1"))
        {
            //버튼을 누를 경우 씬 이동을 진행해보겠습니다
            //Application.LoadLevel(1);
            SceneManager.LoadScene(1); //1번 씬으로 이동
        }
        if (GUI.Button(new Rect(20, 70, 80, 20), "Scene 2"))
        {
            //버튼을 누를 경우 씬 이동을 진행해보겠습니다
            //Application.LoadLevel(1);
            SceneManager.LoadScene(2); //2번 씬으로 이동
        }


    }

}
