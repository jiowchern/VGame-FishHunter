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
        [UnityEditor.MenuItem("VGame/Test")]
        public static void Run()
        {            
            
        }


       

        [Test]
        public void FishSetTest()
        {
            var fishOutline = new VGame.Project.FishHunter.FishBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));
            fishOutline.SetBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));

            var set = new VGame.Project.FishHunter.FishSet();

            set.Add(fishOutline);

            var result = set.Query(new Regulus.CustomType.Rect(0, 0, 100, 100));

            Assert.AreEqual(1, result.Length);
        }
        [Test]
        public void FishSetTestChangeBounds()
        {
            var fishOutline = new VGame.Project.FishHunter.FishBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));
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
            var fishOutline = new VGame.Project.FishHunter.FishBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));
            fishOutline.SetBounds(new Regulus.CustomType.Rect(0, 0, 100, 100));

            var set = new VGame.Project.FishHunter.FishSet();

            set.Add(fishOutline);

            var result = set.Query(new Regulus.CustomType.Rect(500, 500, 100, 100));

            Assert.AreEqual(0, result.Length);
        }
    }
}
