using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Card> playerHand = new List<Card>();
    CardManager cardManager;

    private void Start()
    {
        cardManager = GameObject.FindGameObjectWithTag("Card Manager").GetComponent<CardManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (playerHand.Count == 0)
            {
                Hit();
                Hit();
                ReadHand();
            }
            else
            {
                Hit();
                ReadHand();
            }
        }
    }

    void Hit()
    {
        playerHand.Add(cardManager.deck[cardManager.deck.Count-1]);
        cardManager.deck.RemoveAt(cardManager.deck.Count - 1);
    }

    void ReadHand()
    {
        int total = 0;
        for (int c = 0; c < playerHand.Count; c++)
        {
            Debug.Log(playerHand[c].name + " || Worth " + playerHand[c].value);
            total += playerHand[c].value;
        }
        if (total < 21) Debug.Log("Total: " + total);
        else if (total == 21) Debug.Log("BLACKJACK");
        else Debug.Log("YOU LOSE! GOOD DAY SIR!");


    }
}
