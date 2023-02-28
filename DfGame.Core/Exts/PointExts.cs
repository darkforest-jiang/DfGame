using DfGame.Core.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGame.Core.Exts
{
    public static class PointExts
    {
        public static void Move(this Point point, DfEnum.Direction direction, int pixels)
        {
            switch (direction)
            {
                case DfEnum.Direction.Left: point.X -= pixels; break;
                case DfEnum.Direction.Right: point.X += pixels; break;
                case DfEnum.Direction.Up: point.Y -= pixels; break;
                case DfEnum.Direction.Down: point.Y += pixels; break;
            }
        }
    }
}
