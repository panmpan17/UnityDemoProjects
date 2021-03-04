using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NumberKeypadInputField : MonoBehaviour
{
    [SerializeField]
    private NumberKeypadController m_keypadController;
    private InputField m_inputField;

    private bool m_isLastFocus;

    void Start()
    {
        m_inputField = GetComponent<InputField>();

        m_inputField.onEndEdit.AddListener(delegate(string str) {
            m_keypadController.gameObject.SetActive(false);
        });

        m_keypadController.onNumberChanged += delegate(string str) {
            m_inputField.text = str;
        };
    }

    void Update()
    {
        if (m_inputField.isFocused)
        {
            if (!m_isLastFocus)
            {
                m_keypadController.gameObject.SetActive(true);
                m_isLastFocus = true;
            }
        }
        else if (m_isLastFocus)
            m_isLastFocus = false;
    }
}
