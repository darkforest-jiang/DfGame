using DfGame.Core.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DfGame.Core
{
    public class DfGame : IDisposable
    {
        private Graphics _gph;
        private int _width;
        private int _heigth;
        private Bitmap _dbuffer;
        private Graphics _dbufferG;

        private bool _isRunning;

        private bool _isFirstFrame = true;

        private List<DfGameObject> _gameObjs = new List<DfGameObject>();
        private List<DfGameObject> _gamePendingObjs = new List<DfGameObject>();
        private bool _isUpdating;

        private List<SpriteComponent> _sprites = new List<SpriteComponent>();

        public Channel<DfKeysState> _eventsChannel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ctl"></param>
        public DfGame(Graphics gph, int width, int height)
        {
            _gph = gph;
            _width = width;
            _heigth = height;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            _dbuffer = new Bitmap(_width, _heigth);
            _dbufferG = Graphics.FromImage(_dbuffer);

            _eventsChannel = Channel.CreateBounded<DfKeysState>(new BoundedChannelOptions(1)
            {
                FullMode = BoundedChannelFullMode.DropOldest
            });

            _isRunning = true;
            return true;
        }

        /// <summary>
        /// 游戏主循环
        /// </summary>
        public void Loop()
        {
            while (_isRunning)
            {
                Event();
                Update();
                Draw();
            }
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        public void Shutdown()
        {
            _isRunning = false;
        }

        private void Tick(int fps)
        {
            int fpsTime = 1000 / fps;

            var nowticks = DateTime.Now.Ticks;
            if (_isFirstFrame)
            {
                DfTimer.LastTicks = nowticks;
                _isFirstFrame = false;
            }
            else
            {
                while ((nowticks - DfTimer.LastTicks) / 10000.0f < fpsTime)
                {
                    Thread.Sleep(1);
                    nowticks = DateTime.Now.Ticks;
                }

                DfTimer.DeltaTime = (nowticks - DfTimer.LastTicks) / 10000000.0f;
                DfTimer.LastTicks = nowticks;
            }
        }

        /// <summary>
        /// 处理事件
        /// </summary>
        private void Event()
        {
            _isUpdating = true;

            DfKeysState? dfKeysState;
            _eventsChannel.Reader.TryRead(out dfKeysState);
            if (dfKeysState != null)
            {
                switch (dfKeysState.Keys)
                {
                    case DfEnum.Keys.Escape: _isRunning = false; break;
                    case DfEnum.Keys.Space:
                        do
                        {
                            Thread.Sleep(500);
                            DfTimer.LastTicks = DfTimer.LastTicks + 500 * 10000;
                            _eventsChannel.Reader.TryRead(out dfKeysState);
                        } while (dfKeysState == null || dfKeysState.Keys != DfEnum.Keys.Space);
                        break;
                }
            }

            _isUpdating = true;
            foreach (var item in _gameObjs)
            {
                item.ProcessInput(dfKeysState);
            }
            _isUpdating = false;

        }

        /// <summary>
        /// 更新各种状态
        /// </summary>
        private void Update()
        {
            //Tick(100);

            _isUpdating = true;


            foreach (var item in _gameObjs)
            {
                item.Update();
            }
            _isUpdating = false;

            foreach (var item in _gamePendingObjs)
            {
                _gameObjs.Add(item);
            }
            _gamePendingObjs.Clear();

            _gameObjs.RemoveAll(p => p.State == DfEnum.GameObjectState.Dead);

            Thread.Sleep(1000 / 60);
        }

        /// <summary>
        /// 渲染
        /// </summary>
        private void Draw()
        {
            foreach (var item in _sprites)
            {
                item.Draw(_dbufferG);
            }
            _gph.DrawImage(_dbuffer, 0, 0);
        }

        public void CreateGameObject(DfGameObject gameObj)
        {
            if (_isUpdating)
            {
                _gamePendingObjs.Add(gameObj);
            }
            else
            {
                _gameObjs.Add(gameObj);
            }
        }

        public void RemoveGameObject(DfGameObject gameObj)
        {
            if (!_gamePendingObjs.Remove(gameObj))
            {
                _gameObjs.Remove(gameObj);
            }
        }

        public void CreateSprite(SpriteComponent sprite)
        {
            int order = sprite.DrawOrder;
            var index = 0;
            foreach (var item in _sprites)
            {
                if (order <= item.DrawOrder)
                {
                    break;
                }
                index++;
            }
            _sprites.Insert(index, sprite);
        }

        public void RemoveSprite(SpriteComponent sprite)
        {
            _sprites.Remove(sprite);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
