using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject DrawCardPrefab;
    public List<Card> playerHand = new List<Card>();
    public int total;
    CardManager dealer;
    public TMPro.TextMeshProUGUI statusText;
    public TMPro.TextMeshProUGUI totalText;
    public bool stand = false;
    public bool lost = false;
    public bool won = false;
    public bool yourTurn=false;

    public float thinkDelay;
    private void Start()
    {
        dealer = GameObject.FindGameObjectWithTag("Card Manager").GetComponent<CardManager>();
    }

    private void Update() {
        totalText.text=total.ToString();
        if (lost) statusText.text="BUST!";
        else if (won) statusText.text="WINNER!";
        else if (stand) statusText.text="STAND";

        if (yourTurn) {
            if (lost || stand) dealer.Invoke("NextPlayer",dealer.turnDelay);
            else {
                switch (tag) {
                    case "Player":
                        ProcessPlayer();
                        break;
                    default:
                        ProcessComputer();
                        break;
                }
            }
            if (!yourTurn) {
                dealer.Invoke("NextPlayer",dealer.turnDelay);
            }
        }
    }

    private void ProcessPlayer()
    {
        if (playerHand.Count<2) {
                Hit();
                yourTurn=false;
        } else {
        
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Hit();
            }
            else if (Input.GetKeyDown(KeyCode.Backspace)) {
                stand=true;
            }
            
        }
    }

    private void ProcessComputer() 
    {
        if (playerHand.Count<2) {
                Hit();
                yourTurn=false;
        } else {
            if (total <= 16) {
                Hit();
            } else {
                stand=true;
            }
        }
    }

    void Hit()
    {
        playerHand.Add(dealer.deck[dealer.deck.Count-1]);
        dealer.deck.RemoveAt(dealer.deck.Count - 1);
        DrawHand();
        if (playerHand[playerHand.Count - 1].value == 1 && total + 11 <= 21) playerHand[playerHand.Count - 1].value = 11;
        total +=playerHand[playerHand.Count-1].value;
        if (total > 21) {
            for (int c = 0; c < playerHand.Count; c++)
            {
                if (playerHand[c].value == 11) {playerHand[c].value = 1; total-=10;}
                
                if (total <= 21) return;
            }
            lost = true;
        }
        
        
    }

    void DrawHand() {
        foreach (RenderCard c in GetComponentsInChildren<RenderCard>()) {
            Destroy(c.gameObject);
        }
        for (int c = 0; c < playerHand.Count; c++)
        {
            GameObject drawnCard = Instantiate(DrawCardPrefab,new Vector3(transform.position.x+((c+1)*0.8f),transform.position.y,0),Quaternion.identity,transform);
            drawnCard.GetComponent<RenderCard>().card = playerHand[c];
        }
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
