using UnityEngine;

namespace DefaultNamespace
{
    public interface IStaticAwake
    {
        // function meant to be triggered by some event.
        // Users are meant to put static things here so they can be setup via awake
        public void StaticAwake()
        {
            throw new System.NotImplementedException();
        }
    }
}