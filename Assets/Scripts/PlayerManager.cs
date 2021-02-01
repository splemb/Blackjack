using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Canvas buttons;

    public float thinkDelay;
    public float nextThink=0f;
    private void Start()
    {
        dealer = GameObject.FindGameObjectWithTag("Card Manager").GetComponent<CardManager>();
        buttons.enabled=false;
    }

    private void Update() {
        totalText.text=total.ToString();
        if (lost) statusText.text="BUST!";
        else if (won) {
            statusText.text="WINNER!";
        }
        else if (stand) statusText.text="STAND";
        else statusText.text="";

        switch (tag) {
                    case "Player":
                        ProcessPlayer();
                        break;
                    default:
                        ProcessComputer();
                        break;
                }
    }

    private void ProcessPlayer()
    {
        if (yourTurn) {
            if (lost || stand) {yourTurn = false; buttons.enabled = false;}
            else {
                if (playerHand.Count<2) {
                    Hit();
                    yourTurn=false;
                } else {
                    buttons.enabled = true;
                }
            }
            if (!yourTurn) {
                dealer.Invoke("NextPlayer",dealer.turnDelay);
                buttons.enabled = false;
            }
        }

        
    }

    private void ProcessComputer() 
    {
        if (yourTurn) {
            buttons.enabled = false;
            if (lost || stand) {yourTurn = false; buttons.enabled = false;}
            else {
                if (playerHand.Count<2) {
                    Hit();
                    yourTurn=false;
                }
                else {
                    if (Time.time > nextThink) {
                        nextThink+=Random.Range(1,5)*thinkDelay;
                        if (total <= 16) {
                            Hit();
                        } else {
                            stand=true;
                        }
                    }
                }
            }
            if (!yourTurn) {
                dealer.Invoke("NextPlayer",dealer.turnDelay);
                buttons.enabled = false;
            }
        }
    }

    public void Hit()
    {
        dealer.theSound.Play();
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

    public void Stand()
    {
        stand=true;
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
