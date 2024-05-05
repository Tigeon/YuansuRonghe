using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public GameObject menu;
    [SerializeField] Slider volumeSlider;
    [SerializeField] float defaultVolumeValue = 0.5f;
    MusicPlayer[] MusicPlayers;
    // Start is called before the first frame update
    void Start()
    {
        if (menu != null){
            menu = GameObject.Find("Menu");
            if (menu == null){
                Debug.LogError("Menu not found");
            }
        }
        volumeSlider.value = MasterControl.GetMaster_Volume();
        MusicPlayers = FindObjectsOfType<MusicPlayer>();
        menu.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape")){
            if(!menu.activeSelf){
                menu.gameObject.SetActive(true);
            }else{
                HideMenu();
            }
        }

        if (menu.activeSelf){
            Time.timeScale = 0f;
            MasterControl.SetMaster_Volume( volumeSlider.value );
            foreach(MusicPlayer MusicPlayer in MusicPlayers){
                MusicPlayer.SetVolume( MasterControl.GetMaster_Volume() );
            }
        }
    }

    public void HideMenu(){
        menu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SetDefault(){
        volumeSlider.value = defaultVolumeValue;
    }
}
