using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfGame.Core.Model
{
    public class DfKeysState
    {
        public bool IsAlt { get; set; } = false;
        public bool IsCtrl { get; set; } = false;
        public DfEnum.KeysEvent KeysEvent { get; set; }
        public DfEnum.Keys Keys { get; set; }
    }
}
