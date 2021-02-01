using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject DrawCardPrefab;
    public GameObject FlippedCardPrefab;
    public List<Card> deck = new List<Card>();
    public List<Card> dealerHand = new List<Card>();
    public int dealerTotal;
    public List<PlayerManager> players = new List<PlayerManager>();
    int currentPlayer = 0;
    bool playersLeft = true;
    bool lost = false;
    public float turnDelay;
    float nextTurnTime=0f;
    void Start()
    {      
        GenerateDeck();
        ShuffleDeck(3);
        players[currentPlayer].yourTurn=true;
    }

    void Update() {
        if (playersLeft) {
            foreach (PlayerManager p in players) {
                if (!p.lost && !p.stand) {
                    playersLeft=true;
                    break;
                }
                playersLeft=false;
            } 
        }
        else {
            DrawHand();
            foreach (PlayerManager p in players) {
                if (!p.lost) {
                    if (p.total > dealerTotal){
                        p.won=true;
                    }
                }
            }
        }
    }

    void GenerateDeck() {
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
    }

    void ShuffleDeck(int times)
    {
        for (int i = 0; i < times; i++) {
            for (int c = 0; c < deck.Count; c++)
            {
                Card tmp = deck[c];
                int r = Random.Range(c, deck.Count);
                deck[c] = deck[r];
                deck[r] = tmp;
            }
        }
    }

    void ReadDeck() //DEBUG
    {
        for (int c = 0; c < deck.Count; c++)
        {
            Debug.Log(deck[c].name + " || Worth " + deck[c].value);
        }
    }

    void DrawHand() {
        foreach (RenderCard c in GetComponentsInChildren<RenderCard>()) {
            Destroy(c.gameObject);
        }
        for (int c = 0; c < dealerHand.Count; c++)
        {
            if (!playersLeft || c == 0) {
                GameObject drawnCard = Instantiate(DrawCardPrefab,new Vector3(transform.position.x+((c+1)*0.8f),transform.position.y,0),Quaternion.identity,transform);
                drawnCard.transform.localScale=Vector3.one * 0.5f;
                drawnCard.GetComponent<RenderCard>().card = dealerHand[c];
            }
            else {
                GameObject drawnCard = Instantiate(FlippedCardPrefab,new Vector3(transform.position.x+((c+1)*0.8f),transform.position.y,0),Quaternion.identity,transform);
                drawnCard.transform.localScale=Vector3.one * 0.5f;
            }
        }
    }

    void DealerHit() {
        dealerHand.Add(deck[deck.Count-1]);
        deck.RemoveAt(deck.Count - 1);
        DrawHand();
        if (dealerHand[dealerHand.Count - 1].value == 1 && dealerTotal + 11 <= 21) dealerHand[dealerHand.Count - 1].value = 11;
        dealerTotal += dealerHand[dealerHand.Count-1].value;
        if (dealerTotal > 21) {
            for (int c = 0; c < dealerHand.Count; c++)
            {
                if (dealerHand[c].value == 11) {dealerHand[c].value = 1; dealerTotal-=10;}
                
                if (dealerTotal <= 21) return;
            }
        }
    }

    public void NextPlayer()
    {
        players[currentPlayer].yourTurn=false;
        if (playersLeft) {
            if (currentPlayer+1 == players.Count) {
                currentPlayer=0;
                if (dealerTotal < 17) DealerHit();
            }
            else currentPlayer++;
            players[currentPlayer].yourTurn=true;
        }
    }
}
