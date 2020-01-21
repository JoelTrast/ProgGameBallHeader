using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BallHeader
{
    class Ball : PhysicalObject
    {
        float elaps;
        float Ac=-4f;
        float ballMid;
        float playerMid;

        public Ball(Texture2D texture, float X, float Y, float speedX, float speedY):base(texture, X, Y, speedX, speedY)
        {
            
        }

        public void Update(GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();            
            
            vector.Y += speed.Y;
            vector.X += speed.X;
            
            //gravitiy function
            speed.Y += 0.15f;
                
            //fönster kollision
            if (vector.X < 0)
            {
                //vector.X = 0;
                speed.X *= -1;
            }


            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width - texture.Width;
                speed.X *= -1;
            }

            

            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
                speed.Y = studs();
                speed.X -= speed.X < 0? -0.1f : 0.1f;

            }

            elaps += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        //Kollision
        public void Kollision(Player player)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //spelare och bollens mittpunkt
            playerMid = (player.Width / 2 + player.X);
            ballMid = X + texture.Width / 2;


            if (keyboardState.IsKeyDown(Keys.Space))
            {
                speed.Y = -6f;
                reset();
            }
            else
            {
                speed.Y = studs();
            }

            if(ballMid > playerMid)
            {
                speed.X = (ballMid - playerMid)/8;
            }
            else if (ballMid < playerMid)
            {
                speed.X = (ballMid - playerMid) /8;
            }

            /*
            if (ballMid > (playerMid - 10) && ballMid < (playerMid + 10))
            {
                speed.X = 0f;
            }
            */

        }


        //studs
        public float studs()
        {
            if (Ac<0f)
            {
                if (elaps >= 200f)
                {
                    Ac += 0.5f;

                    elaps = 0;
                }
            }

            return Ac;
        }

        public void reset()
        {
            Ac = -6f;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }
    }
}
