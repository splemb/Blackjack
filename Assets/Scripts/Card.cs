using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card")]
public class Card : ScriptableObject
{
    public int value;
    public char suit;
    public char rank;
    public new string name;

    public Card(char inputSuit, char inputRank)
    {
        suit = inputSuit;
        rank = inputRank;
        GenerateAttributes();
    }

    void GenerateAttributes()
    {
        switch (rank)
        {
            case 'A':
                name = "Ace";
                value = 1;
                break;
            case '2':
                name = "Two";
                value = 2;
                break;
            case '3':
                name = "Three";
                value = 3;
                break;
            case '4':
                name = "Four";
                value = 4;
                break;
            case '5':
                name = "Five";
                value = 5;
                break;
            case '6':
                name = "Six";
                value = 6;
                break;
            case '7':
                name = "Seven";
                value = 7;
                break;
            case '8':
                name = "Eight";
                value = 8;
                break;
            case '9':
                name = "Nine";
                value = 9;
                break;
            case '0':
                name = "Ten";
                value = 10;
                break;
            case 'J':
                name = "Jack";
                value = 10;
                break;
            case 'Q':
                name = "Queen";
                value = 10;
                break;
            case 'K':
                name = "King";
                value = 10;
                break;
        }

        name = name + (" of ");

        switch (suit)
        {
            case '0':
                name = name + "Diamonds";
                break;
            case '1':
                name = name + "Hearts";
                break;
            case '2':
                name = name + "Clubs";
                break;
            case '3':
                name = name + "Spades";
                break;
        }
    }
}
