using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public Image[] IndicatorSaveCard;//Индикатор пропуска хода
    public float TimerBegin;//Время таймеров
    private float Timer;//Таймер пропуска хода
    private float PlusTimer;//Таймер взятия карты
    private bool goSave = false;//начало таймера пропуска хода
    private GameManagerScript game;//Игровой менеджер
    private SoundScript Sound; //Звуки
    public Text EndText;//Текст победы или поражения
    public GameObject End;//Меню окончания игры
    public GameObject Pause;//Меню паузы
    private bool PauseMenuOpen = false;//Включено ли меню паузы
    public GameObject Setting;//Меню настроек
    public Slider MusicSound;//Настрока громкости музыки
    public Slider SoundSound;//Настройка громкости звуков
    public Slider EnvironmentSound;//Настройка громкости звуков окружения
    private float MusicVolume = 0;//Громкость музыки
    private float SoundVolume = 0;//Громкость звуков
    private float EnvironmentVolume = 0;//Громкость окружения
    private bool endmatch = false;//Окончился ли матч
    public AudioClip Click;//Воспроизведение звука на нажатие кнопки
    private bool setmenu;//Включено ли меню настроек
    private bool openpausemenu = false; //Открыто ли меню паузы
    [SerializeField] private int sound; //Множитель громкости звука окружения
    private int win = 2;
    private bool end = false;
    void Start()
    {
        setmenu = false;

        game = FindObjectOfType<GameManagerScript>();

        Sound = FindObjectOfType<SoundScript>();

        for(int i = 0; i< IndicatorSaveCard.Length;i++)
        {
            IndicatorSaveCard[i].enabled = false;
        }
        Timer = TimerBegin;
        PlusTimer = TimerBegin;

        for(int i = 0; i< game.PlusText.Length; i++)
        {
            game.PlusText[i].text = "";
        }

        MusicSound.value = MusicVolume;
        SoundSound.value = SoundVolume;
        EnvironmentSound.value = EnvironmentVolume * sound;

    }

    private void Awake()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicSound");
        SoundVolume = PlayerPrefs.GetFloat("SoundSound");
        EnvironmentVolume = PlayerPrefs.GetFloat("EnvironmentSound");
    }

    void Update()
    {
        SaveCardInd();

        EndMatch();

        SoundSetting();

        PauseMenu();
    }

    private void FixedUpdate()
    {
        PlusCardIndicator();

        if(endmatch == true)
        {
            if (end == false)
            {
                if (win == 1 || win == 0)
                {
                    Sound.EnvironmentSound.clip = Sound.WinandLose[win];
                    if (win == 0)
                    {
                        if (Sound.EnvironmentSound.volume != 0)
                        {
                            Sound.EnvironmentSound.volume = 1;
                        }
                    }
                    Sound.EnvironmentSound.Play();
                }
            }
            end = true;
        }
    }

    private void PlusCardIndicator()//Отображение получения карт у игроков
    {
        if (game.plusCard[0] > 0 || game.plusCard[1] > 0 || game.plusCard[2] > 0 || game.plusCard[3] > 0)
        {
            PlusTimer -= Time.fixedDeltaTime;
            if (PlusTimer <= 0)
            {
                for (int i = 0; i < game.PlusText.Length; i++)
                {
                    game.PlusText[i].text = "";
                    game.plusCard[i] = 0;
                }
                PlusTimer = TimerBegin;
                Sound.GiveCardSound = true;
            }
        }
    }

    private void SaveCardInd() //Отображение пропуска хода
    {
        for (int i = 0; i < IndicatorSaveCard.Length; i++)
        {
            if (goSave == false)
            {
                if (i == game.SaveCard)
                {
                    IndicatorSaveCard[i].enabled = true;
                    goSave = true;
                }
                else
                {
                    IndicatorSaveCard[i].enabled = false;
                }
            }
        }
        if (goSave == true)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                game.SaveCard = 4;
                goSave = false;
                Timer = TimerBegin;
            }
        }
    }

    private void EndMatch()//Меню окончания матча
    {
        if(game.CurrentGame.FirstEnemyHand.Count == 0 || game.CurrentGame.SecondEnemyHand.Count == 0 ||
            game.CurrentGame.ThirdEnemyHand.Count == 0|| game.CurrentGame.PlayerHand.Count == 0)
        {
            game.go = false;
            endmatch = true;
            End.SetActive(true);
            if(game.CurrentGame.PlayerHand.Count == 0)
            {
                EndText.text = "Победа!!!";
                win = 0;
            }
            else
            {
                EndText.text = " Поражение!!!";
                win = 1;
            }
        }
    }

    private void PauseMenu()//Реализация меню паузы
    {
        if (endmatch == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenuOpen = !PauseMenuOpen;
            }

            if (PauseMenuOpen == true)
            {
                game.go = false;
                Pause.SetActive(true);
                if(setmenu == true && openpausemenu == false)
                {
                    setmenu = false;
                    openpausemenu = true;
                }
            }
            else
            {
                game.go = true;
                Pause.SetActive(false);
                Setting.SetActive(false);
                openpausemenu = false;
            }
        }
    }

    public void SettingMenu()//Включение меню настроек
    {
        if (setmenu == false)
        {
            Setting.SetActive(true);
            setmenu = true;
        }
        else
        {
            Setting.SetActive(false);
            setmenu = false;
        }
        Sound.SoundCard.clip = Click;
        Sound.SoundCard.Play();
    }

    public void MusicAction( float val)//Настрока Громкости Музыки
    {
        PlayerPrefs.SetFloat("MusicSound", val);
    }

    public void SoundAction( float val)//Настройка громкости звуков
    {
        PlayerPrefs.SetFloat("SoundSound", val);
    }

    public void EnvironmentAction( float val)//Настройка громкости окружения
    {
        PlayerPrefs.SetFloat("EnvironmentSound", val/sound);
    }

    private void SoundSetting()//Просчитывание настроек
    {
        Sound.Music.volume = PlayerPrefs.GetFloat("MusicSound");
        Sound.SoundCard.volume = PlayerPrefs.GetFloat("SoundSound");
        Sound.EnvironmentSound.volume = PlayerPrefs.GetFloat("EnvironmentSound");
    }

    public void GrandMenu()//Возвращение в главное меню
    {
        SceneManager.LoadScene(0);
    }
}
