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
        static Keys keyCodeLeftPlayer1 = Keys.A;
        static Keys keyCodeRightPlayer1 = Keys.D;
        static Keys keyCodeJumpPlayer1 = Keys.W;

        static Keys keyCodeLeftPlayer2 = Keys.Left;
        static Keys keyCodeRightPlayer2 = Keys.Right;
        static Keys keyCodeJumpPlayer2 = Keys.Up;

        bool isPlayer1;
        Keys keyCodeLeft;
        Keys keyCodeRight;
        Keys keyCodeJump;

        float freezTime;

        Texture2D[] vänster;
        Texture2D[] höger;

        public Vector2 origin;

        public Player(Texture2D[] vänster, Texture2D[] höger, float X, float Y, float speedX, float speedY, bool isPlayer1) : base(höger[0], X, Y, speedX, speedY)
        {
            this.isPlayer1 = isPlayer1;
            if (isPlayer1)
            {
                this.keyCodeJump = keyCodeJumpPlayer1;
                this.keyCodeLeft = keyCodeLeftPlayer1;
                this.keyCodeRight = keyCodeRightPlayer1;
            }
            else
            {
                this.keyCodeJump = keyCodeJumpPlayer2;
                this.keyCodeLeft = keyCodeLeftPlayer2;
                this.keyCodeRight = keyCodeRightPlayer2;
            }

            this.höger = höger;
            this.vänster = vänster;

            this.origin = new Vector2(texture.Width / 2, texture.Width / 2);
        }

        /*################################################################################################*/
                                                    /*UPDATE*/
        /*################################################################################################*/

        public void Update(GameWindow window, GameTime gameTime)
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

            //Frames loop
            frames = Frames(1, 80f, gameTime);


            //SPEED
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(keyCodeRight))
            {
                vector.X += speed.X;
            }

            if (keyboardState.IsKeyDown(keyCodeLeft))
            {
                vector.X -= speed.X;
            }

            if (keyboardState.IsKeyDown(keyCodeJump))
            {
                speed.Y = -4f;
            }

            //PlayerKollision
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                speed.X = 4f;


        }

        /*################################################################################################*/
                                                    /*METODER*/
        /*################################################################################################*/

        public void PlayerKollision()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            speed.X = 0f;

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Right))
                speed.X = 4f;
        }

        public void Reset(float X, float Y)
        {
            vector.X = X;
            vector.Y = Y;
            speed.X = 4f;
        }

        public void freez()
        {
            speed.X = 0;
        }

        public void speedReset()
        {
            speed.X = 4f;
        }

        /*################################################################################################*/
                                                        /*DRAW*/
        /*################################################################################################*/
        public override void Draw(SpriteBatch spriteBatch)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(keyCodeRight))
            {
                spriteBatch.Draw(höger[(int)frames], vector, Color.White);
            }
            else if (keyboardState.IsKeyDown(keyCodeLeft))
            {
                spriteBatch.Draw(vänster[(int)frames], vector, Color.White);
            }
            else
            {
                if (isPlayer1)
                    spriteBatch.Draw(höger[1], vector, Color.White);
                else
                    spriteBatch.Draw(vänster[1], vector, Color.White);
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
