using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_Sample : MonoBehaviour
{

    private void OnGUI()
    {
        //�ڽ��� GUI�� ���(Background)�� �۾��� �� ���� ����
        GUI.Box(new Rect(10, 10, 100, 90), "MY MENU");

        //��ư�� ���� ����
        //��ư�� ������ ��츦 �۾��� �� if���� GUI�� �����մϴ� 
        if (GUI.Button(new Rect(20, 40, 80, 20), "Scene 1"))
        {
            //��ư�� ���� ��� �� �̵��� �����غ��ڽ��ϴ�
            //Application.LoadLevel(1);
            SceneManager.LoadScene(1); //1�� ������ �̵�
        }
        if (GUI.Button(new Rect(20, 70, 80, 20), "Scene 2"))
        {
            //��ư�� ���� ��� �� �̵��� �����غ��ڽ��ϴ�
            //Application.LoadLevel(1);
            SceneManager.LoadScene(2); //2�� ������ �̵�
        }


    }

}
