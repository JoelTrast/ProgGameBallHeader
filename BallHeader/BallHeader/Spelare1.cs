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
    class Spelare1 : Player
    {
        Texture2D[] vänster;
        Texture2D[] höger;

        double frames;

        public Spelare1(Texture2D[] vänster, Texture2D[] höger, float X, float Y, float speedX, float speedY, double frames) : base(vänster, höger, X, Y, speedX, speedY, frames)
        {
            this.vänster = vänster;
            this.höger = höger;

            this.speed.X = speedX;
            this.speed.Y = speedY;

            this.frames = frames;
        }

        public override void Update(GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //SPEED
            if (keyboardState.IsKeyDown(Keys.D))
            {
                vector.X += speed.X;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                vector.X -= speed.X;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                speed.Y = -4f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D))
            {
                spriteBatch.Draw(höger[(int)frames], vector, Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.A))
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
