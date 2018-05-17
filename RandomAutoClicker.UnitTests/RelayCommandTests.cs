using NUnit.Framework;
using RandomAutoClicker.Infrastructure;
using System;

namespace RandomAutoClicker.UnitTests
{
    [TestFixture]
    public class RelayCommandTests
    {
        [Test]
        public void ctor_ExecuteNullArgument_ThrowArgumentsNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new RelayCommand(null); });
        }

        [Test]
        public void ctor_AllNullArguments_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RelayCommand(null, null));
        }

        [Test]
        public void CanExecute_nullCanExecute_true() 
        {
            RelayCommand relayCommand = new RelayCommand((o) => { }, null);
            Assert.True(relayCommand.CanExecute(null));
        }

        [Test]
        public void CanExecute_canExecuteReturnTrue_true() 
        {
            RelayCommand relayCommand = new RelayCommand(o => { }, o => true);
            Assert.True(relayCommand.CanExecute(null));
        }

        [Test]
        public void CanExecute_canExecutereturnFalse_false() 
        {
            RelayCommand relayCommand = new RelayCommand(o => { }, o => false);
            Assert.False(relayCommand.CanExecute(null));
        }
    }
}
