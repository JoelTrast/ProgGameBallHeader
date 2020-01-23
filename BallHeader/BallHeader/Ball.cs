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
        float Ac = -6f;
        bool isJumping = false;

        float ballMidX;
        float playerMidX;

        float ballMidY;
        float playerMidY;

        //rotations variabler
        public Vector2 origin;
        //float rotation;


        public Ball(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        /*################################################################################################*/
                                                        /*UPDATE*/
        /*################################################################################################*/

        public void Update(GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //gravitiy function
            vector.Y += speed.Y;
            vector.X += speed.X;

            speed.Y += 0.15f;


            //X-axel vänster
            if (vector.X < 0)
            {
                vector.X = 0; 
                speed.X *= -1; //byter håll
            }

            //X-axel höger
            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width - texture.Width;
                speed.X *= -1; //byter håll
            }

            //Y-axel ner
            if (vector.Y > window.ClientBounds.Height - texture.Height - 6)
            {
                vector.Y = window.ClientBounds.Height - texture.Height - 6;

                //friktion
                if (speed.X < 0)
                    speed.X += 0.3f;
                else if (speed.X > 0)
                    speed.X -= 0.3f;

                //studsar vid marken
                speed.Y = studs(); 
            }

            elaps += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /*################################################################################################*/
                                                    /*METODER*/
        /*################################################################################################*/

        //Kollision
        public void Kollision(Player player, GameWindow window)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //spelare och bollens mittpunkt(X)
            playerMidX = player.origin.X + player.X;
            ballMidX = origin.X + vector.X;

            //spelare och bollens mittpunkt(Y)
            playerMidY = player.origin.Y + player.Y;
            ballMidY = origin.Y + vector.Y;


            //ball speed i X-axeln
            if (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter)|| keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                Ac = -5f; //reset studs
            }

            if (player.Y + player.Height < window.ClientBounds.Height)
                isJumping = true;
            else
                isJumping = false;

            //Y
            if (ballMidY != playerMidY && isJumping)
            {
                if (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter))
                    speed.Y = -5f;
                else
                    speed.Y = (ballMidY - playerMidY) / 10;

            }
            else if (!isJumping)
                speed.Y = studs();

            //X
            if (Ac == 0 && vector.Y < window.ClientBounds.Height - texture.Height - 6) //ifall bollen inte studsar på huvudet så är speed.X = 0
            {
                speed.X = 0;
            }
            else if (ballMidX != playerMidX)
            {

                    speed.X = (ballMidX - playerMidX) / 7;
            }
        }

        //GOAL KOLLISION
        public void GoalKollision(Goal goal)
        {
            speed.Y = studs();
         
        }

        //RESET BALL
        public void Reset(float X, float Y, float speedX, float speedY)
        {
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;
        }

        //studs
        public float studs()
        {
            if (Ac < 0f)
            {
                if (elaps >= 200f)
                {
                    Ac += 1f;

                    elaps = 0;
                }
            }
            return Ac;
        }

        /*################################################################################################*/
                                                        /*DRAW*/
        /*################################################################################################*/

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            //spriteBatch.Draw(texture, vector, null, Color.White, rotation, origin, 1 ,SpriteEffects.None, 0f);
        }
    }
}