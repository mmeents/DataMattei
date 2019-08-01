using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;
using C0DEC0RE;
using System.Linq;

namespace UnitTestProject1 {

  public class CObject : ConcurrentDictionary<string, object> {
    public CObject() : base() { }
    public Boolean Contains(String aKey) { return base.Keys.Contains(aKey); }
    public new object this[string aKey] {
      get { return (Contains(aKey) ? base[aKey] : null); }
      set { base[aKey] = value; }
    }
    public void Remove(string aKey) {
      if (Contains(aKey)) {
        base.TryRemove(aKey, out object outcast);
      }
    }
    public void Merge(CObject aObject, Boolean OnDupOverwiteExisting) {
      if (aObject != null) {
        if (OnDupOverwiteExisting) {
          foreach (string sKey in aObject.Keys) {
            base[sKey] = aObject[sKey];
          }
        } else {
          foreach (string sKey in aObject.Keys) {
            if (!Contains(sKey)) {
              base[sKey] = aObject[sKey];
            }
          }
        }
      }
    }
  }

  public class CQueue : ConcurrentDictionary<Int64, object> {
    public Int64 Nonce = Int64.MinValue;
    public Boolean Contains(Int64 aKey) { return base.Keys.Contains(aKey); }
    public CQueue() : base() { }
    public object Add(object aObj) {
      Nonce++;
      base[Nonce] = aObj;
      return aObj;
      //return base[Nonce++] = aObj;
    }
    public object Pop() {
      Object aR = null;
      if (Keys.Count > 0) {        
        base.TryRemove(base.Keys.OrderBy(x => x).First(), out aR);
      }
      return aR;
    }
    public void Remove(Int64 aKey) {
      if (Contains(aKey)) {
        object outcast;
        base.TryRemove(aKey, out outcast);
      }
    }
  }


  [TestClass]
  public class UTQueue {

    [TestMethod]
    public void TestAdd1000() {
      CQueue testQ = new CQueue();
      for (Int32 i = 1; i <= 100000; i++) {
        testQ.Add(i);
      }
    }

    [TestMethod]
    public void TestAdd10001() {
      CQueue testQ = new CQueue();      
      for (Int32 i = 1; i<= 100000; i++) {
        testQ.Add(i.toString());
      }   
    }

    [TestMethod]
    public void TestAdd10002() {
      CQueue testQ = new CQueue();
      for (Int32 i = 1; i <= 100000; i++) {
        testQ[testQ.Nonce++] = i;        
      }
    }


    [TestMethod]
    public void TestMethod2() {

      CQueue testQ = new CQueue();
      string sN = "";
      for (Int32 i = 1; i <= 10000; i++) {
        testQ.Add(i.toString());
      }

      for (Int32 i = 1; i <= 10000; i++) {
        testQ.Add((i + 10000).toString());
        sN = (string)testQ.Pop();
      }      
            
    }

    [TestMethod]
    public void TestMethod3() {

      CQueue testQ = new CQueue();
      string sN = "";
      for (Int32 i = 1; i <= 1000; i++) {
        testQ.Add(i.toString());
      }      

      for (Int32 i = 1; i <= 999; i++) {
        sN = (string)testQ.Pop();
      }
      sN = (string)testQ.Pop();
      Console.WriteLine("cpB:" + sN);


    }

  }

 
}
