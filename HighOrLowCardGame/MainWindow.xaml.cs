using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using FontFamily = System.Windows.Media.FontFamily;
using FontStyle = System.Windows.FontStyle;
using Image = System.Windows.Controls.Image;
using Path = System.Windows.Shapes.Path;
using Point = System.Windows.Point;
using MaterialDesignColors;
using MaterialDesignThemes;
using System.Diagnostics;

namespace HighOrLowCardGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string playerName = "";
        private int playerBalance = 0;
        private int cardDrawnCount = 0;
        private int numOfCorrectGuesses = 0;
        private Random rand = new Random();
        private Game game;
        private Storyboard storyboard1;
        private Storyboard storyboard2;
        private Storyboard cardFlipStory;
        private Storyboard TextBlockAnimationStory;
        private MediaPlayer soundFx;
        private MediaPlayer backgroundAudio;
        private bool hasVolume = true;

        public MainWindow()
        {
            InitializeComponent();
            MediaPlayer.Play();
            soundFx = new MediaPlayer();
            backgroundAudio = new MediaPlayer();
        }

        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            waitForSplashScreen(MediaPlayer.NaturalDuration);
        }

        private async void waitForSplashScreen(Duration naturalDuration)
        {
            await Task.Delay(naturalDuration.TimeSpan);
            Navigator.SelectedIndex = 1;
        }

        private void startAnimateChip(Image chip)
        {

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            chip.RenderTransformOrigin = new Point(0.5, 0.5);
            chip.RenderTransform = scale;

            DoubleAnimation growXAnimation = new DoubleAnimation();
            growXAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growXAnimation.From = 1;
            growXAnimation.To = 1.2;
            growXAnimation.AutoReverse = true;
            growXAnimation.RepeatBehavior = RepeatBehavior.Forever;
            storyboard1.Children.Add(growXAnimation);
            Storyboard.SetTargetProperty(growXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growXAnimation, chip);

            DoubleAnimation growYAnimation = new DoubleAnimation();
            growYAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growYAnimation.From = 1;
            growYAnimation.To = 1.2;
            growYAnimation.AutoReverse = true;
            growYAnimation.RepeatBehavior = RepeatBehavior.Forever;
            storyboard2.Children.Add(growYAnimation);
            Storyboard.SetTargetProperty(growYAnimation, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(growYAnimation, chip);

            storyboard1.Begin();
            storyboard2.Begin();
        }

        private void stopAnimateChip()
        {
            if ((storyboard1 != null) || (storyboard2 != null))
            {
                storyboard1.Stop();
                storyboard2.Stop();
            }
            storyboard1 = new Storyboard();
            storyboard2 = new Storyboard();
        }

        private void Chip1_MouseDown(object sender, EventArgs e)
        {
            if (game.CanAffordToWager(game.Wager + 1))
            {
                SelectArrow1.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                startAnimateChip(Chip1);
                game.Wager += 1;
                WagerLabel.Content = String.Format("{0} has bet ${1}", game.Player1Name, game.Wager);
                formatWager(game.Wager);
                setArrowActive(true);
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/select.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            } else
            {
                foo.Visibility = Visibility.Visible;
                foo.MessageQueue.Enqueue("You don't have sufficient cash!");
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/crackle.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
        }

        private void Chip2_MouseDown(object sender, EventArgs e)
        {
            if (game.CanAffordToWager(game.Wager + 5))
            {
                SelectArrow2.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                startAnimateChip(Chip2);
                game.Wager += 5;
                WagerLabel.Content = String.Format("{0} has bet ${1}", game.Player1Name, game.Wager);
                formatWager(game.Wager);
                setArrowActive(true);
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/select.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
            else
            {
                foo.Visibility = Visibility.Visible;
                foo.MessageQueue.Enqueue("You don't have sufficient cash!");
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/crackle.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
        }
        private void Chip3_MouseDown(object sender, EventArgs e)
        {
            if (game.CanAffordToWager(game.Wager + 25))
            {
                SelectArrow3.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                startAnimateChip(Chip3);
                game.Wager += 25;
                WagerLabel.Content = String.Format("{0} has bet ${1}", game.Player1Name, game.Wager);
                formatWager(game.Wager);
                setArrowActive(true);
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/select.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
            else
            {
                foo.Visibility = Visibility.Visible;
                foo.MessageQueue.Enqueue("You don't have sufficient cash!");
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/crackle.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
        }
        private void Chip4_MouseDown(object sender, EventArgs e)
        {
            if (game.CanAffordToWager(game.Wager + 100))
            {
                SelectArrow4.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                startAnimateChip(Chip4);
                game.Wager += 100;
                WagerLabel.Content = String.Format("{0} has bet ${1}", game.Player1Name, game.Wager);
                formatWager(game.Wager);
                setArrowActive(true);
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/select.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
            else
            {
                foo.Visibility = Visibility.Visible;
                foo.MessageQueue.Enqueue("You don't have sufficient cash!");
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/crackle.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
        }

        private void Chip5_MouseDown(object sender, EventArgs e)
        {
            if (game.CanAffordToWager(game.Wager + 500))
            {
                SelectArrow5.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                startAnimateChip(Chip5);
                game.Wager += 500;
                WagerLabel.Content = String.Format("{0} has bet ${1}", game.Player1Name, game.Wager);
                formatWager(game.Wager);
                setArrowActive(true);
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/select.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
            else
            {
                foo.Visibility = Visibility.Visible;
                foo.MessageQueue.Enqueue("You don't have sufficient cash!");
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/crackle.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            backgroundAudio.MediaEnded -= Background_MediaEnded;
            Navigator.SelectedIndex = 1;
        }

        private void setArrowActive(bool isActive)
        {
            if (isActive)
            {
                UpArrow.Source = new BitmapImage(new Uri("pack://application:,,,/images/up-blue-128.png"));
                DownArrow.Source = new BitmapImage(new Uri("pack://application:,,,/images/down-128.png"));
            }
            else
            {
                UpArrow.Source = new BitmapImage(new Uri("pack://application:,,,/images/up_disabled-128.png"));
                DownArrow.Source = new BitmapImage(new Uri("pack://application:,,,/images/down_disabled-128.png"));
            }
        }

        private void formatWager(int wagerAmount)
        {
            switch (wagerAmount)
            {
                case 5:
                    SelectArrow1.Source = null;
                    SelectArrow2.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                    SelectArrow3.Source = null;
                    SelectArrow4.Source = null;
                    SelectArrow5.Source = null;
                    stopAnimateChip();
                    startAnimateChip(Chip2);
                    setArrowActive(true);
                    break;
                case 25:
                    SelectArrow1.Source = null;
                    SelectArrow2.Source = null;
                    SelectArrow3.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                    SelectArrow4.Source = null;
                    SelectArrow5.Source = null;
                    stopAnimateChip();
                    startAnimateChip(Chip3);
                    setArrowActive(true);
                    break;
                case 100:
                    SelectArrow1.Source = null;
                    SelectArrow2.Source = null;
                    SelectArrow3.Source = null;
                    SelectArrow4.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                    SelectArrow5.Source = null;
                    stopAnimateChip();
                    startAnimateChip(Chip4);
                    setArrowActive(true);
                    break;
                case 500:
                    SelectArrow1.Source = null;
                    SelectArrow2.Source = null;
                    SelectArrow3.Source = null;
                    SelectArrow4.Source = null;
                    SelectArrow5.Source = new BitmapImage(new Uri("pack://application:,,,/images/arrow-up-32.png"));
                    stopAnimateChip();
                    startAnimateChip(Chip5);
                    setArrowActive(true);
                    break;
                default:
                    break;
            }
        }

        private void Clear_Bet_Button_Click(object sender, RoutedEventArgs e)
        {
            game.Wager = 0;
            SelectArrow1.Source = null;
            SelectArrow2.Source = null;
            SelectArrow3.Source = null;
            SelectArrow4.Source = null;
            SelectArrow5.Source = null;
            stopAnimateChip();
            setArrowActive(false);
            WagerLabel.Content = "";

        }

        private void Leave_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            disableUI();
            backgroundAudio.MediaEnded -= Background_MediaEnded;
            Navigator.SelectedIndex = 1;
            if (hasVolume)
            {
                backgroundAudio.Stop();
            }
        }

        private void All_In_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectArrow1.Source = null;
            SelectArrow2.Source = null;
            SelectArrow3.Source = null;
            SelectArrow4.Source = null;
            SelectArrow5.Source = null;
            stopAnimateChip();
            setArrowActive(false);
            game.Wager = game.Balance;
            WagerLabel.Content = String.Format("{0} has bet ${1}", game.Player1Name, game.Wager);
            setArrowActive(true);
        }

        private void UpArrow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (game.HasWager())
            {
                Game.GuessResult result = game.PlayTurn(Game.Guess.High);
                if (result != null)
                {
                    try
                    {
                        flipCard(new Uri(String.Format("pack://application:,,,/images/{0}.jpg", result.flippedCard.DisplayName)));
                    } catch (NullReferenceException ne)
                    {
                        Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                    catch (IOException io)
                    {
                        Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                    switch (result.isGuessCorrect)
                    {
                        case true:
                            AnimateTextBlock("You Won!");
                            ++this.numOfCorrectGuesses;
                            if (hasVolume)
                            {
                                soundFx.Open(new Uri("sounds/mallet_hit.mp3", UriKind.Relative));
                                soundFx.Play();
                            }
                            break;
                        default:
                            AnimateTextBlock("You Lost!");
                            if (hasVolume)
                            {
                                soundFx.Open(new Uri("sounds/record_scratch2.mp3", UriKind.Relative));
                                soundFx.Play();
                            }
                            break;
                    }

                    game.Wager = 0;
                    SelectArrow1.Source = null;
                    SelectArrow2.Source = null;
                    SelectArrow3.Source = null;
                    SelectArrow4.Source = null;
                    SelectArrow5.Source = null;
                    stopAnimateChip();
                    WagerLabel.Content = "";
                    if (result.isDeckEmpty) {
                        flipCardClose();
                        disableUI();
                        AnimateTextBlock("Done!");
                        Card.MouseDown += new MouseButtonEventHandler(Card_MouseDown);
                        replayImage.MouseDown += new MouseButtonEventHandler(Card_MouseDown);
                        replayImage.Source = new BitmapImage(new Uri("pack://application:,,,/images/replay_white_72.png"));
                        if (hasVolume)
                        {
                            backgroundAudio.MediaEnded -= Background_MediaEnded;
                            backgroundAudio.Open(new Uri("sounds/done.mp3", UriKind.Relative));
                            backgroundAudio.Play();
                        }
                    } else if ( result.isBalanceZero)
                    {
                        flipCardClose();
                        disableUI();
                        AnimateTextBlock("Bust!");
                        Card.MouseDown += new MouseButtonEventHandler(Card_MouseDown);
                        replayImage.MouseDown += new MouseButtonEventHandler(Card_MouseDown);
                        replayImage.Source = new BitmapImage(new Uri("pack://application:,,,/images/replay_white_72.png"));
                        if (hasVolume)
                        {
                            backgroundAudio.MediaEnded -= Background_MediaEnded;
                            backgroundAudio.Open(new Uri("sounds/record_scratch3.mp3", UriKind.Relative));
                            backgroundAudio.Play();
                        }
                    } else {
                        flipCardClose();
                    }
                    BalanceTextBlock.Text = String.Format("{0} has ${1} remaining", game.Player1Name, game.Balance);
                    ++this.cardDrawnCount;
                    StatusTextBlock.Text = String.Format("Total Guesses: {0}/52 ({1})", this.cardDrawnCount, ((float)this.numOfCorrectGuesses/(float)this.cardDrawnCount).ToString("p2"));
                }
            }
            else
            {
                foo.Visibility = Visibility.Visible;
                foo.MessageQueue.Enqueue("You haven't placed a bet!");
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/crackle.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
        }

        private void DownArrow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (game.HasWager())
            {
                Game.GuessResult result = game.PlayTurn(Game.Guess.Low);
                if (result != null)
                {
                    try
                    {
                        flipCard(new Uri(String.Format("pack://application:,,,/images/{0}.jpg", result.flippedCard.DisplayName)));
                    }
                    catch (NullReferenceException ne)
                    {
                        Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                    catch (IOException io)
                    {
                        Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                    switch (result.isGuessCorrect)
                    {
                        case true:
                            AnimateTextBlock("You Won!");
                            ++this.numOfCorrectGuesses;
                            if (hasVolume)
                            {
                                soundFx.Open(new Uri("sounds/mallet_hit.mp3", UriKind.Relative));
                                soundFx.Play();
                            }
                            break;
                        default:
                            AnimateTextBlock("You Lost!");
                            if (hasVolume)
                            {
                                soundFx.Open(new Uri("sounds/record_scratch2.mp3", UriKind.Relative));
                                soundFx.Play();
                            }
                            break;
                    }

                    game.Wager = 0;
                    SelectArrow1.Source = null;
                    SelectArrow2.Source = null;
                    SelectArrow3.Source = null;
                    SelectArrow4.Source = null;
                    SelectArrow5.Source = null;
                    stopAnimateChip();
                    WagerLabel.Content = "";
                    if (result.isDeckEmpty)
                    {
                        flipCardClose();
                        disableUI();
                        AnimateTextBlock("Done!");
                        Card.MouseDown += new MouseButtonEventHandler(Card_MouseDown);
                        replayImage.MouseDown += new MouseButtonEventHandler(Card_MouseDown);
                        replayImage.Source = new BitmapImage(new Uri("pack://application:,,,/images/replay_white_72.png"));
                        if (hasVolume)
                        {
                            backgroundAudio.MediaEnded -= Background_MediaEnded;
                            backgroundAudio.Open(new Uri("sounds/done.mp3", UriKind.Relative));
                            backgroundAudio.Play();
                        }
                    }
                    else if (result.isBalanceZero)
                    {
                        flipCardClose();
                        disableUI();
                        AnimateTextBlock("Bust!");
                        Card.MouseDown += new MouseButtonEventHandler(Card_MouseDown);
                        replayImage.MouseDown += new MouseButtonEventHandler(Card_MouseDown);
                        replayImage.Source = new BitmapImage(new Uri("pack://application:,,,/images/replay_white_72.png"));
                        if (hasVolume)
                        {
                            backgroundAudio.MediaEnded -= Background_MediaEnded;
                            backgroundAudio.Open(new Uri("sounds/record_scratch3.mp3", UriKind.Relative));
                            backgroundAudio.Play();
                        }
                    }
                    else
                    {
                        flipCardClose();
                    }
                    BalanceTextBlock.Text = String.Format("{0} has ${1} remaining", game.Player1Name, game.Balance);
                    ++this.cardDrawnCount;
                    StatusTextBlock.Text = String.Format("Total Guesses: {0}/52 ({1})", this.cardDrawnCount, ((float)this.numOfCorrectGuesses / (float)this.cardDrawnCount).ToString("p2"));
                }
            }
            else
            {
                foo.Visibility = Visibility.Visible;
                foo.MessageQueue.Enqueue("You haven't placed a bet!");
                if (hasVolume)
                {
                    soundFx.Open(new Uri("sounds/crackle.mp3", UriKind.Relative));
                    soundFx.Play();
                }
            }
        }

        private void disableUI()
        {
            Clear_Bet_Button.IsEnabled = false;
            All_In_Button.IsEnabled = false;
            VolumeToggle.IsEnabled = false;
            Chip1.MouseDown -= Chip1_MouseDown;
            Chip2.MouseDown -= Chip2_MouseDown;
            Chip3.MouseDown -= Chip3_MouseDown;
            Chip4.MouseDown -= Chip4_MouseDown;
            Chip5.MouseDown -= Chip5_MouseDown;
            UpArrow.MouseDown -= UpArrow_MouseDown;
            DownArrow.MouseDown -= DownArrow_MouseDown;
        }

        private void enableUI()
        {
            Clear_Bet_Button.IsEnabled = true;
            All_In_Button.IsEnabled = true;
            VolumeToggle.IsEnabled = true;
            Chip1.MouseDown += new MouseButtonEventHandler(Chip1_MouseDown);
            Chip2.MouseDown += new MouseButtonEventHandler(Chip2_MouseDown);
            Chip3.MouseDown += new MouseButtonEventHandler(Chip3_MouseDown);
            Chip4.MouseDown += new MouseButtonEventHandler(Chip4_MouseDown);
            Chip5.MouseDown += new MouseButtonEventHandler(Chip5_MouseDown);
            UpArrow.MouseDown += new MouseButtonEventHandler(UpArrow_MouseDown);
            DownArrow.MouseDown += new MouseButtonEventHandler(DownArrow_MouseDown);
        }

        private void flipCard(Uri uri)
        {
            cardFlipStory = new Storyboard();
            DoubleAnimation flipAnimation = new DoubleAnimation();
            flipAnimation.Duration = TimeSpan.FromMilliseconds(500);
            flipAnimation.To = -1;
            flipAnimation.FillBehavior = FillBehavior.Stop;
            cardFlipStory.Children.Add(flipAnimation);
            Storyboard.SetTargetProperty(flipAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(flipAnimation, Card);

            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.Duration = TimeSpan.FromMilliseconds(500);
            opacityAnimation.From = 1;
            opacityAnimation.To = 0;
            opacityAnimation.AutoReverse = true;
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            Storyboard.SetTarget(opacityAnimation, Card);
            cardFlipStory.Children.Add(opacityAnimation);

            ObjectAnimationUsingKeyFrames keyframe = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(keyframe, new PropertyPath(Image.SourceProperty));
            Storyboard.SetTarget(keyframe, Card);
            DiscreteObjectKeyFrame d1 = new DiscreteObjectKeyFrame();
            d1.KeyTime = TimeSpan.FromMilliseconds(500);
            d1.Value = new BitmapImage(uri);
            keyframe.KeyFrames.Add(d1);
            cardFlipStory.Children.Add(keyframe);

            cardFlipStory.Begin();
        }

        private async void flipCardClose()
        {
            this.IsHitTestVisible = false;
            await Task.Delay(2000);
            flipCard(new Uri(this.getCardCover(game.getCardCover())));
            ResultTextBlock.Text = "";
            setArrowActive(false);
            this.IsHitTestVisible = true;
        }

        private void AnimateTextBlock(string TextBlockContent)
        {
            TextBlockAnimationStory = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(animation, ResultTextBlock);
            animation.Duration = TimeSpan.FromSeconds(1);
            animation.From = 10;
            animation.To = 1;
            BounceEase easingFunction = new BounceEase();
            easingFunction.Bounces = 3;
            easingFunction.EasingMode = EasingMode.EaseOut;
            easingFunction.Bounciness = 2;
            animation.EasingFunction = easingFunction;
            TextBlockAnimationStory.Children.Add(animation);
            ResultTextBlock.Text = TextBlockContent;
            TextBlockAnimationStory.Begin();
        }

        private void PlayerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PlayerNameTextBox.Text))
            {
                if (FirstPageNextButton != null)
                    FirstPageNextButton.IsEnabled = false;
            }
            else
            {
                if (FirstPageNextButton != null)
                    FirstPageNextButton.IsEnabled = true;
            }
        }

        private void PlayerBalanceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isNumeric = int.TryParse(PlayerBalanceTextBox.Text, out int playerBalance);
            if (!isNumeric)
            {
                if (FirstPageNextButton != null)
                {
                    FirstPageNextButton.IsEnabled = false;
                }
            }
            else if (isNumeric && (playerBalance > 500) || playerBalance < 1)
            {
                playerBalance = 500;
                PlayerBalanceTextBox.Text = "500";
                if (FirstPageNextButton != null && !String.IsNullOrWhiteSpace(PlayerNameTextBox.Text))
                {
                    FirstPageNextButton.IsEnabled = true;
                }
                else if (FirstPageNextButton != null)
                {
                    FirstPageNextButton.IsEnabled = false;
                }
            }
            else
            {
                if (FirstPageNextButton != null && !String.IsNullOrWhiteSpace(PlayerNameTextBox.Text))
                {
                    FirstPageNextButton.IsEnabled = true;
                }
                else if (FirstPageNextButton != null)
                {
                    FirstPageNextButton.IsEnabled = false;
                }
            }
        }

        private void FirstPageNextButton_Click(object sender, RoutedEventArgs e)
        {
            var isNumeric = int.TryParse(PlayerBalanceTextBox.Text, out int playerBalance);
            this.playerName = PlayerNameTextBox.Text;
            if (!isNumeric || (isNumeric && playerBalance > 500) || (isNumeric && playerBalance < 1))
            {
                playerBalance = 500;
            }
            if (String.IsNullOrWhiteSpace(PlayerNameTextBox.Text))
            {
                this.playerName = "Player 1";
            }
            game = new Game(this.playerName, playerBalance);
            Card.Source = new BitmapImage(new Uri(this.getCardCover(game.getCardCover())));
            BalanceTextBlock.Text = String.Format("{0} has ${1} remaining", game.Player1Name, game.Balance);
            UpArrow.Source = new BitmapImage(new Uri("pack://application:,,,/images/up_disabled-128.png"));
            DownArrow.Source = new BitmapImage(new Uri("pack://application:,,,/images/down_disabled-128.png"));
            storyboard1 = new Storyboard();
            storyboard2 = new Storyboard();
            foo.Visibility = Visibility.Collapsed;
            replayImage.Source = null;
            Card.MouseDown -= Card_MouseDown;
            replayImage.MouseDown -= Card_MouseDown;
            enableUI();
            StatusTextBlock.Text = "";
            this.cardDrawnCount = 0;
            this.numOfCorrectGuesses = 0;
            if (hasVolume)
            {
                backgroundAudio.Open(new Uri("sounds/CasinoJazz.wav", UriKind.Relative));
                backgroundAudio.Play();
            }
            backgroundAudio.MediaEnded += Background_MediaEnded;
        }

        private string getCardCover(Game.CardCover cover)
        {
            switch (cover)
            {
                case Game.CardCover.Blue:
                    return "pack://application:,,,/images/blue_back.jpg";
                case Game.CardCover.Red:
                    return "pack://application:,,,/images/Red_back.jpg";
                case Game.CardCover.Green:
                    return "pack://application:,,,/images/Green_back.jpg";
                case Game.CardCover.Gray:
                    return "pack://application:,,,/images/Gray_back.jpg";
                case Game.CardCover.Yellow:
                    return "pack://application:,,,/images/Yellow_back.jpg";
                default:
                    return "pack://application:,,,/images/purple_back.jpg";
            }
        }

        private void Background_MediaEnded(object sender, EventArgs e)
        {
            if (hasVolume)
            {
                backgroundAudio.Position = TimeSpan.Zero;
                backgroundAudio.Play();
            }
        }

        private void VolumeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            hasVolume = true;
            if (backgroundAudio != null)
            {
                backgroundAudio.Open(new Uri("sounds/CasinoJazz.wav", UriKind.Relative));
                backgroundAudio.Play();
            }

        }

        private void VolumeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            hasVolume = false;
            backgroundAudio.Stop();
        }
    }
}
