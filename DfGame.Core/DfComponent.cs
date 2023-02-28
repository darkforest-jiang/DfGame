using DfGame.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGame.Core
{
    public class DfComponent:IDisposable
    {
        protected DfGameObject _gameObj;
        protected int _updateOrder;

        public DfComponent(DfGameObject gameObj, int updateOrder)
        {
            _gameObj = gameObj;
            _updateOrder = updateOrder;
            _gameObj.AddComponent(this);
        }

        public int UpdateOrder => _updateOrder;

        public virtual void ProcessInput(DfKeysState keysState) { }

        public virtual void Update() { }

        public virtual void Dispose()
        {

        }
    }
}
