using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectManager : Singleton<ObjectManager>
{
    #region
   // public ClassObjectPool<TestSerilize> Test = ObjectManager.Instance.GetOrCreatePool<TestSerilize>(100);
    protected Dictionary<Type, object> m_ClassPoolDic = new Dictionary<Type, object>();

    //public void TestClass()
    //{
    //   TestSerilize t= Test.Spawn(true);//拿取
    //    Test.Recycle(t);
    //}
    /// <summary>
    /// 创建类对象池,创建完成以后外面可以保存ClassObjectPool<T>,然后调用Spawn和Recycle来创建和回收类对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="maxcount"></param>
    /// <returns></returns>
    public ClassObjectPool<T> GetOrCreatePool<T>(int maxcount) where T : class, new()
    {
        System.Type type = typeof(T);
        object outObj = null;
        if (m_ClassPoolDic.TryGetValue(type, out outObj) || outObj == null)
        {
            ClassObjectPool<T> newPool = new ClassObjectPool<T>(maxcount);
            m_ClassPoolDic.Add(type,newPool);
            return newPool;
        }
        return outObj as ClassObjectPool<T>;
    }
    #endregion
}
