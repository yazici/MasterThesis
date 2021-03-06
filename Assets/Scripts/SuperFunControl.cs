﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using Kino;
using UnityEngine.UI;
public class SuperFunControl : MonoBehaviour
{
   
    public UnityEvent TeleportToDepriWorld;
    public GameObject PlayerPrefab;
    
    public Camera PlayerCam;

    public AudioSource MonkeyMusic;


    public PostProcessVolume Post;
    public PostProcessProfile HappyPost;
    public PostProcessProfile FinalHappyPost;

     public PostProcessProfile BonusHappyPost;
    public PostProcessProfile DepriPost;

    public Material DepriSkybox;
    public Material HappySkybox;

    public Canvas UI;

    public AudioClip[] Music;

    public AudioClip[] glitch;

    public AudioSource MusicPlayer;

    public GameObject CartoonTree;
    public GameObject GhostLog;

    public GameObject Rainbow;

   

    private bool StartDigitalGlitch = false;
     private bool GrowRainbow = false;
  
    private float DigitalglitchSpeed;

    float RainbowSize = 0.1f;
    


    // Start is called before the first frame update
    void OnEnable()
    {
      //Post.profile=DepriPost;
      SetupFunworld();
      if(MonkeyMusic.isPlaying){
         MonkeyMusic.Stop();
      }
       if(GameStateManager.TimesInSuperFunWorld==1){

            StartCoroutine(StuffThatHappensBeforeReturningFirstTime(Random.Range(2,10),1));
              
              MusicPlayer.clip=Music[0];
              MusicPlayer.Play();
              DigitalglitchSpeed = 0.1f;
              Rainbow.gameObject.SetActive(false);
              GhostLog.gameObject.SetActive(false);
            
              
        }
         if(GameStateManager.TimesInSuperFunWorld==2){
              StartCoroutine(StuffThatHappensBeforeReturningSecondTime(Random.Range(3,15),3));
               MusicPlayer.clip=Music[1];
              MusicPlayer.Play();
               DigitalglitchSpeed = 0.3f;
               Rainbow.gameObject.SetActive(true);
               GhostLog.gameObject.SetActive(false);
               GrowRainbow = true;
        }
         if(GameStateManager.TimesInSuperFunWorld==3){
              StartCoroutine(StuffThatHappensBeforeReturningThirdTime(Random.Range(3,20),10));
               MusicPlayer.clip=Music[2];
              MusicPlayer.Play();
               DigitalglitchSpeed = 0.4f;
               StartCoroutine(BlinkingTree());
               Rainbow.gameObject.SetActive(false);
        }
         if(GameStateManager.TimesInSuperFunWorld==4){
              StartCoroutine(StuffThatHappensBeforeReturningFourthTime(Random.Range(1,7),5));
               MusicPlayer.clip=Music[0];
              MusicPlayer.Play();
               DigitalglitchSpeed = 0.1f;
               Rainbow.gameObject.SetActive(true);
               CartoonTree.gameObject.SetActive(false);
                  GhostLog.gameObject.SetActive(true);
                  PlayerPrefab.transform.localScale = new Vector3(2,2,2);

        }
    }

    private void ReturnToOtherWorld(){
        
    //set up the graphics for depri world
        MusicPlayer.Stop();
        Post.profile=DepriPost;
       RenderSettings.skybox=DepriSkybox;
       PlayerCam.GetComponent<Tube>().enabled=true;
       PlayerCam.GetComponent<AnalogGlitch>().enabled=false;
       UI.gameObject.SetActive(true);
         PlayerCam.GetComponent<DigitalGlitch>().intensity = 0.0f;
         PlayerCam.GetComponent<DigitalGlitch>().enabled=false;
         StartDigitalGlitch = false;
         if(GameStateManager.TimesInSuperFunWorld==4){
         PlayerPrefab.transform.localScale = new Vector3(1,1,1);
         }

       //call teleport funtion from teleport script
       TeleportToDepriWorld.Invoke();        
    }
    void Update(){
        if(StartDigitalGlitch){
            PlayerCam.GetComponent<DigitalGlitch>().intensity +=DigitalglitchSpeed*Time.deltaTime;
        }

        if(GrowRainbow){
           Rainbow.transform.localScale = new Vector3(1+RainbowSize, 1+RainbowSize, 1+RainbowSize);
           RainbowSize +=  0.2f * Time.deltaTime;
        }
    }


      IEnumerator StuffThatHappensBeforeReturningFirstTime(int TimeBeforeGlitches, int timeToReturn){
        yield return new WaitForSeconds(TimeBeforeGlitches); 
        //start glitches and music before leaving
   
        MusicPlayer.clip=glitch[0];
        MusicPlayer.Play();
        PlayerCam.GetComponent<AnalogGlitch>().enabled=true;
        PlayerCam.GetComponent<AnalogGlitch>().verticalJump=0.1f;
        PlayerCam.GetComponent<AnalogGlitch>().scanLineJitter=0.2f;
        PlayerCam.GetComponent<AnalogGlitch>().horizontalShake=0.2f;
        PlayerCam.GetComponent<AnalogGlitch>().colorDrift=0.1f;
         PlayerCam.GetComponent<DigitalGlitch>().enabled=true;
         StartDigitalGlitch = true;
        
        yield return new WaitForSeconds(timeToReturn);
        //call leave function
        Debug.Log("NO! I want to stay. Don't make me leave! Please!");

       ReturnToOtherWorld();    

    }
       IEnumerator StuffThatHappensBeforeReturningSecondTime(int TimeBeforeGlitches, int timeToReturn){
        yield return new WaitForSeconds(TimeBeforeGlitches); 
        //start glitches and music before leaving
           MusicPlayer.clip=glitch[1];
        MusicPlayer.Play();
         PlayerCam.GetComponent<AnalogGlitch>().enabled=true;
        PlayerCam.GetComponent<AnalogGlitch>().verticalJump=0.3f;
        PlayerCam.GetComponent<AnalogGlitch>().scanLineJitter=0.5f;
        PlayerCam.GetComponent<AnalogGlitch>().horizontalShake=0.5f;
        PlayerCam.GetComponent<AnalogGlitch>().colorDrift=0.7f;
         PlayerCam.GetComponent<DigitalGlitch>().enabled=true;
         StartDigitalGlitch = true;

        yield return new WaitForSeconds(timeToReturn);  
        //call leave function
      GrowRainbow=false;
       ReturnToOtherWorld();    

    }
    IEnumerator StuffThatHappensBeforeReturningThirdTime(int TimeBeforeGlitches, int timeToReturn)
    {
        yield return new WaitForSeconds(TimeBeforeGlitches);
        //start glitches and music before leaving
        MusicPlayer.clip = glitch[2];
        MusicPlayer.Play();
        PlayerCam.GetComponent<AnalogGlitch>().enabled = true;
        PlayerCam.GetComponent<AnalogGlitch>().verticalJump = 0.3f;
        PlayerCam.GetComponent<AnalogGlitch>().scanLineJitter = 0.9f;
        PlayerCam.GetComponent<AnalogGlitch>().horizontalShake = 0.7f;
        PlayerCam.GetComponent<AnalogGlitch>().colorDrift = 1f;
        PlayerCam.GetComponent<DigitalGlitch>().enabled = true;
        StartDigitalGlitch = true;
        PlayerCam.GetComponent<AnalogGlitch>().enabled = true;
        yield return new WaitForSeconds(timeToReturn);
        //call leave function
        ReturnToOtherWorld();

        Debug.Log("NO!Ic3 06 1e b3 71  to &\\!!?.....0c 41 2d fe d2..? ++1FE LEAVE!");
        

    }

    IEnumerator StuffThatHappensBeforeReturningFourthTime(int TimeBeforeGlitches, int timeToReturn){
        yield return new WaitForSeconds(TimeBeforeGlitches); 
        //start glitches and music before leaving
           MusicPlayer.clip=glitch[1];
        MusicPlayer.Play();
          PlayerCam.GetComponent<AnalogGlitch>().enabled=true;
        PlayerCam.GetComponent<AnalogGlitch>().verticalJump=0.2f;
        PlayerCam.GetComponent<AnalogGlitch>().scanLineJitter=0.2f;
        PlayerCam.GetComponent<AnalogGlitch>().horizontalShake=0.2f;
        PlayerCam.GetComponent<AnalogGlitch>().colorDrift=0.3f;
         PlayerCam.GetComponent<DigitalGlitch>().enabled=true;
      StartDigitalGlitch = true;
        PlayerCam.GetComponent<AnalogGlitch>().enabled=true;
        yield return new WaitForSeconds(timeToReturn);  
        //call leave function
       ReturnToOtherWorld();

        Debug.Log("NO! Ic3 06 NEVER LEAVE!");

    }

    void SetupFunworld(){
        //set up basic graphic for fun world

        switch(GameStateManager.TimesInSuperFunWorld){
           case 1:
           Post.profile=HappyPost;
           break;
            case 2:
           Post.profile=HappyPost;
           break;
            case 3:
            Post.profile=FinalHappyPost;
  
           break;
               case 4:
            Post.profile=BonusHappyPost;
           
           break;

        }
       RenderSettings.skybox=HappySkybox;
       PlayerCam.GetComponent<Tube>().enabled=false;
       UI.gameObject.SetActive(false);

    }
    IEnumerator BlinkingTree(){

       for(int i = 0; i < 30; i++){
        yield return new WaitForSeconds(Random.Range(0.05f,0.5f));  
        CartoonTree.gameObject.SetActive(false);
        GhostLog.gameObject.SetActive(true);

         yield return new WaitForSeconds(Random.Range(0.05f,0.5f));  
          CartoonTree.gameObject.SetActive(true);
        GhostLog.gameObject.SetActive(false);
       }
    }

}
