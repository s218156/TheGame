using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.SoundControllers
{
    public class CoinSoundController
    {
        private Song sound;
        public CoinSoundController(Song sound)
        {
            this.sound = sound;
        }
        public void PlaySound()
        {
            MediaPlayer.Play(sound);
        }
    }
}
