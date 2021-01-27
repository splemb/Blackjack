using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Card[] deck = new Card[52];

    void Start()
    {
        int indexCount = 0;
        for (int s = 0; s < 4; s++)
        {
            for (int r = 1; r <= 13; r++)
            {
                switch (r)
                {
                    case 1:
                        deck[indexCount] = new Card(r, s.ToString()[0], 'A');
                        break;
                    case 10:
                        deck[indexCount] = new Card(r, s.ToString()[0], '0');
                        break;
                    case 11:
                        deck[indexCount] = new Card(r, s.ToString()[0], 'J');
                        break;
                    case 12:
                        deck[indexCount] = new Card(r, s.ToString()[0], 'Q');
                        break;
                    case 13:
                        deck[indexCount] = new Card(r, s.ToString()[0], 'K');
                        break;
                    default:
                        deck[indexCount] = new Card(r, s.ToString()[0], r.ToString()[0]);
                        break;
                }
                indexCount++;
            }
        }
        ShuffleDeck();
        ReadDeck();

    }

    void ShuffleDeck()
    {
        for (int t = 0; t < deck.Length; t++)
        {
            Card tmp = deck[t];
            int r = Random.Range(t, deck.Length);
            deck[t] = deck[r];
            deck[r] = tmp;
        }
    }

    void ReadDeck()
    {
        foreach (Card c in deck)
        {
            Debug.Log(c.name);
        }
    }
}
