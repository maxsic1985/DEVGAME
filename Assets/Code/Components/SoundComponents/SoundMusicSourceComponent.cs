﻿using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public struct SoundMusicSourceComponent
    {
        public AudioSource Source;
        public AudioClip[] Tracks;
        public int PlayedTrack;
        
        public  int FirstTrackNumber;
    }
}