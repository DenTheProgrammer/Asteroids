using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

interface IDestructible
{
    public event Action OnDestroy;
    public abstract void Destruct();
}
