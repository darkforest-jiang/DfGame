using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGame.Core
{
    public class SpriteComponent : DfComponent
    {
        protected Bitmap? _texture;
        protected int _drawOrder;
        protected int _texWidth;
        protected int _texHeight;

        public SpriteComponent(DfGameObject gameObj, int drawOrder) : base(gameObj, 0)
        {
            _drawOrder = drawOrder;
            _gameObj.GetGame().CreateSprite(this);
        }

        public int DrawOrder
        {
            get { return _drawOrder; }
        }

        public virtual void SetTexture(Bitmap bmp)
        {
            _texture = bmp;
            _texWidth = bmp.Width;
            _texHeight = bmp.Height;
        }

        public virtual void Draw(Graphics g)
        {
            if (_texture == null)
            {
                return;
            }

            g.DrawImage(_texture, _gameObj.Position.X, _gameObj.Position.Y, 50, 50);
        }
    }
}
