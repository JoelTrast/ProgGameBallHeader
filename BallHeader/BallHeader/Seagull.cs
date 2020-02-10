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
    class Seagull : PhysicalObject
    {
        Texture2D[] texture;

        List<Bullet> bullets;
        Texture2D bulletTexture;


        public Seagull(Texture2D[] texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture) : base(texture[0], X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;

            this.texture = texture;
        }

        public void Update(GameWindow window, GameTime gameTime)
        {
            vector.X += speed.X;
            frames = Frames(2, 120f, gameTime);

            Random random = new Random();
            int newBullet = random.Next(1, 50);

            if (newBullet == 1)
            {
                Bullet temp = new Bullet(bulletTexture, vector.X + bulletTexture.Width / 2, vector.Y);
                bullets.Add(temp);
            }

            //Reset
            if (vector.X - 100 > window.ClientBounds.Width)
                isAlive = false;

            if (vector.X + 100 < 0)
                isAlive = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture[(int)frames], vector, Color.White);

            foreach (Bullet b in bullets)
                b.Draw(spriteBatch);
        }

        public List<Bullet> Bullets { get { return bullets; } }

        public void Reset(float X, float Y)
        {
            vector.X = X;
            vector.Y = Y;
            isAlive = true;
        }
    }



    class Bullet : PhysicalObject
    {
        Seagull seagull;

        public Bullet(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 0)
        {
        }

        public void Update()
        {
            speed.Y += 0.15f;
            vector.Y += speed.Y;
            if (vector.Y < 0)
            {
                isAlive = false;
                foreach (Bullet b in seagull.Bullets.ToList())
                    seagull.Bullets.Remove(b);
            }
        }
    }
}