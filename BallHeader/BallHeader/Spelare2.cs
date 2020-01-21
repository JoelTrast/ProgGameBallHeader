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
    class Spelare2 : Player
    {
        Texture2D[] vänster;
        Texture2D[] höger;

        float elaps;
        float delay = 80f;
        double frames;

        public Spelare2(Texture2D[] vänster, Texture2D[] höger, float X, float Y, float speedX, float speedY, double frames) : base(vänster, höger, X, Y, speedX, speedY, frames)
        {
            this.vänster = vänster;
            this.höger = höger;

            this.frames = frames;
        }

        public override void Update(GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //SPEED
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                vector.X += speed.X;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                vector.X -= speed.X;
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                speed.Y = -4f;
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
                spriteBatch.Draw(vänster[1], vector, Color.White);
            }

        }
    }
}
