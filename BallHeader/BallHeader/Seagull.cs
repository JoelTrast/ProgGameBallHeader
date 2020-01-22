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

        public Seagull(Texture2D[] texture, float X, float Y, float speedX, float speedY) : base(texture[0], X, Y, speedX, speedY)
        {
            this.texture = texture;
        }

        public void Update(GameWindow window, GameTime gameTime)
        {
            vector.X += speed.X;
            frames = Frames(2, 150f, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture[(int)frames], vector, Color.White);
        }
    }
}