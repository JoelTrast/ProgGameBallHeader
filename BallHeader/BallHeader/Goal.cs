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
    class Goal : PhysicalObject
    {
        bool rightGoal;

        public Goal(Texture2D texture, float X, float Y, bool rightGoal) : base(texture, X, Y, 0, 0)
        {
            this.rightGoal = rightGoal;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (rightGoal)
                spriteBatch.Draw(texture, vector, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0f);
            else
                spriteBatch.Draw(texture, vector, Color.White);
        }
    }
}
