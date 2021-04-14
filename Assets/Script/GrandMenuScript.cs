using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrandMenuScript : MonoBehaviour
{
    private Animator anima;// Аниматор двери
    public GameObject[] Menu; //Экраны меню
    public Slider MusicSlider;// Слайдер изменения громкости музыки
    public Slider SoundSlider;// Слайдер изменения громкости звуков
    public Slider EnvironmentSlider;//Слайдер изменения громкости окружения
    private float MusicVolume = 0;// Громкость музыки
    private float SoundVolume = 0;// Громкость звуков
    private float EnvironmentVolume = 0; //Громкость окружения
    public AudioSource Music; //Музыка
    public AudioSource SoundCard; //Звуки карт и битвы
    public AudioSource Environment; //звуки окружения
    public Material BarMat; //Материал вывески бара
    
    void Start()
    {
        anima = GetComponent<Animator>();
        BarMat.DisableKeyword("_EMISSION");
        MusicSlider.value = MusicVolume;
        SoundSlider.value = SoundVolume;
        EnvironmentSlider.value = EnvironmentVolume * 3;
    }

    private void Awake()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicSound");
        SoundVolume = PlayerPrefs.GetFloat("SoundSound");
        EnvironmentVolume = PlayerPrefs.GetFloat("EnvironmentSound");
    }

    void Update()
    {
        SoundSetting();
    }

    public void OpenDoor()//Открытие двери
    {
        anima.SetTrigger("OpenDoor");
        SoundCard.Play();
    }

    public void BeginGame() // Начало игры
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()//Выход из игры
    {
        Application.Quit();
    }

    public void Author() //Открытие меню об авторах
    {
        Debug.Log("Author");
        Menu[0].SetActive(false);
        Menu[1].SetActive(true);
        SoundCard.Play();
    }

    public void Setting() // Открытие меню настроек
    {
        Menu[0].SetActive(false);
        Menu[3].SetActive(true);
        SoundCard.Play();
    }

    public void Rules()// Открытие меню правил
    {
        Menu[0].SetActive(false);
        Menu[2].SetActive(true);
        SoundCard.Play();
    }

    public void Collection()//Открытие меню коллекции карт
    {
        Menu[0].SetActive(false);
        Menu[4].SetActive(true);
        SoundCard.Play();
    }

    public void BackMenu()//Возвращение на главное меню
    {
        for(int i = 1;i<Menu.Length;i++)
        {
            Menu[i].SetActive(false);
        }
        Menu[0].SetActive(true);
        SoundCard.Play();
    }

    private void SoundSetting()//Просчитывание настроек
    {
        Music.volume = PlayerPrefs.GetFloat("MusicSound");
        SoundCard.volume = PlayerPrefs.GetFloat("SoundSound");
        Environment.volume = PlayerPrefs.GetFloat("EnvironmentSound");
    }

    public void MusicAction(float val)//Настрока Громкости Музыки
    {
        PlayerPrefs.SetFloat("MusicSound", val);
    }

    public void SoundAction(float val)//Настройка громкости звуков
    {
        PlayerPrefs.SetFloat("SoundSound", val);
    }

    public void EnvironmentAction(float val)//Настройка громкости окружения
    {
        PlayerPrefs.SetFloat("EnvironmentSound", val / 3);
    }

    public void OnPoint() //Включение вывески бар при наведении на дверь
    {
        BarMat.EnableKeyword("_EMISSION");
    }

    public void OffPoint()//Выключение вывески бар при отведения курсора от двери
    {
        BarMat.DisableKeyword("_EMISSION");
    }

    public void Open()//Воспроизведение звука двери
    {
        Environment.Play();
    }
}
