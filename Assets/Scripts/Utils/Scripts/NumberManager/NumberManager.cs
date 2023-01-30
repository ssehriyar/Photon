using UnityEngine;

namespace NewUtils
{
    public class NumberManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _numbers = new GameObject[10];
        [SerializeField] private GameObject _go;
        [SerializeField] private int _maxNumber;

        public void Create()
        {
            int num = 2;
            while (num <= _maxNumber)
            {
                GenerateNumbers(num);
                num *= 2;
            }
        }

        private void GenerateNumbers(int number)
        {
            int digitCounter = 0;
            int unitsDigit = -1;
            int tensDigit = -1;
            int hundredsDigit = -1;

            int thousandsDigit = -1;
            int tenThousandsDigit = -1;
            int hundredThousandsDigit = -1;

            int millionDigit = -1;
            int tenMillionDigit = -1;
            int hundredMillionDigit = -1;

            int num = number;
            GameObject go = Instantiate(_go);
            GameObject newGo;

            while (num > 0)
            {
                num /= 10;
                digitCounter++;
            }
            num = number;

            if (number >= 100000000)
            {
                hundredMillionDigit = num / 100000000;
                newGo = Instantiate(_numbers[hundredMillionDigit]);
                newGo.transform.SetParent(go.transform);
                num -= hundredMillionDigit * 100000000;
            }
            if (number >= 10000000)
            {
                tenMillionDigit = num / 10000000;
                newGo = Instantiate(_numbers[tenMillionDigit]);
                newGo.transform.SetParent(go.transform);
                num -= tenMillionDigit * 10000000;
            }
            if (number >= 1000000)
            {
                millionDigit = num / 1000000;
                newGo = Instantiate(_numbers[millionDigit]);
                newGo.transform.SetParent(go.transform);
                num -= millionDigit * 1000000;
                newGo = Instantiate(_numbers[11]);
                newGo.transform.SetParent(go.transform);
                return;
            }
            if (number >= 100000)
            {
                hundredThousandsDigit = num / 100000;
                newGo = Instantiate(_numbers[hundredThousandsDigit]);
                newGo.transform.SetParent(go.transform);
                num -= hundredThousandsDigit * 100000;
            }
            if (number >= 10000)
            {
                tenThousandsDigit = num / 10000;
                newGo = Instantiate(_numbers[tenThousandsDigit]);
                newGo.transform.SetParent(go.transform);
                num -= tenThousandsDigit * 10000;
            }
            if (number >= 1000)
            {
                thousandsDigit = num / 1000;
                newGo = Instantiate(_numbers[thousandsDigit]);
                newGo.transform.SetParent(go.transform);
                num -= thousandsDigit * 1000;
                newGo = Instantiate(_numbers[10]);
                newGo.transform.SetParent(go.transform);
            }
            if (number >= 100)
            {
                hundredsDigit = num / 100;
                num -= hundredsDigit * 100;
                newGo = Instantiate(_numbers[hundredsDigit]);
                newGo.transform.SetParent(go.transform);
            }
            if (number >= 10)
            {
                tensDigit = num / 10;
                num -= tensDigit * 10;
                newGo = Instantiate(_numbers[tensDigit]);
                newGo.transform.SetParent(go.transform);
            }
            unitsDigit = num;
            newGo = Instantiate(_numbers[unitsDigit]);
            newGo.transform.SetParent(go.transform);

        }
    }
}