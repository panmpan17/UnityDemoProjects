#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MPack
{
    [System.Serializable]
    public class OneShotLoopPlayer
    {
        [SerializeField]
        private AudioIDEnum Type;
        [SerializeField]
        private Timer Interval;
        // [SerializeField]
        // private bool LoopAfterSoundFinished;
        public float Volume = 1;

        [System.NonSerialized]
        public Vector3 Position;
        private bool usePosition;

        public System.Action ForceStopDelegate;

        private bool isPlaying = false;
        public bool IsPlaying {
            get {
                return isPlaying;
            }
        }

        public void Play()
        {
            isPlaying = true;
            Interval.Reset();
            usePosition = false;

            // AudioMgr.ins.PlayOneShot(Type, Volume);
            // AudioMgr.ins.PlayLoop(this);
        }

        public void Play(Vector3 position)
        {
            isPlaying = true;
            Interval.Reset();
            usePosition = true;
            Position = position;

            // AudioOneShotPlayer player = AudioMgr.ins.PlayOneShotAtPosition(Type, position, Volume);
            // if (player != null)
                // player.RegisterForceStop(this);
            // AudioMgr.ins.PlayLoop(this);
        }

        public void Stop()
        {
            isPlaying = false;
            // AudioMgr.ins.StopLoop(this);

            ForceStopDelegate?.Invoke();
            ForceStopDelegate = null;
            // if (player != null) {
            //     player.Stop();
            //     player = null;
            // }
        }

        public void Update()
        {
            if (Interval.UpdateEnd)
            {
                Interval.Reset();
                if (usePosition)
                {
                    // AudioOneShotPlayer player = AudioMgr.ins.PlayOneShotAtPosition(Type, Position, Volume);
                    // if (player != null)
                        // player.RegisterForceStop(this);
                }
                // else
                    // AudioMgr.ins.PlayOneShot(Type, Volume);
            }
        }
    }
}