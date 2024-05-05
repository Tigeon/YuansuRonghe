using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterControl : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master_volume";
    const string MASTER_RANDOM_KEY = "master_random";
    const string Map_FirstRowOffsetKEY = "Map_FirstRowOffset";

    const float MASTER_VOLUME_MIN = 0;
    const float MASTER_VOLUME_MAX = 1;

    public const float GridWidth = 1.5f;

    public const int WorldMapMaxTryTimes = 99;
    public const int Infinity = 999999;

    public const int Map_HexRadius = 100;

    public const string Tag_BattleCamera = "BattleCamera";

    public static WaitForSeconds WaitForOneSecond = new WaitForSeconds(1f);
    public static WaitForSeconds WaitForHalfSecond = new WaitForSeconds(0.5f);
    public static WaitForSeconds WaitForDotOneSecond = new WaitForSeconds(0.1f);


    public static void SetMaster_Volume( float i )
    {
        if( i >= MASTER_VOLUME_MIN && i <= MASTER_VOLUME_MAX ){
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, i);
        }else{
            Debug.Log("Float out of range");
        }
    }

    public static void SetMaster_Random( float i )
    {
        PlayerPrefs.SetFloat(MASTER_RANDOM_KEY, i);
    }

    public static void SetMap_FirstRowOffset( int FirstRowOffset ){
        if ( FirstRowOffset%1==0 )
            PlayerPrefs.SetInt(Map_FirstRowOffsetKEY, 0);
        else
            PlayerPrefs.SetInt(Map_FirstRowOffsetKEY, 1);

    }

    public static bool Map_FirstRowOffset(){
        if(PlayerPrefs.GetInt(Map_FirstRowOffsetKEY)==0)
            return false;
        else
            return true;
    }

    public static float GetMaster_Volume(){ return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY) ;}
    public static float GetMaster_Random(){ return PlayerPrefs.GetFloat(MASTER_RANDOM_KEY) ;}
}
