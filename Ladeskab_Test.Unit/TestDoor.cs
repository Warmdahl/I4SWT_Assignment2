using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Moduls;
using NUnit.Framework;

namespace Ladeskab_Test.Unit
{
    class TestDoor
    {
        private Ladeskab.Moduls.Door _door;
        private ChangedEventArgs _changedEventArgs;

        [SetUp]
        public void Setup()
        {
            _changedEventArgs = null;
            _door = new Door();
            _door.ChangedValueEvent += (o, args) => { _changedEventArgs = args; };
        }

        [Test]
        public void Door_isUnlockedfromstandby()
        {
            Assert.That(_door.Islocked, Is.False);
        }

        [Test]
        public void Door_isLocked()
        {
            _door.LockDoor();
            Assert.That(_door.Islocked, Is.True);
        }

        [Test]
        public void Door_isUnlocked()
        {
            _door.LockDoor();
            _door.UnlockDoor();
            Assert.That(_door.Islocked, Is.False);
        }

        [Test]
        public void Door_StateisOpen()
        {
            _door.SimulateDoorOpen();
            Assert.That(_changedEventArgs.DoorState, Is.True); //Doorstate is true when open
        }

        [Test]
        public void Door_StateisClosed()
        {
            _door.SimulateDoorClose();
            Assert.That(_changedEventArgs.DoorState, Is.False); //Doorstate is false
        }
    }
}
