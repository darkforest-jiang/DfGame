using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGame.Core
{
    public class Map2DComponent : SpriteComponent
    {
        private Bitmap? _texture1;
        private Bitmap? _texture2;

        private int[,] _mapData = new int[5, 5] {
            { 1, 1, 1, 1, 1},
            { 1, 0, 0, 0, 1},
            { 1, 0, 0, 0, 1},
            { 1, 0, 0, 0, 1},
            { 1, 1, 1, 1, 1},
        };

        public Map2DComponent(DfGameObject gameObj, int drawOrder = 0) : base(gameObj, drawOrder)
        {

        }

        public void SetTexture(Bitmap texture1, Bitmap texture2)
        {
            _texture1 = texture1;
            _texture2 = texture2;
        }

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var bmp = _mapData[i, j] == 0 ? _texture1! : _texture2!;
                    g.DrawImage(bmp, i * 50, j * 50, 50, 50);
                }
            }
        }

        public override void Dispose()
        {
            _texture1?.Dispose();
            _texture2?.Dispose();

            base.Dispose();
        }
    }
}
