using System;
using System.Reflection;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class FunctionSelectorBase
    {
    }

    [Serializable]
    public class FunctionSelectorBool : FunctionSelector<bool>
    {
    }

    [Serializable]
    public class FunctionSelectorInt : FunctionSelector<int>
    {
    }

    [Serializable]
    public class FunctionSelectorFloat : FunctionSelector<float>
    {
    }

    [Serializable]
    public class FunctionSelectorGameObject : FunctionSelector<GameObject>
    {
    }

    public class FunctionSelector<ReturnType> : FunctionSelectorBase
    {
        public GameObject targetGameObject;
        public MonoBehaviour targetScript;
        public string methodName;

        private object type = null;

        public FunctionSelector()
        {
            //method = type.GetType().GetMethod(methodName);
        }

        public ReturnType Invoke(object instance)
        {
            return (ReturnType)instance.GetType().GetMethod(methodName).Invoke(instance, null);
        }

    }
}
