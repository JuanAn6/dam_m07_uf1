using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace _20241021_BuscaMinas.View
{

    public sealed partial class UCBoard : UserControl
    {

        public event EventHandler OnGameOver;
        public event EventHandler OnGameWin;


        DispatcherTimer timer = null;
        public UCBoard()
        {
            this.InitializeComponent();
        }


        private int uncoverField;

        public int UncoverField
        {
            get { return uncoverField; }
            set { 
                uncoverField = value;
                if (uncoverField >= COLS * ROWS - MINES)
                {
                    Mode = MODE.WIN;
                }
            }
        }


        public int MarkedMinesNumber
        {
            get { return (int)GetValue(MarkedMinesNumberProperty); }
            set { SetValue(MarkedMinesNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkedMinesNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkedMinesNumberProperty =
            DependencyProperty.Register("MarkedMinesNumber", typeof(int), typeof(UCBoard), new PropertyMetadata(0));




        public int Seconds
        {
            get { return (int)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Seconds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(int), typeof(UCBoard), new PropertyMetadata(0));



        public enum MODE
        {
            IN_GAME,
            GAME_OVER,
            WIN,
            READY,
        }

        private MODE mode;

        public MODE Mode
        {
            get { return mode; }
            set
            {
                
                switch (value)
                {
                    case MODE.READY:
                        InitTauler();
                        ShowTauler();
                        MarkedMinesNumber = 0;
                        break;
                    case MODE.IN_GAME:
                        if(mode != MODE.IN_GAME)
                        {
                            if (timer!= null) timer.Stop();
                            StartTime();
                        }
                        break;
                    case MODE.GAME_OVER:
                        if (timer != null)
                        {
                            timer.Stop();
                        }
                        OnGameOver?.Invoke(this, EventArgs.Empty);
                        break;
                    case MODE.WIN: 
                        OnGameWin?.Invoke(this, EventArgs.Empty);
                        break;
                }

                mode = value;
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            Seconds++;
        }

        private void StartTime()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private const int ROWS = 16;
        private const int COLS = 16;
        private const int MINES = 2;
        private const int MINECODE = 9;
        private const int CELL_SIZE = 32;
        private const int FONT_SIZE = 15;

        private int[,] tauler;

        private Dictionary<Punt, Image> ImageList = new Dictionary<Punt, Image>();


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Mode = MODE.READY;
        }

        private void InitTauler()
        {
            tauler = new int[ROWS, COLS];

            int mines = 0;

            while (mines < MINES)
            {
                int row_int = new Random().Next(0, ROWS);
                int col_int = new Random().Next(0, COLS);

                if (tauler[row_int, col_int] != MINECODE)
                {
                    mines++;
                    tauler[row_int, col_int] = MINECODE;

                    IncrementNeighbor(row_int, col_int);
                }

            }

        }

        private void IncrementNeighbor(int row_int, int col_int)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int k = -1; k < 2; k++)
                {
                    if ((k != 0 || i != 0)
                        && row_int + i >= 0
                        && col_int + k >= 0
                        && i + row_int < ROWS
                        && k + col_int < COLS
                        && tauler[row_int + i, col_int + k] != MINECODE
                        )
                    {
                        tauler[row_int + i, col_int + k]++;
                    }
                }
            }
        }

        private void ShowTauler()
        {
            
            grid.Children.Clear();
            ImageList.Clear();
            canvas.Children.Clear();
            if (grid.RowDefinitions.Count == 0)
            {
                for (int row = 0; row < ROWS; row++)
                {
                    RowDefinition rowDefinition = new RowDefinition();
                    rowDefinition.Height = new GridLength(CELL_SIZE);
                    grid.RowDefinitions.Add(rowDefinition);
                }

                for (int col = 0; col < COLS; col++)
                {
                    ColumnDefinition colDefinition = new ColumnDefinition();
                    colDefinition.Width = new GridLength(CELL_SIZE);
                    grid.ColumnDefinitions.Add(colDefinition);
                }
            }

            /*
            grid.ColumnSpacing = 3;
            grid.RowSpacing = 3;
            */

            

            //DIBUXAR LINIES
            int x1 = 0;
            int x2 = ROWS * CELL_SIZE;

            for (int row = 0; row <= ROWS; row++)
            {
                int y = row * CELL_SIZE;
                Line l = new Line();
                l.X1 = x1;
                l.Y1 = y;
                l.X2 = x2;
                l.Y2 = y;
                l.StrokeThickness = 1;
                l.Stroke = new SolidColorBrush(Colors.White);
                canvas.Children.Add(l);

                l = new Line();
                l.X1 = y;
                l.Y1 = x1;
                l.X2 = y;
                l.Y2 = x2;
                l.StrokeThickness = 1;
                l.Stroke = new SolidColorBrush(Colors.White);
                canvas.Children.Add(l);
            }

            grid.Children.Add (canvas);

            int mineNumber = 0;

            //PRINTAR ELS NUMEROS I MINES
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {


                    TextBlock b = new TextBlock();

                    b.FontFamily = new FontFamily("MINE-SWEEPER");
                    Grid.SetColumn(b, col);
                    Grid.SetRow(b, row);
                    grid.Children.Add(b);

                    mineNumber = tauler[row, col];


                    b.Text = getTextFromNumber(mineNumber);

                    b.Foreground = getColorFromNumber(mineNumber);
                    b.FontSize = FONT_SIZE;
                    b.VerticalAlignment = VerticalAlignment.Center;
                    b.HorizontalAlignment = HorizontalAlignment.Center;
                    b.Padding = new Thickness(0);

                    //b.Visibility = Visibility.Collapsed;

                    //IMG
                    //<Image Source="/Assets/img/title.png">
                    Image img = new Image();
                    BitmapImage i = new BitmapImage();
                    i.UriSource = new Uri("ms-appx://20241021_BuscaMinas/Assets/img/tile.png");
                    img.Source = i;

                    //img.Opacity = 0.5;

                    //img.Width = CELL_SIZE;
                    //img.Height = CELL_SIZE;
                    img.Stretch = Stretch.UniformToFill;

                    Grid.SetColumn(img, col);
                    Grid.SetRow(img, row);

                    grid.Children.Add(img);

                    img.Tapped += FieldPressed;

                    img.RightTapped += FieldRightPress;

                    Punt p = new Punt(row, col);
                    p.IsFlagged = false;
                    p.MinaText = b;

                    img.Tag = p;

                    ImageList.Add(p, img);
                }
            }

        }

        private void FieldRightPress(object sender, RightTappedRoutedEventArgs e)
        {
            if (Mode == MODE.GAME_OVER) return;
            mode = MODE.IN_GAME;

            Image img = (Image)sender;
            Punt p = (Punt)img.Tag;

            BitmapImage i = new BitmapImage();
            if (p.IsFlagged)
            {
                i.UriSource = new Uri("ms-appx://20241021_BuscaMinas/Assets/img/tile.png");
                MarkedMinesNumber--;
            }
            else
            {
                i.UriSource = new Uri("ms-appx://20241021_BuscaMinas/Assets/img/flag.png");
                MarkedMinesNumber++;
            }

            p.IsFlagged = !p.IsFlagged;
            img.Tag = p;
            img.Source = i;

        }

        private void FieldPressed(object sender, TappedRoutedEventArgs e)
        {
            if (Mode == MODE.GAME_OVER) return;
            mode = MODE.IN_GAME;

            Image image = (Image)sender;
            Punt p = (Punt)image.Tag;

            if (p.IsFlagged) return; // no podem clicar sobre banderas

            image.Visibility = Visibility.Collapsed;
            p.MinaText.Visibility = Visibility.Visible;

            CheckFieldsMines(p);
        }

        private void CheckFieldsMines(Punt p)
        {

            if (tauler[p.row, p.col] != MINECODE)
            {
                DestaparCasella(p);
            }
            else
            {
                Mode = MODE.GAME_OVER;
            }

        }

        private void DestaparCasella(Punt p)
        {

            if (p.col < 0 || p.col >= COLS || p.row < 0 || p.row >= ROWS) { return; }

            UncoverField++;

            if (p.IsFlagged)
            {
                MarkedMinesNumber--;
            }

            ImageList[p].Visibility = Visibility.Collapsed;
            p.MinaText.Visibility = Visibility.Visible;

            //Si ja ha pasat el primer nivell i es un numero destapa
            if (tauler[p.row, p.col] > 0) return;

            int[,] mods = {
                {0, 1 },
                {0 , -1},

                {1 , 0}, 
                //{1 , 1},
                //{1, -1 },

                //{-1, 1},
                {-1, 0 },
                //{-1, -1},

            };

            for (int i = 0; i < mods.GetLength(0); i++)
            {

                Punt n = new Punt(p.row + mods[i, 0], p.col + mods[i, 1]);

                if (!ImageList.ContainsKey(n)) continue;
                if (ImageList[n].Visibility == Visibility.Collapsed) continue;

                Punt complet = ImageList[n].Tag as Punt;

                DestaparCasella(complet);

            }
        }

        private string getTextFromNumber(int mineNumber)
        {
            switch (mineNumber)
            {
                case 0: return "";
                case MINECODE: return "*";
                default: return mineNumber.ToString();
            }
        }

        private Brush getColorFromNumber(int mineNumber)
        {
            Color c;

            switch (mineNumber)
            {
                case 1: c = Colors.Blue; break;
                case 2: c = Colors.Green; break;
                case MINECODE: c = Colors.White; break;
                default: c = Colors.Red; break;
            }

            return new SolidColorBrush(c);
        }

    }
}

