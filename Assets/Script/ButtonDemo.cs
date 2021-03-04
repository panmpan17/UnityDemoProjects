using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;

public class ButtonDemo : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;

    private int a = 1;


    public void Login()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        Debug.Log(username);
        Debug.Log(password);










        // Debug.LogFormat("Username: {0}, Password: {1}", username, password);
    }










    















    public void ClickMe1() {
        Debug.Log(1);
    }

    private void ClickMe2() {
        Debug.Log(2);
    }

    void ClickMe3() {
        Debug.Log(2);
    }


}
