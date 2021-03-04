using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NumberKeypadDisplay : MonoBehaviour
{
    [SerializeField]
    private NumberKeypadController m_keypadController;

    private Text m_text;

    void Start() {
        m_text = GetComponent<Text>();

        m_keypadController.onNumberChanged += delegate(string str) {
            m_text.text = str;
        };
    }
}
