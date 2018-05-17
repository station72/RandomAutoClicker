using RandomAutoClicker.Model.Clicker.ClickBehaviour;
using RandomAutoClicker.Model.Clicker.Config;
using RandomAutoClicker.Model.Clicker.Interval;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RandomAutoClicker.Model.Clicker
{
    internal class Clicker : IMouseClicker
    {
        private CancellationTokenSource _cancelTokenSource;
        private readonly Random _random;
        private readonly BaseClickerConfig _clickConfig;
        private readonly IClickerInterval _interval;
        private readonly ClickBehaviourBase _clickBehaviour;

        internal Clicker(IClickerInterval interval, BaseClickerConfig clickConfig, ClickBehaviourBase clickBehaviour)
        {
            _clickBehaviour = clickBehaviour;

            _interval = interval;
            _clickConfig = clickConfig;

            _random = new Random();
            _cancelTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            _cancelTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                StartTask(_cancelTokenSource.Token);
            });
        }

        public void Stop()
        {
            _cancelTokenSource?.Cancel();
        }

        private async void StartTask(CancellationToken token)
        {
            while (true)
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    int delay = _interval.GetNextInterval();
                    await Task.Delay(delay, token);
                    await ClickOnRandomPos();
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    throw;
                }
            }
        }

        private async Task ClickOnRandomPos()
        {
            var xPos = _random.Next(_clickConfig.GetXStart(), _clickConfig.GetXStart() + _clickConfig.GetWidth());
            var yPos = _random.Next(_clickConfig.GetYStart(), _clickConfig.GetYStart() + _clickConfig.GetHeight());

            _clickBehaviour.SetCursorPosition(xPos, yPos);
            await _clickBehaviour.ClickAsync(xPos, yPos);
        }
    }
}