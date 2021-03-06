﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicControl : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource RoomTone;
    public AudioSource EerieMusic;

    public AudioSource PreDeathMusic;

    public AudioSource Gong;
    public bool preloadAudioData; 

    public AudioSource DigitalGlitch;

     public AudioSource AngelSynth;

     public AudioSource EndGameMusic;


     public AudioClip[] DatamoshAudio;

     private bool FunWorldPauseMusic = true;

    private float RoomToneVolume;
    private float EerieMusicVolume;
    private float AngelSynthVolume;
    void Start()
    {
        PlayRoomtone(20);  
        RoomToneVolume = RoomTone.volume;
        EerieMusicVolume = EerieMusic.volume;
        AngelSynthVolume = AngelSynth.volume;

    }

    public void TurnDownMusic(){
            RoomTone.volume=0.0f;
            AngelSynth.volume=0.0f;
            EerieMusic.volume=0.0f;

    }
     public void StopMusic(){
            RoomTone.Stop();
            AngelSynth.Stop();
            EerieMusic.Stop();

    }
    public void TurnUpMusic(){
            RoomTone.volume=0.7f;
            AngelSynth.volume=0.1f;
            EerieMusic.volume=0.1f;
    }
   

    public void PlayRoomtone(int FadeTime){
    RoomTone.Play();
    StartCoroutine(FadeUp(RoomTone,FadeTime)); 
    }

    public void playEndGameMusic(int FadeTime){
    EndGameMusic.Play();
    StartCoroutine(FadeUp(EndGameMusic,FadeTime)); 

    }
    public void PlayEerieMusic(int FadeTime){
    EerieMusic.Play();
    StartCoroutine(FadeUp(EerieMusic,FadeTime)); 
    }
  public void PlayPreDeathMusic(int FadeTime){
    PreDeathMusic.Play();
    StartCoroutine(FadeUp(PreDeathMusic,FadeTime)); 
    }

public void PlayAngelSynth(int FadeTime){
    AngelSynth.Play();
    StartCoroutine(FadeUp(AngelSynth,FadeTime)); 
    }

    public void PlayGong(){
        Gong.pitch=Random.Range(0.8f,1.2f);
         Gong.Play();
    }

    public void PlayDigitalGlitchSound(int FadeTime){
    int RandomGlitchsound = Random.Range(0,DatamoshAudio.Length);
            DigitalGlitch.clip=DatamoshAudio[RandomGlitchsound];
            DigitalGlitch.pitch=Random.Range(0.3f,1.5f);
            DigitalGlitch.time=Random.Range(0.3f,3.5f);
            DigitalGlitch.Play();
            StartCoroutine(FadeUp(DigitalGlitch,FadeTime)); 
    }

    public void FadeOutErieMusic(int FadeTime){
            StartCoroutine(FadeOut(EerieMusic,FadeTime));  
    }
     public void FadeOutPreDeathTime(int FadeTime){
            StartCoroutine(FadeOut(PreDeathMusic,FadeTime));  
    }
    public void FadeOutAngelSynth(int FadeTime){
            StartCoroutine(FadeOut(AngelSynth,FadeTime));  
    }
    public void FadeOutRoomTone(int FadeTime){
            StartCoroutine(FadeOut(RoomTone,FadeTime));  
    }
     public void FadeOutDigitalGlitch(int FadeTime){
            StartCoroutine(FadeOut(DigitalGlitch,FadeTime));  
    }

    public void FadeOutEndGameMusic(int FadeTime){
            StartCoroutine(FadeOut(EndGameMusic,FadeTime));  
    }

   
 private IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }

    
 private IEnumerator FadeUp (AudioSource audioSource, float FadeTime) {
    float startVolume = audioSource.volume;
       audioSource.volume = 0;
 
        while (audioSource.volume < startVolume) {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
       
        audioSource.volume = startVolume;
    }
 

  
}