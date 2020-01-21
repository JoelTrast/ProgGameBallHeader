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
    class Player : PhysicalObject
    {
        Texture2D[] framåt;
        Texture2D[] bakåt;
        Texture2D[] vänster;
        Texture2D[] höger;
        float elaps;
        float delay=80f;
        double frames=0;
        
        public Player(Texture2D[] vänster, Texture2D[] höger, float X, float Y, float speedX, float speedY) : base(höger[0], X, Y, speedX, speedY)
        {   
            this.vänster = vänster;
            this.höger = höger;
        }

        public void Update(GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //if (keyboardState.IsKeyDown(Keys.Escape))
            //isAlive = false;

            //SPEED
            if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    vector.X += speed.X;
                }
                    
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    vector.X -= speed.X;
                }
            }



            
            vector.Y += speed.Y;

            speed.Y += 0.25f;

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                speed.Y = -4f;
            }


                


            //SPEED
            /*
            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    vector.Y += speed.Y;
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    vector.Y -= speed.Y;
                }
            }
            */
            
            //BOOST
            /*
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                delay = 40f;
                speed.X = 6f;

                speed.Y = -2f;
                
            }
            else
            {

                speed.X = 4f;
                speed.Y = 0f;
                delay = 100f;
            }
            */
            //FRAMES LOOP
            /*
            if (!(keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.Up)))
            {
                delay = 300f;
                //vector.Y += 1f;
            }
            */
            elaps += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elaps >= delay)
            {
                if (frames > 1) frames = 0;

                else frames++;

                elaps = 0;
            }



                //within window
                if (vector.X < 0)
                vector.X = 0;

            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width - texture.Width;
            }

            if (vector.Y < 0)
                vector.Y = 0;

            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                spriteBatch.Draw(höger[(int)frames], vector, Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                spriteBatch.Draw(vänster[(int)frames], vector, Color.White);
            }
            else
            {
                spriteBatch.Draw(höger[1], vector, Color.White);
            }
        }
    }
}
