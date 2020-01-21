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
        float Ac = -6f;
        float ballMid;
        float playerMid;

        //rotations variabler
        public Vector2 origin;
        float rotation;


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
            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
                
                //friktion
                speed.X -= speed.X < 0 ? -0.2f : 0.2f;

                //studsar vid marken
                speed.Y = studs(); 
            }

            elaps += (float)gameTime.ElapsedGameTime.TotalMilliseconds;


            //Rotation
            rotation += speed.X > 0 ? MathHelper.ToRadians(speed.X) : -MathHelper.ToRadians(-speed.X);
        }

        /*################################################################################################*/
                                                    /*METODER*/
        /*################################################################################################*/

        //Kollision
        public void Kollision(Player player, GameWindow window)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //spelare och bollens mittpunkt
            playerMid = player.origin.X + player.X;
            ballMid = origin.X + vector.X;

            //ball speed i X-axeln
            if (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter) || keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                speed.Y = -6f;
                Ac = -6f;
            }
            else
            {
                speed.Y = studs();
            }

            if (Ac == 0 && vector.Y < window.ClientBounds.Height - texture.Height) //ifall bollen inte studsar på huvudet så är speed.X = 0
            {
                speed.X = 0;
            }
            else if (ballMid != playerMid)
            {
                speed.X = (ballMid - playerMid) / 7;
            }
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