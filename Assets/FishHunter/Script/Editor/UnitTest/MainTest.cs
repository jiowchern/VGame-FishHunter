using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using System.Linq;


namespace VGame
{
    using NSubstitute;
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void PathBuilderTest()
        {
           var pathAsset = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/FishHunter/Test/Path/Points.txt", typeof(UnityEngine.TextAsset)) as UnityEngine.TextAsset;
           var pathRoot = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/FishHunter/Test/Path/Root.prefab", typeof(UnityEngine.GameObject)) as UnityEngine.GameObject;
           var pathNode = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/FishHunter/Entity/Path/Node.prefab", typeof(UnityEngine.GameObject)) as UnityEngine.GameObject;

           PathBuilder pathBuilder = new PathBuilder();
           pathBuilder.File = pathAsset;
           pathBuilder.Target = GameObject.Instantiate(pathRoot);
           pathBuilder.Node = pathNode;
           pathBuilder.Build();

           var ch1 = pathBuilder.Target.transform.FindChild("1");
           var ch2 = pathBuilder.Target.transform.FindChild("2");
           var ch3 = pathBuilder.Target.transform.FindChild("3");
           
           Assert.AreEqual("1" , ch1.name);
           Assert.AreEqual("2", ch2.name);
           Assert.AreEqual("3", ch3.name);           
        }

        [Test]
        public void FishSetTest()
        {
            var fishOutline = new VGame.Project.FishHunter.FishBounds(0,new Regulus.CustomType.Rect(0, 0, 100, 100));
            fishOutline.SetBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));

            var set = new VGame.Project.FishHunter.FishSet();

            set.Add(fishOutline);

            var result = set.Query(new Regulus.CustomType.Rect(0, 0, 100, 100));

            Assert.AreEqual(1, result.Length);
        }
        [Test]
        public void FishSetTestChangeBounds()
        {


            var fishOutline = new VGame.Project.FishHunter.FishBounds(0,new Regulus.CustomType.Rect(0, 0, 100, 100));
            fishOutline.SetBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));

            var set = new VGame.Project.FishHunter.FishSet();

            set.Add(fishOutline);

            var result = set.Query(new Regulus.CustomType.Rect(0, 0, 100, 100));

            Assert.AreEqual(1, result.Length);


            fishOutline.SetBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));

            result = set.Query(new Regulus.CustomType.Rect(600, 500, 100, 100));

            Assert.AreEqual(0, result.Length);

            fishOutline.SetBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));
            
            result = set.Query(new Regulus.CustomType.Rect(0, 0, 100, 100));

            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void FishSetTestNoHit()
        {
            var fishOutline = new VGame.Project.FishHunter.FishBounds(0,new Regulus.CustomType.Rect(0, 0, 100, 100));
            fishOutline.SetBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));

            var set = new VGame.Project.FishHunter.FishSet();

            set.Add(fishOutline);

            var result = set.Query(new Regulus.CustomType.Rect(500, 500, 100, 100));

            Assert.AreEqual(0, result.Length);
        }
    }
}
