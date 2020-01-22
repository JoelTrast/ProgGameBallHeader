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
    class PhysicalObject : MovingObject
    {
        protected bool isAlive = true;
        protected float elaps;
        protected double frames;


        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {

        }

        public bool CheckCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Convert.ToInt32(Width), Convert.ToInt32(Height));
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }


        public double Frames(double amount, float delay, GameTime gameTime)
        {
            

            elaps += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elaps >= delay)
            {
                if (frames > amount) frames = 0;

                else frames++;

                elaps = 0;
            }

            return frames;
        }
    }
}
