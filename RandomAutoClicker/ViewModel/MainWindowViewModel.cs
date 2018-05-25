using RandomAutoClicker.Infrastructure;
using RandomAutoClicker.Infrastructure.Events;
using RandomAutoClicker.Model;
using RandomAutoClicker.Model.Clicker;
using RandomAutoClicker.Model.Clicker.Factory;
using System;
using System.Windows;
using System.Windows.Input;

namespace RandomAutoClicker.ViewModel
{
    //TODO: descrease access level in the project
    //TODO: try to split up class into couple of separate
    public class MainWindowViewModel : ObservableObject, IDisposable
    {
        private readonly IEventBroker<ClickerEventArgs> _eventBroker;
        private readonly ISubscribesContainer<ClickerEventArgs> _subscribeContainer;
        private readonly IViewDispatcher _dispatcher;
        private readonly IClickerFactory _clickerFactory;
        private readonly IDelayRangeProvider _delayRangeProvider;
        private readonly IAreaRectProvider _areaRectProvider;
        private readonly IFixedDelayProvider _fixedDelayProvider;
        private readonly int _deltaValue;
        private IMouseClicker _clicker;

        public MainWindowViewModel(
            IEventBroker<ClickerEventArgs> eventBroker,
            ISubscribesContainer<ClickerEventArgs> subscribeContainer,
            IViewDispatcher dispatcher,
            IClickerFactory clickerFactory,
            IDelayRangeProvider delayRangeProvider,
            IAreaRectProvider areaRectProvider,
            IFixedDelayProvider fixedDelayProvider
            )
        {
            _eventBroker = eventBroker ?? throw new NullReferenceException(nameof(eventBroker));
            _subscribeContainer = subscribeContainer ?? throw new NullReferenceException(nameof(subscribeContainer));
            _dispatcher = dispatcher ?? throw new NullReferenceException(nameof(dispatcher));
            _clickerFactory = clickerFactory ?? throw new NullReferenceException(nameof(clickerFactory));
            _delayRangeProvider = delayRangeProvider ?? throw new NullReferenceException(nameof(delayRangeProvider));
            _areaRectProvider = areaRectProvider ?? throw new NullReferenceException(nameof(areaRectProvider));
            _fixedDelayProvider = fixedDelayProvider ?? throw new NullReferenceException(nameof(fixedDelayProvider));

            _deltaValue = Constants.DeltaValue;

            Init();
            Subscribe();
        }

        private void Init()
        {
            Area = _areaRectProvider.GetAreaRect();
            DelayRange = _delayRangeProvider.GetDelayRange();
            FixedDelay = _fixedDelayProvider.GetFixedDelay();
        }

        //TODO: save all values on exit
        private void Subscribe()
        {
            _subscribeContainer.Subscribe(EventNames.KeyPressHandled, OnKeyPressHandled);
            _subscribeContainer.Subscribe(EventNames.ToggleClickerState, OnToggleClickerState);
        }

        private void OnKeyPressHandled(ClickerEventArgs args)
        {
            IsKeyBindWorking = false;
            BindedKey = args.Data.ToString();
            _eventBroker.Raise(EventNames.KeyBindingStop, new ClickerEventArgs
            {
                Data = args.Data,
                Sender = this
            });
        }

        private void OnToggleClickerState(ClickerEventArgs args)
        {
            if (args.Data.ToString() == BindedKey)
            {
                if (!IsClickerWorking)
                {
                    if (Start.CanExecute(null))
                        Start.Execute(null);

                    return;
                }

                if (Stop.CanExecute(null))
                    Stop.Execute(null);
            }
        }

        private string _bindedKey =  Constants.DefaultBindedKey;
        public string BindedKey
        {
            get { return _bindedKey; }
            set
            {
                _bindedKey = value;
                RaisePropertyChangedEvent(nameof(BindedKey));
            }
        }

        private ClickTypeEnum _clickType = ClickTypeEnum.LeftClick;
        public ClickTypeEnum ClickType
        {
            get { return _clickType; }
            set
            {
                _clickType = value;
                RaisePropertyChangedEvent(nameof(ClickType));
            }
        }

        private ClickAreaEnum _clickArea = ClickAreaEnum.Area;
        public ClickAreaEnum ClickArea
        {
            get { return _clickArea; }
            set
            {
                _clickArea = value;
                RaisePropertyChangedEvent(nameof(ClickArea));
            }
        }

        private ClickDelayEnum _clickDelay = ClickDelayEnum.Fixed;
        public ClickDelayEnum ClickDelay
        {
            get { return _clickDelay; }
            set
            {
                _clickDelay = value;
                RaisePropertyChangedEvent(nameof(ClickDelay));
            }
        }

        private bool _isClickerWorking;
        public bool IsClickerWorking
        {
            get { return _isClickerWorking; }
            set
            {
                _isClickerWorking = value;
                _dispatcher.Invoke(() =>
                {
                    InvalidateRequerySuggested();
                });
            }
        }

        private bool _isKeyBindWorking;
        public bool IsKeyBindWorking
        {
            get { return _isKeyBindWorking; }
            set
            {
                _isKeyBindWorking = value;
                _dispatcher.Invoke(() =>
                {
                    RaisePropertyChangedEvent(nameof(IsKeyBindWorking));
                    InvalidateRequerySuggested();
                });
            }
        }

        private ICommand _start;
        public ICommand Start
        {
            get
            {
                return _start ?? (_start = new RelayCommand(
                    StartExecute,
                    (o) => !IsClickerWorking
                ));
            }
        }

        private RelayCommand _stop;
        public ICommand Stop
        {
            get
            {
                return _stop ?? (_stop = new RelayCommand(
                    StopExecute,
                    (o) => IsClickerWorking
                ));
            }
        }

        public void StartExecute(object parameter)
        {
            _clicker = GetCurrentClicker();
            _clicker.Start();
            IsClickerWorking = true;
            IsMenuEnabled = false;
        }

        public void StopExecute(object parameter)
        {
            _clicker.Stop();
            IsClickerWorking = false;
            IsMenuEnabled = true;
        }

        private bool _isMenuEnabled = true;
        public bool IsMenuEnabled
        {
            get { return _isMenuEnabled; }
            set { _isMenuEnabled = value; RaisePropertyChangedEvent(nameof(IsMenuEnabled)); }
        }

        public AreaRect Area { get; set; }

        #region X
        private ICommand _xUpCommand;
        public ICommand XUpCommand
        {
            get
            {
                return _xUpCommand ?? (_xUpCommand = new RelayCommand(
                        (o) => { Area.X += _deltaValue; },
                        (o) => !IsClickerWorking)
                        );
            }
        }

        private ICommand _xDownCommand;
        public ICommand XDownCommand
        {
            get
            {
                return _xDownCommand ?? (_xDownCommand = new RelayCommand(
                    (o) => { Area.X -= _deltaValue; },
                    (o) => Area.X > 0 && !IsClickerWorking
                    ));
            }
        }
        #endregion

        #region Y
        private ICommand _yUpCommand;
        public ICommand YUpCommand
        {
            get
            {
                return _yUpCommand ?? (_yUpCommand = new RelayCommand(
                    (o) => { Area.Y += _deltaValue; },
                    (o) => !IsClickerWorking
                    ));
            }
        }

        private ICommand _yDownCommand;
        public ICommand YDownCommand
        {
            get
            {
                return _yDownCommand ?? (_yDownCommand = new RelayCommand(
                    (o) => { Area.Y -= _deltaValue; },
                    (o) => Area.Y > 0 && !IsClickerWorking
                    ));
            }
        }
        #endregion

        #region Width
        private ICommand _widthUpCommand;
        public ICommand WidthUpCommand
        {
            get
            {
                return _widthUpCommand ?? (_widthUpCommand = new RelayCommand(
                    (o) => { Area.Width += _deltaValue; },
                    (o) => !IsClickerWorking
                    ));
            }
        }

        private ICommand _widthDownCommand;
        public ICommand WidthDownCommand
        {
            get
            {
                return _widthDownCommand ?? (_widthDownCommand = new RelayCommand(
                    (o) => { Area.Width -= _deltaValue; },
                    (o) => Area.WidthCanBeReduced() && !IsClickerWorking
                    ));
            }
        }
        #endregion

        #region Height
        private ICommand _heightUpCommand;
        public ICommand HeightUpCommand
        {
            get
            {
                return _heightUpCommand ?? (_heightUpCommand = new RelayCommand(
                    (o) => { Area.Height += _deltaValue; },
                    (o) => !IsClickerWorking
                    ));
            }
        }

        private ICommand _heightDownCommand;
        public ICommand HeightDownCommand
        {
            get
            {
                return _heightDownCommand ?? (_heightDownCommand = new RelayCommand(
                    (o) => { Area.Height -= _deltaValue; },
                    (o) => Area.HeightCanBeReduced()
                    ));
            }
        }
        #endregion

        private IMouseClicker GetCurrentClicker()
        {
            var clicker = _clickerFactory.CreateClicker(ClickDelay, ClickArea, ClickType);
            return clicker;
        }

        public void Dispose()
        {
            _subscribeContainer.UnsubscribeAll();
        }

        #region Delay Range
        public Range DelayRange { get; set; }

        private ICommand _delayBeetwenStartUpCommand;
        public ICommand DelayBeetwenStartUpCommand
        {
            get
            {
                return _delayBeetwenStartUpCommand ?? (_delayBeetwenStartUpCommand = new RelayCommand(
                    (o) => DelayRange.From += _deltaValue,
                    (o) => DelayRange.FromCanBeIncreased()
                    ));
            }
        }

        private ICommand _delayBeetwenStartDownCommand;
        public ICommand DelayBeetwenStartDownCommand
        {
            get
            {
                return _delayBeetwenStartDownCommand ?? (_delayBeetwenStartDownCommand = new RelayCommand(
                    (o) => { DelayRange.From -= _deltaValue; },
                    (o) => DelayRange.From > 0
                    ));
            }
        }

        private ICommand _delayBeetwenEndUpCommand;
        public ICommand DelayBeetwenEndUpCommand
        {
            get
            {
                return _delayBeetwenEndUpCommand ?? (_delayBeetwenEndUpCommand = new RelayCommand(
                    (o) => DelayRange.To += _deltaValue,
                    null
                    ));
            }
        }

        private ICommand _delayBeetwenEndDownCommand;
        public ICommand DelayBeetwenEndDownCommand
        {
            get
            {
                return _delayBeetwenEndDownCommand = new RelayCommand(
                    (o) => DelayRange.To -= _deltaValue,
                    (o) => DelayRange.ToCanBeReduced()
                    );
            }
        }
        #endregion

        #region Fixed delay
        public FixedDelay FixedDelay { get; set; }

        private ICommand _fixedDelayUpCommand;
        public ICommand FixedDelayUpCommand
        {
            get
            {
                return _fixedDelayUpCommand = new RelayCommand(
                    (o) => FixedDelay.Delay += _deltaValue,
                    null
                );
            }
        }

        private ICommand _fixedDelayDownCommand;
        public ICommand FixedDelayDownCommand
        {
            get
            {
                return _fixedDelayDownCommand ?? (_fixedDelayDownCommand = new RelayCommand(
                    (o) => FixedDelay.Delay -= _deltaValue,
                    (o) => FixedDelay.Delay > FixedDelay.FixedDelayMin
                    ));
            }
        }
        #endregion

        #region keyBinding
        private ICommand _bindKeyCommandStart;
        public ICommand BindKeyStartCommand
        {
            get
            {
                return _bindKeyCommandStart ?? (_bindKeyCommandStart = new RelayCommand(
                    (o) =>
                    {
                        IsKeyBindWorking = true;
                        _eventBroker.Raise(EventNames.KeyBindingStart, new ClickerEventArgs
                        {
                            Data = new object(),
                            Sender = this
                        });
                    },
                    null
                    ));
            }
        }
        #endregion
    }
}