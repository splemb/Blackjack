using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();

    void Start()
    {
        for (int s = 0; s < 4; s++)
        {
            for (int r = 1; r <= 13; r++)
            {
                switch (r)
                {
                    case 1:
                        deck.Add(new Card(s.ToString()[0], 'A'));
                        break;
                    case 10:
                        deck.Add(new Card(s.ToString()[0], '0'));
                        break;
                    case 11:
                        deck.Add(new Card(s.ToString()[0], 'J'));
                        break;
                    case 12:
                        deck.Add(new Card(s.ToString()[0], 'Q'));
                        break;
                    case 13:
                        deck.Add(new Card(s.ToString()[0], 'K'));
                        break;
                    default:
                        deck.Add(new Card(s.ToString()[0], r.ToString()[0]));
                        break;
                }
            }
        }
        ShuffleDeck();
        //ReadDeck();

    }

    void ShuffleDeck()
    {
        for (int c = 0; c < deck.Count; c++)
        {
            Card tmp = deck[c];
            int r = Random.Range(c, deck.Count);
            deck[c] = deck[r];
            deck[r] = tmp;
        }
    }

    void ReadDeck()
    {
        for (int c = 0; c < deck.Count; c++)
        {
            Debug.Log(deck[c].name + " || Worth " + deck[c].value);
        }
    }
}
