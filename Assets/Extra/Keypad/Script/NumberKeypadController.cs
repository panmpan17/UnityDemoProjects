using System;
using UnityEngine;
using UnityEngine.UI;

public class NumberKeypadController : MonoBehaviour
{
    public enum KeyType {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Decimal = 21,
        Clear = 22,
    }

    private long m_number;

    private bool m_isDecimal = false;
    private long m_decimalNumber;
    private int m_decimalZeroNumber = 0;

    public Action<string> onNumberChanged;

    private AudioSource m_keyPressedSound;

    public string NumberString {
        get {
            if (m_isDecimal)
            {
                string s = m_number.ToString() + ".";

                for (var i = 0; i < m_decimalZeroNumber; i++)
                    s += "0";
                
                if (m_decimalNumber > 0)
                    s += m_decimalNumber.ToString();

                return s;
            }
            else
                return m_number.ToString();
        }
    }


    private void Start() {
        m_keyPressedSound = GetComponent<AudioSource>();
    }


    public void KeyPressed(int keyTypeIndex)
    {
        KeyType type = (KeyType) keyTypeIndex;

        switch (type)
        {
            case KeyType.Zero:
                if (m_isDecimal)
                {
                    if (m_decimalNumber == 0)
                        m_decimalZeroNumber++;
                    else
                        m_decimalNumber *= 10;
                }
                else
                    m_number *= 10;
                break;
            case KeyType.One:
            case KeyType.Two:
            case KeyType.Three:
            case KeyType.Four:
            case KeyType.Five:
            case KeyType.Six:
            case KeyType.Seven:
            case KeyType.Eight:
            case KeyType.Nine:
                int number = keyTypeIndex;

                if (m_isDecimal)
                {
                    if (m_decimalNumber <= long.MaxValue / 10)
                        m_decimalNumber = m_decimalNumber * 10 + number;
                }
                else
                {
                    if (m_number <= long.MaxValue / 10)
                        m_number = m_number * 10 + number;
                }
                break;
            case KeyType.Decimal:
                m_isDecimal = true;
                break;
            case KeyType.Clear:
                m_number = 0;

                m_isDecimal = false;
                m_decimalNumber = 0;
                m_decimalZeroNumber = 0;
                break;
        }

        if (m_keyPressedSound != null)
            m_keyPressedSound.Play();

        if (onNumberChanged != null)
            onNumberChanged.Invoke(NumberString);
    }
}
