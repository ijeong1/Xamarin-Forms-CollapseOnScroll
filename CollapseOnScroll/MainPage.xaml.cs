using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CollapseOnScroll
{
    public partial class MainPage : ContentPage
    {
        private Animation _lastRowAnimation;
        private readonly double _initialLastRowHeight;
        private readonly RowDefinition _lastRowDefinition;
        private bool _isScrolled;
        private bool _isFirstRun;
        public List<string> Items { get; set; } = new List<string>();

        public MainPage()
        {
            InitializeComponent();

            _lastRowDefinition = DashboardBarGrid.RowDefinitions[0];
            _initialLastRowHeight = _lastRowDefinition.Height.Value;
            _isFirstRun = true;
            Items.Add("A");
            Items.Add("B");
            Items.Add("C");
            Items.Add("D");
            Items.Add("E");
            Items.Add("F");
            Items.Add("G");
            Items.Add("H");
            Items.Add("I");
            Items.Add("J");
            Items.Add("K");
            Items.Add("L");
            Items.Add("M");
            Items.Add("O");
            Items.Add("P");
            Items.Add("Q");
            Items.Add("R");
            Items.Add("S");
            Items.Add("T");
            Items.Add("U");
            Items.Add("V");
            Items.Add("W");
            Items.Add("X");
            Items.Add("Y");
            Items.Add("Z");

            BindingContext = this;
        }

        private void listView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var listView = (ListView)sender;
            if (e.ScrollY > 50) _isFirstRun = false;
            if (_isScrolled && e.ScrollY == 0 && _lastRowDefinition?.Height.Value < _initialLastRowHeight)
            {
                listView.IsEnabled = false;

                _lastRowAnimation = new Animation(
                    (d) => _lastRowDefinition.Height = new GridLength(d.Clamp(50, double.MaxValue)),
                    _lastRowDefinition.Height.Value, _initialLastRowHeight, Easing.Linear, () => _lastRowAnimation = null);
                _lastRowAnimation.Commit(this, "Animation", 16, 500, Easing.Linear, (v, c) =>
                {
                    _isScrolled = false;
                    listView.IsEnabled = true;
                });
            }
            // Hide the rows
            else if (!_isScrolled && _lastRowDefinition?.Height.Value >= _initialLastRowHeight && !_isFirstRun)
            {
                listView.IsEnabled = false;

                _lastRowAnimation = new Animation(
                    (d) => _lastRowDefinition.Height = new GridLength(d.Clamp(0, double.MaxValue)),
                    _initialLastRowHeight, 0, Easing.Linear, () => _lastRowAnimation = null);
                _lastRowAnimation.Commit(this, "Animation", 16, 500, Easing.Linear, (v, c) =>
                {
                    _isScrolled = true;
                    listView.IsEnabled = true;
                });
            }
        }
    }
}
