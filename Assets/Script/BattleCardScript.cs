using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCardScript : MonoBehaviour
{
    public int Race; //Раса карты сброса
    public int Specialization;// Специализация карты сброса (лучник, воин, маг, ассассин, демиург или некромант)
    public int ForceCard; //Сила карты
    public Image BattleImage;//Картинка карты
    public Card SelfCard;//Все данные карты
    public Text Name;// Имя персонажа
    public Image Icon; //Значок специализации
    public Image Number; //Значок силы
    public Sprite[] AllNumber; //Все виды чисел
    public Sprite[] AllIcon; //Все виды значков саециализации

    private void ShowCardInfo(Card card)//Получение карты при старте игры
    {
        SelfCard = card;
        Race = card.Race;
        Specialization = card.Specialization;
        ForceCard = card.ForceCard;
        BattleImage.sprite = card.Logo;
        Name.text = card.Name;

    }

    private void Start()
    {
        BattleImage = GetComponent<Image>();
        CardManagerScript cardMan = FindObjectOfType<CardManagerScript>();
        ShowCardInfo(CardManager.AllCards[Random.Range(0, cardMan.CardVariation.Length - 20)]);
        InstallCardIconAndNumber();
        Debug.Log($" Раса {Race} Сила {ForceCard} Специализация {Specialization} картинка {BattleImage.sprite.name}");
    }

    public void InstallCardIconAndNumber()//Присваивание значкам специализации и силы значение
    {
        for(int i = 0; i< AllNumber.Length;i++)
        {
            if(i == ForceCard - 1 && Race < 4)
            {
                Number.sprite = AllNumber[i];
            }
            else if (Race > 3)
            {
                Number.sprite = AllNumber[AllNumber.Length - 1];
            }
        }
        
        for(int i = 0; i< AllIcon.Length; i++)
        {
            if(i == Specialization - 1 && Race<4)
            {
                Icon.sprite = AllIcon[i];
            }
            else if(  Race == 4 ) 
            {
                Icon.sprite = AllIcon[AllIcon.Length - 2];
            }
            else if(Race == 5)
            {
                Icon.sprite = AllIcon[AllIcon.Length - 1];
            }
        }
    }

    public void ColorPlayer( int numberPlayer)//Изменение значков под цвет игрока
    {
        switch(numberPlayer)
        {
            case 0: Icon.color = Color.cyan; Number.color = Color.cyan; break;
            case 1: Icon.color = Color.yellow; Number.color = Color.yellow; break;
            case 2: Icon.color = Color.green; Number.color = Color.green; break;
            case 3: Icon.color = Color.magenta; Number.color = Color.magenta; break;
        }
    }
}
