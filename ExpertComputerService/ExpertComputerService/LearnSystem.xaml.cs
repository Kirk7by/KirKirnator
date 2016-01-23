using DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Domain;
using System.Data;
using Configurate;
using ExpertComputerService.LearnSystemsMaterials;
using System.IO;
using Microsoft.Win32;
using ExpertCore;
using System.Windows.Media.Animation;

namespace ExpertComputerService
{
    /// <summary>
    /// Логика взаимодействия для LearnSystem.xaml
    /// </summary>
    public partial class LearnSystem : Window
    {
        public LearnSystem()
        {
            InitializeComponent();
            ConfigWindow();

            DataGridUpdate();
        }

        private void ConfigWindow()
        {
            switch (ExpConfig.Default.FullscreanWinow)
            {
                case 1:
                    this.WindowState = WindowState.Normal;
                    break;
                case 2:
                    this.WindowState = WindowState.Maximized;
                    break;
                case 3:
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    break;
            }
        }
        enum selectedButtons
        {
            HeroesBut,
            QuestionsBut,
            DominatingBut,
            HeroesThree,
            QuestionTree,
            HeroesLearnClear
        }
        selectedButtons SelectBut;

        //
        List<PHero> Hl = new List<PHero>();
   
        private void DataGridUpdate()
        {
            showEltmtnts();

            switch (SelectBut)
            {
                case selectedButtons.HeroesBut:
                    List<PHero> Hp = new List<PHero>();
                    foreach (var item in new Repository().GetHeroesSource().ToList())
                    {
                        Hp.Add(new PHero { NameHeroes = item.NameHeroes, WeigthHero = item.WeigthHero });
                    }

                    dgrid.ItemsSource = Hp.ToList();
                    break;

                case selectedButtons.QuestionsBut:
                    //    var a = (new Repository().GetQuestionsSource()).Select(d => new { d.NameQestion }).Distinct().ToList();
                    var a = new Repository().GetQuestionsSource();
                    List<PQuestions> Pq = new List<PQuestions>();
                    foreach(var ia in a.Select(d => new { d.NameQestion }).Distinct())
                    {
                        int summary = 0;
                        foreach(var ia2 in a)
                        {
                            if(ia2.NameQestion==ia.NameQestion)
                            {
                                summary = (int)ia2.OtvetSelected + summary;
                            }
                        }
                        Pq.Add(new PQuestions { NameQestion = ia.NameQestion, TotalWeight = summary });
                    }
                    
                    dgrid.ItemsSource = Pq.ToList();
                    break;

                case selectedButtons.DominatingBut:
                    dgrid.ItemsSource = (new Repository().GetQuestionsSource()).Select(d => new
                    {
                        d.NameQestion,
                        d.NameHeroes,
                        d.OtvetQuest1,
                        d.OtvetQuest2,
                        d.OtvetQuest3,
                        d.OtvetQuest4,
                        d.OtvetQuest5,
                        d.OtvetSelected
                    }).ToList();
                    break;

                case selectedButtons.QuestionTree:
                    break;

                case selectedButtons.HeroesLearnClear:
                  //  List<PHero> Hl = new List<PHero>();
                    foreach (var item in new Repository().GetHeroesSource().ToList())
                    {
                        Hl.Add(new PHero { NameHeroes = item.NameHeroes, WeigthHero = item.WeigthHero });
                    }

                    GridHeroLearnDataGrid1.ItemsSource = Hl.ToList();
                    break;
            }
        }
        private void showEltmtnts()
        {
            DoubleAnimation db = new DoubleAnimation();
            db.From = 0;
            db.To = 1.0;
            db.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            db.AutoReverse = false;
            db.RepeatBehavior = RepeatBehavior.Forever;

            db.RepeatBehavior = new RepeatBehavior(1);
            dgrid.BeginAnimation(OpacityProperty, db);
            switch (SelectBut)
            {
                case selectedButtons.HeroesBut:
                    GridHeroes.Visibility = Visibility.Visible;
                    GridQuestion.Visibility = Visibility.Collapsed;
                    GridGeneral.Visibility = Visibility.Visible;
                    GridExtendet.Visibility = Visibility.Collapsed;
                    GridHeroLearnClear.Visibility = Visibility.Collapsed;
                    GridHeroesThree.Visibility = Visibility.Collapsed;
                    break;

                case selectedButtons.QuestionsBut:
                    GridHeroes.Visibility = Visibility.Collapsed;
                    GridQuestion.Visibility = Visibility.Visible;
                    GridGeneral.Visibility = Visibility.Visible;
                    GridExtendet.Visibility = Visibility.Collapsed;
                    GridHeroLearnClear.Visibility = Visibility.Collapsed;
                    GridHeroesThree.Visibility = Visibility.Collapsed;
                    break;

                case selectedButtons.DominatingBut:
                    GridHeroes.Visibility = Visibility.Collapsed;
                    GridQuestion.Visibility = Visibility.Visible;
                    GridGeneral.Visibility = Visibility.Visible;
                    GridExtendet.Visibility = Visibility.Collapsed;
                    GridHeroLearnClear.Visibility = Visibility.Collapsed;
                    GridHeroesThree.Visibility = Visibility.Collapsed;
                    break;

                case selectedButtons.HeroesThree:
                    GridGeneral.Visibility = Visibility.Collapsed;
                    GridExtendet.Visibility = Visibility.Collapsed;
                    GridHeroLearnClear.Visibility = Visibility.Collapsed;
                    GridHeroesThree.Visibility = Visibility.Visible;
                    break;

                case selectedButtons.QuestionTree:
                    GridGeneral.Visibility = Visibility.Collapsed;
                    GridExtendet.Visibility = Visibility.Visible;
                    GridHeroLearnClear.Visibility = Visibility.Collapsed;
                    GridHeroesThree.Visibility = Visibility.Collapsed;
                    break;

                case selectedButtons.HeroesLearnClear:
                    GridGeneral.Visibility = Visibility.Collapsed;
                    GridExtendet.Visibility = Visibility.Collapsed;
                    GridHeroLearnClear.Visibility = Visibility.Visible;
                    GridHeroesThree.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        #region EventsInForm
        #region buttonSelected
        private void buttonHeroes_Click(object sender, RoutedEventArgs e)
        {
            SelectBut = selectedButtons.HeroesBut;
            DataGridUpdate();
            
        }
        private void buttonQuestions_Click(object sender, RoutedEventArgs e)
        {
            SelectBut = selectedButtons.QuestionsBut;
            DataGridUpdate();
        }
        private void buttonDominations_Click(object sender, RoutedEventArgs e)
        {
            SelectBut = selectedButtons.DominatingBut;
            DataGridUpdate();
        }

        private void buttonHeroes_clear_lern_Click(object sender, RoutedEventArgs e)
        {
            SelectBut = selectedButtons.HeroesLearnClear;
            DataGridUpdate();
        }
        private void buttonTreeHeroes_Click(object sender, RoutedEventArgs e)
        {
            SelectBut = selectedButtons.HeroesThree;
            DataGridUpdate();
        }
        private void buttonTreeQuestion_Click(object sender, RoutedEventArgs e)
        {
            SelectBut = selectedButtons.QuestionTree;
            DataGridUpdate();
        }
        #endregion

        private void deletebut_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (dgrid.SelectedItem != null)
                {
                    PHero a = dgrid.SelectedItem as PHero;
                    var NameQuest = dgrid.SelectedCells[0].Item.ToString();
               
                
               // MessageBox.Show(NameQuest.Substring("{"));
                }
                Exception ERep=null;

                switch(SelectBut)
                {
                    case selectedButtons.QuestionsBut:
                        
          //              ERep = new Repository().RemoveQuestion(textBoxAddQuestion.Text);
                        break;
                                   
               }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

    
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (ProgressSuccessLabel.IsVisible == true)
                ProgressSuccessLabel.Visibility = Visibility.Collapsed;
        }
        private void BackMenu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void button_upd_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void buttonSaveQuestion_Click(object sender, RoutedEventArgs e)
        {

        }




        private void dgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SelectBut)
            {
                case selectedButtons.HeroesBut:
                    PHero hero = dgrid.SelectedItem as PHero;
                    if (hero != null)
                    {
                        try
                        {
                            LabelUpd.Content = hero.NameHeroes;
                            string OldHero2 = hero.NameHeroes.Substring(hero.NameHeroes.IndexOf("(") + 1);
                            OldHero2 = OldHero2.Substring(0, OldHero2.Length - 1);
                            string OldHero1 = hero.NameHeroes.Substring(0, hero.NameHeroes.Length - OldHero2.Length - 2);


                            OldName1.Text = OldHero1;
                            OldName2.Text = OldHero2;
                            NewName1.Text = OldHero1;
                            NewName2.Text = OldHero2;
                        }
                        catch
                        {
                            //           MessageBox.Show("Названия героя заданы некорректо, исправьте на корректное название");
                            OldName1.Text = "Некорректно";
                            OldName2.Text = "Некорректно";
                            NewName1.Text = "";
                            NewName2.Text = "";
                        }
                        string imgpatch = ExpConfig.Default.patchImages + hero.NameHeroes;

                        if (File.Exists(imgpatch + ".jpg"))
                            initializeImage(imgpatch, ".jpg");
                        else if (File.Exists(imgpatch + ".png"))
                            initializeImage(imgpatch, ".png");
                        else if (File.Exists(imgpatch + ".gif"))
                            initializeImage(imgpatch, ".gif");
                        else
                            image.Source = new BitmapImage(new Uri("pack://application:,,,/Media/Unknown_Flag.png"));
                    }
                    break;

                case selectedButtons.QuestionsBut:
                    PQuestions quest = dgrid.SelectedItem as PQuestions;
                    if (quest != null)
                    {
                        LabelUpd.Content = quest.NameQestion;
                        TBOldQuestion.Text = quest.NameQestion;
                        TBNewQuestion.Text = quest.NameQestion;
                    }


                    break;
            }
        }
        #endregion


        #region Operations with Heroes
        string patchImage = null; //путь к картинке


        #region Operations with image
        private void initializeImage(string imgpatch, string ImgFormat)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                FileStream stream = new FileStream(imgpatch + ImgFormat, FileMode.Open, FileAccess.Read);
                ms.SetLength(stream.Length);
                stream.Read(ms.GetBuffer(), 0, (int)stream.Length);

                ms.Flush();
                stream.Close();

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.StreamSource = ms;
                src.EndInit();
                image.Source = src;
                patchImage = imgpatch + ImgFormat;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void loadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Файлы изображений|*.png;*.jpg;*.gif|Все файлы(*.*)|*.*";
            openFileDialog1.Title = "Выберите картинку героя";
            try
            {
                if (openFileDialog1.ShowDialog() == true)
                {
                    patchImage = openFileDialog1.FileName;
                    image.Source = new BitmapImage(new Uri(openFileDialog1.FileName));
                }
            }
            catch
            { patchImage = null; }
        }   //загрузка картинки в имедж
        private bool ImageSave(string HeroName)
        {
            bool errors = false;
            if (patchImage != null)
            {
                try
                {
                    if (!Directory.Exists(ExpConfig.Default.patchImages))
                    {
                        Directory.CreateDirectory(ExpConfig.Default.patchImages);
                    }
                    System.IO.File.Copy(patchImage, ExpConfig.Default.patchImages + HeroName + patchImage.Substring(patchImage.Length - 4, 4), true);
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show("Не удалось сохранить картинку. Продолжить отправку?: \n" + ex.Message, "Ошибка сохранения фото", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                        errors = true;
                }
            }
            return errors;
        }   //сохранение картинки в папке с соответствующим имененм
        #endregion

        private void buttonImageBackground_Click(object sender, RoutedEventArgs e)
        {
            buttonImageBackground.Visibility = Visibility.Collapsed;
        }

        private void buttonImageChange_Click(object sender, RoutedEventArgs e)
        {
            buttonImageBackground.Visibility = Visibility.Visible;
            var brush = new ImageBrush();
            brush.ImageSource = image.Source;
            buttonImageBackground.Background = brush;
        }

        private void buttonSwithImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Файлы изображений|*.png;*.jpg;*.gif|Все файлы(*.*)|*.*";
            openFileDialog1.Title = "Выберите картинку героя";
            try
            {
                if (openFileDialog1.ShowDialog() == true)
                {
                    patchImage = openFileDialog1.FileName;
                    image.Source = new BitmapImage(new Uri(openFileDialog1.FileName));
                }
            }
            catch
            { patchImage = null; }
        }

        private void buttonSaveHeroes_Click(object sender, RoutedEventArgs e)
        {
            if (NewName1.Text != "" && NewName2.Text != "")
            {
                if (NewName1.Text == OldName1.Text && NewName2.Text == OldName2.Text)
                {
                    ImageSave(Convert.ToString(LabelUpd.Content));
                    return;
                }
                //

                string NewHeroName = NewName1.Text + "(" + NewName2.Text + ")";
                string OldHeroName = Convert.ToString(LabelUpd.Content);

                if (ImageSave(NewHeroName))
                    return;

                Exception ex = new Repository().updHero(OldHeroName, NewHeroName);
                if (ex != null)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                new Repository().updHero(OldHeroName, NewHeroName);
                DataGridUpdate();
                OldName1.Text = NewName1.Text;
                OldName2.Text = NewName2.Text;
                LabelUpd.Content = NewHeroName;
                ProgressSuccessLabel.Visibility = Visibility.Visible;
              //  MessageBox.Show("Данные сохранены!");
            }
        }



        #endregion

        #region Operations with Questions
        //Okay
        //go     



        #endregion

        //others operations
        #region Others operations

        #region GridHeroLearn

        private void GridHeroLearnTextbox1_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (GridHeroLearnTextbox1.Text!="")
            {
                GridHeroLearnDataGrid1.ItemsSource = (from d in Hl
                                                      where d.NameHeroes.ToLower().Contains(GridHeroLearnTextbox1.Text.ToLower())
                                                      select d).ToList();
                
            }
            else
            {
                GridHeroLearnDataGrid1.ItemsSource = Hl.ToList();
            }
        }

        private void GridHeroLearnTextbox2_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void GridHeroLearnButton1_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #endregion
    }
}
