using RandomAutoClicker.Infrastructure;
using RandomAutoClicker.Infrastructure.Events;
using RandomAutoClicker.Model;
using RandomAutoClicker.Model.Clicker;
using RandomAutoClicker.Model.Clicker.ClickBehaviour;
using RandomAutoClicker.Model.Clicker.Config;
using RandomAutoClicker.Model.Clicker.Interval;
using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace RandomAutoClicker.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IEventEntityFactory<ClickerEventArgs> _eventEntityFactory;
        private readonly IEventBroker<ClickerEventArgs> _eventBroker;
        private readonly ISubscribesContainer<ClickerEventArgs> _subscribeContainer;

        private IMouseClicker _clicker;
        private readonly Dispatcher _dispatcher;
        private readonly int _deltaValue;

        public MainWindowViewModel(Dispatcher dispatcher, IEventEntityFactory<ClickerEventArgs> eventEntityFactory)
        {
            _dispatcher = dispatcher;

            _eventEntityFactory = eventEntityFactory;
            _eventBroker = _eventEntityFactory.GetEventBroker();
            _subscribeContainer = _eventEntityFactory.GetSubscribeContainer(_eventBroker);

            _deltaValue = 10;
            _fixedDelayMin = 10;
            _fixedDelay = _fixedDelayMin;

            InitArea();
            InitDelayRange();
            Subscribe();
        }

        private void InitArea()
        {
            Area = new AreaRect(10, 10)
            {
                Height = 100,
                Width = 100
            };
        }

        private void InitDelayRange()
        {
            DelayRange = new Range
            {
                To = 100,
                From = 50
            };
        }

        //TODO: save all value on exit
        private void Subscribe()
        {
            _subscribeContainer.Subscribe(EventNames.KeyPressHandled, (u) =>
            {
                IsKeyBindWorking = false;
                BindedKey = u.Data.ToString();
                _eventBroker.Raise(EventNames.KeyBindingStop, new ClickerEventArgs { Data = u.Data, Sender = this });
            });

            _subscribeContainer.Subscribe(EventNames.ToggleClickerState, (u) =>
            {
                if (u.Data.ToString() == BindedKey)
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
            });
        }

        private string _bindedKey = "F3";
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

        #region Clicker resolver
        private IMouseClicker GetCurrentClicker()
        {
            var interval = GetCurrentInterval(ClickDelay);
            var area = GetCurrentArea(ClickArea);
            var clickBehaviour = GetCurrentClickBehaviour();

            return new Clicker(interval, area, clickBehaviour);
        }

        private BaseClickerConfig GetCurrentArea(ClickAreaEnum clickArea)
        {
            switch (clickArea)
            {
                case ClickAreaEnum.FullScreen:
                    return new FullScreenClickerConfig();
                case ClickAreaEnum.Area:
                    return new AreaClickerConfig(Area.X, Area.Width, Area.Y, Area.Height);
                default:
                    throw new NotImplementedException();
            }
        }

        private ClickBehaviourBase GetCurrentClickBehaviour()
        {
            switch (ClickType)
            {
                case ClickTypeEnum.None:
                    return new EmptyClickBehaviour();
                case ClickTypeEnum.LeftClick:
                    return new LeftButtonClickBehaviour();
                case ClickTypeEnum.RightClick:
                    return new RightButtonClickBehaviour();
                case ClickTypeEnum.LeftDoubleClick:
                    return new DoubleClickBehaviour(new LeftButtonClickBehaviour());
                case ClickTypeEnum.RightDoubleClick:
                    return new DoubleClickBehaviour(new RightButtonClickBehaviour());
                default:
                    throw new NotImplementedException();
            }
        }

        private IClickerInterval GetCurrentInterval(ClickDelayEnum clickDelay)
        {
            switch (clickDelay)
            {
                case ClickDelayEnum.Random:
                    return new RandomClickerInterval(DelayRange.From, DelayRange.To);
                case ClickDelayEnum.Fixed:
                    return new FixedClickerInterval(FixedDelay);
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

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
        private int _fixedDelayMin;
        private int _fixedDelay;
        public int FixedDelay
        {
            get { return _fixedDelay; }
            set
            {
                if (value < _fixedDelayMin)
                    value = _fixedDelayMin;

                _fixedDelay = value;
                InvalidateRequerySuggested();
                RaisePropertyChangedEvent(nameof(FixedDelay));
            }
        }

        private ICommand _fixedDelayUpCommand;
        public ICommand FixedDelayUpCommand
        {
            get
            {
                return _fixedDelayUpCommand = new RelayCommand(
                    (o) => FixedDelay += _deltaValue,
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
                    (o) => FixedDelay -= _deltaValue,
                    (o) => FixedDelay > _fixedDelayMin
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