using RuletaV1.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace RuletaV1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        public MainWindow()
        {
            InitializeComponent();

            int time = 66;
            Progress_Bar(time);

            tbTimer(time);

            int kredit = 1000;

            //Views.Okno1.MouseDownEvent.
            //Random random = new Random();
            //int randomSt = random.Next(0, 36);
            int[,] tabStevila ={ { 0, 1, 7, 9 , 36 }, { 20, 10,500, 100, 300}};
            RuletaStatStevilka(7, kredit, tabStevila);

            int [] tabSpodaj ={100,400};
            RuletaStatBarva(kredit, tabSpodaj);
            RuletaStatSodoLiho(21, kredit, tabSpodaj);
            RuletaStatSpodGor(10, kredit, tabSpodaj);



            int[] tabVecStevil = { 1, 2, 3, 4, 5, 6 };

            int[] tabVrstica = {200, 30, 50};
            RuletaStatVrstica(3, kredit, tabVrstica);

            RuletaStatStolpec(12, kredit, tabVrstica);
        }



        //OKNA-CLIENT
        private void Okno1_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new Okno1VM();
        }

        private void Okno2_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new Okno2VM();
        }

        private void Okno3_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new Okno3VM();
        }

        //PROGRESS BAR
        private void Progress_Bar(int i)
        {

            Duration duration = new Duration(TimeSpan.FromSeconds(i));
            DoubleAnimation doubleanimation = new DoubleAnimation(100, duration);
            pbStatus.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);

        }
        //TIMER
        private void tbTimer(int time)
        {
            _time = TimeSpan.FromSeconds(time);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }


        //KLIK STEVILO
        private void RuletaStatStevilka(int dobSt, int kredit, int [,] tabela)
        {
            int k = tabela.Length/2 ;

            for (int i = 0;i<k; i++) {

                if (tabela[0, i] == dobSt)
                {
                    kredit = kredit + (tabela[1, 2] * 36);
                    ruletaStat.Text = tabela[0, 2] + " stava   " + tabela[1, 2] + "   " + kredit;
                }
                else
                {
                    kredit = kredit - tabela[1, i];
                }

            }

        }


        //KLIK BARVA
        private void RuletaStatBarva(int kredit, int [] barvaStava)
        {

            if (ruletaStat.Background.ToString() == "#FFFF0000")
            {
                kredit = kredit + barvaStava[0] * 2;
                kredit = kredit - barvaStava[1];
                ruletaStat2.Text = "je rdeča" + kredit.ToString();
            }
            else {
                kredit = kredit + barvaStava[1] * 2;
                kredit = kredit - barvaStava[0];
                ruletaStat2.Text = "je črna" + kredit.ToString();
            }
        }


        //KLIK SODO LIHO
        private void RuletaStatSodoLiho(int dobSt, int kredit, int []sodoLiho)
        {

            if (dobSt%2==0)
            {
                kredit = kredit + sodoLiho[0] * 2;
                kredit = kredit - sodoLiho[1];
                ruletaStat5.Text = "sodo" + kredit.ToString();
            }
            else
            {
                kredit = kredit + sodoLiho[1] * 2;
                kredit = kredit - sodoLiho[0];
                ruletaStat5.Text = "liho" + kredit.ToString();
            }
        }


        //KLIK 1-18 ali 19-36
        private void RuletaStatSpodGor(int dobSt, int kredit, int[] sodoLiho)
        {

            if (dobSt > 0 && dobSt < 19 )
            {
                kredit = kredit + sodoLiho[0] * 2;
                kredit = kredit - sodoLiho[1];
                ruletaStat6.Text = "-1-18-" + kredit.ToString();
            }
            else
            {
                kredit = kredit + sodoLiho[1] * 2;
                kredit = kredit - sodoLiho[0];
                ruletaStat6.Text = "-19-36-" + kredit.ToString();
            }
        }


        //KLIK VRSTICA
        private void RuletaStatVrstica(int dobSt, int kredit, int [] vrsticaStava)
        {
            //dobitna št = vrstica?  kredit=kredit + dobitna vrstica * 3 - vsota drugih 2 vrstic

                if (dobSt % 3 == 0)
                {
                    kredit = kredit + vrsticaStava[0] * 3;
                    kredit = kredit - vrsticaStava[1] - vrsticaStava[2];
                    ruletaStat3.Text = "vrstica 1---" + kredit.ToString();
                }
                else if (dobSt % 3 == 2)
                {
                    kredit = kredit + vrsticaStava[1] * 3;
                    kredit = kredit - vrsticaStava[0] - vrsticaStava[2];
                    ruletaStat3.Text = "vrstica 2---" + kredit.ToString();
                }
                else if (dobSt % 3 == 1)
                {
                    kredit = kredit + (vrsticaStava[2] * 3);
                    kredit = kredit - vrsticaStava[0] - vrsticaStava[1];
                    ruletaStat3.Text = "vrstica 3---" + kredit.ToString();

                }
        }

        //KLIK STOLPEC
        private void RuletaStatStolpec(int dobSt, int kredit, int[] vrsticaStava)
        {
            //dobitna št = stolpec?  kredit=kredit + dobitni stolpec * 3 - vsota drugih 2 stolpcev

            if (dobSt >0 && dobSt < 13)
            {
                kredit = kredit + vrsticaStava[0] * 3;
                kredit = kredit - vrsticaStava[1] - vrsticaStava[2];
                ruletaStat4.Text = "stolpec 1---" + kredit.ToString();
            }
            else if (dobSt >12 && dobSt < 25)
            {
                kredit = kredit + vrsticaStava[1] * 3;
                kredit = kredit - vrsticaStava[0] - vrsticaStava[2];
                ruletaStat4.Text = "stolpec 2---" + kredit.ToString();
            }
            else if (dobSt > 24 && dobSt < 37)
            {
                kredit = kredit + (vrsticaStava[2] * 3);
                kredit = kredit - vrsticaStava[0] - vrsticaStava[1];
                ruletaStat4.Text = "stolpec 3---" + kredit.ToString();

            }
        }
    }
}
