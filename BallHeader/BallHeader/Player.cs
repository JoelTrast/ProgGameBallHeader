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
    abstract class Player : PhysicalObject
    {
        public Vector2 origin;

        float elaps;
        float delay = 80f;
        double frames;

        public Player(Texture2D[] vänster, Texture2D[] höger, float X, float Y, float speedX, float speedY, double frames) : base(höger[0], X, Y, speedX, speedY)
        {
            this.origin = new Vector2(texture.Width / 2, texture.Width / 2);
        }

        public virtual void Update(GameWindow window, GameTime gameTime)
        {
            //if (keyboardState.IsKeyDown(Keys.Escape))
            //isAlive = false;

            //gravity
            vector.Y += speed.Y;

            speed.Y += 0.25f;


            //X-axeln vänster
            if (vector.X < 0)
                vector.X = 0;

            //X-axeln höger
            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width - texture.Width;
            }

            //Y-axeln upp
            if (vector.Y < 0)
                vector.Y = 0;

            //Y-axeln ner
            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
            }

            //frames loop
            elaps += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elaps >= delay)
            {
                if (frames > 1) frames = 0;

                else frames++;

                elaps = 0;
            }
        }

    }
}



































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
