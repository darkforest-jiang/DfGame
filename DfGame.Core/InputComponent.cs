using DfGame.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DfGame.Core.Model.DfEnum;

namespace DfGame.Core
{
    public class InputComponent : DfComponent
    {
        public InputComponent(DfGameObject gameObj, int updateOrder) : base(gameObj, updateOrder)
        {

        }

        public override void ProcessInput(DfKeysState keysState)
        {
            var moveComponent = _gameObj.GetComponet<MoveComponent>();
            if (moveComponent != null)
            {
                switch (keysState.Keys)
                {
                    case Keys.Left: moveComponent.Direction = DfEnum.Direction.Left; break;
                    case Keys.Right: moveComponent.Direction = DfEnum.Direction.Right; break;
                    case Keys.Up: moveComponent.Direction = DfEnum.Direction.Up; break;
                    case Keys.Down: moveComponent.Direction = DfEnum.Direction.Down; break;
                }
            }
        }

    }
}
