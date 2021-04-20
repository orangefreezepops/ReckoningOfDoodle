using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController Instance;
    public int CoinNumber =0;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UIController.Instance.CoinText.text = CoinNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void addCoin()
    {
        CoinNumber++;
        UIController.Instance.CoinText.text = CoinNumber.ToString();
    }
    public void loseCoin(int i)
    {
        CoinNumber = CoinNumber - i;
        if (CoinNumber < 0) CoinNumber = 0;

        UIController.Instance.CoinText.text = CoinNumber.ToString();
    }

}
