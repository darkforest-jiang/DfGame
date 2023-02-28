using DfGame.Core.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGame.Core
{
    public class DfGameObject : IDisposable
    {
        private DfGame _dfGame;
        private DfEnum.GameObjectState _state;
        private Point _position;

        protected IList<DfComponent> _components = new List<DfComponent>();

        public DfGameObject(DfGame dfGame)
        {
            _dfGame = dfGame;
            _state = DfEnum.GameObjectState.Active;
            dfGame.CreateGameObject(this);
        }

        public DfGame GetGame() => _dfGame;

        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        public DfEnum.GameObjectState State
        {
            get => _state;
            set => _state = value;
        }

        public void ProcessInput(DfKeysState? dfKeysState)
        {
            if (dfKeysState == null)
            {
                return;
            }
            if (_state == DfEnum.GameObjectState.Active)
            {
                foreach (var item in _components)
                {
                    item.ProcessInput(dfKeysState!);
                }
            }
        }

        public void Update()
        {
            if (_state == DfEnum.GameObjectState.Active)
            {
                foreach (var item in _components)
                {
                    item.Update();
                }
            }
        }

        public void AddComponent(DfComponent component)
        {
            int order = component.UpdateOrder;
            var index = 0;
            foreach (var item in _components)
            {
                if (order <= item.UpdateOrder)
                {
                    break;
                }
                index++;
            }
            _components.Insert(index, component);
        }

        public void RemoveComponent(DfComponent dfComponent)
        {
            _components.Remove(dfComponent);
        }

        public T? GetComponet<T>() where T : DfComponent
        {
            foreach (var item in _components)
            {
                if (item is T)
                {
                    return item as T;
                }
            }
            return null;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
