using DfGame.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGame.Core
{
    public class MoveComponent : DfComponent
    {
        private DfEnum.Direction _direction = DfEnum.Direction.Right;
        public DfEnum.Direction Direction
        {
            get => _direction;
            set
            {
                //if (_isMoving)
                //{
                //    return;
                //}
                _direction = value;
                //_dirLoc.MoveTo(_gameObj.Position.Position);
                //_dirLoc.Move(_direction, 50);
                //_isMoving = true;
            }
        }
        private DfEnum.Direction? _tempDir = null;

        private float _moveSpeed = 2;
        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        private bool _isMoving = true;
        private float _pixelsCount = 0.0f;
        private Point _dirLoc = new Point(0, 0);
        public Point DirLoc
        {
            get => _dirLoc;
            set => _dirLoc = value;
        }

        public MoveComponent(DfGameObject gameObj, int updateOrder) : base(gameObj, updateOrder)
        {
        }

        private float _lastm = 0.0f;


        public override void Update()
        {
            //if(_isMoving)
            {
                if (_tempDir == null)
                {
                    _tempDir = _direction;
                }

                //var moveLength = _moveSpeed * DfTimer.deltaTime;
                var moveLength = _moveSpeed;
                _pixelsCount += moveLength;
                moveLength += _lastm;

                int i = (int)moveLength;
                if (i < moveLength)
                {
                    _lastm = moveLength - i;
                }
                else
                {
                    _lastm = 0.0f;
                }


                if (_pixelsCount < 50)
                {
                    //_gameObj.Position.Move(_tempDir!.Value, i);
                }
                else
                {
                    //_isMoving = false;
                    //_pixelsCount = 0.0f;
                    //_lastm = 0.0f;
                    //_tempDir = null;
                    //_gameObj.Position.MoveTo(_dirLoc.Position);
                    //_dirLoc.Move(_direction, 50);
                }
            }
        }
    }
}
