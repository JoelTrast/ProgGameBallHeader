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
        public Goal(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 0)
        {

        }
    }
}
