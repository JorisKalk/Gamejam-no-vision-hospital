using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DroppingCoins : MonoBehaviour
{
    [SerializeField]
    private int maxCoins = 20;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private TextMeshProUGUI text;

    [Header("Dropping Coins")]
    [SerializeField]
    private float coinDropCooldown = .5f;

    [Header("Pick Up Coins")]
    [SerializeField]
    private float pickUpRange = 1.5f;

    private int coinsLeft;
    private float coinDropCooldownLeft;

    private List<GameObject> droppedCoinList = new List<GameObject>();

    void Start()
    {
        coinsLeft = maxCoins;
    }

    void Update()
    {
        text.text = "Coins left: " + coinsLeft;

        if (coinDropCooldownLeft <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DropCoin();
            }
        }
        else
        {
            coinDropCooldownLeft -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpCoin();
        }
    }

    private void DropCoin()
    {
        if (coinsLeft > 0)
        {
            GameObject newCoin = Instantiate(coinPrefab);
            newCoin.transform.position = transform.position;
            droppedCoinList.Add(newCoin);

            coinsLeft--;
            coinDropCooldownLeft = coinDropCooldown;
        }
    }

    private void PickUpCoin()
    {
        List<GameObject> coinsToDestroy = new List<GameObject>();
        if (droppedCoinList.Count > 0)
        {
            foreach (GameObject coin in droppedCoinList)
            {
                if (Vector2.Distance(coin.transform.position, transform.position) <= pickUpRange)
                {
                    coinsLeft++;
                    coinsToDestroy.Add(coin);
                }
            }
            if (coinsToDestroy.Count > 0)
            {
                foreach (GameObject coin in coinsToDestroy)
                {
                    droppedCoinList.Remove(coin);
                    Destroy(coin);
                }
            }
        }
    }
}
