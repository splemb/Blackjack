using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card")]
public class Card : ScriptableObject
{
    private int value;
    private char suit;
    private char rank;
    public new string name;

    public Card(int inputValue, char inputSuit, char inputRank)
    {
        value = inputValue;
        suit = inputSuit;
        rank = inputRank;
        GenerateName();
    }

    void GenerateName()
    {
        switch (rank)
        {
            case 'A':
                name = "Ace";
                break;
            case '2':
                name = "Two";
                break;
            case '3':
                name = "Three";
                break;
            case '4':
                name = "Four";
                break;
            case '5':
                name = "Five";
                break;
            case '6':
                name = "Six";
                break;
            case '7':
                name = "Seven";
                break;
            case '8':
                name = "Eight";
                break;
            case '9':
                name = "Nine";
                break;
            case '0':
                name = "Ten";
                break;
            case 'J':
                name = "Jack";
                break;
            case 'Q':
                name = "Queen";
                break;
            case 'K':
                name = "King";
                break;
        }

        name = name + (" of ");

        switch (suit)
        {
            case 'D':
                name = name + "Diamonds";
                break;
            case 'H':
                name = name + "Hearts";
                break;
            case 'C':
                name = name + "Clubs";
                break;
            case 'S':
                name = name + "Spades";
                break;
        }
    }
}
