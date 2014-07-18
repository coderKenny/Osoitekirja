using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Osoitekirja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Person  //Luodaan Henkilö
    {
        public string Name { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class FooPerson // Käytettiin ei hakutuloksia kohdassa!
    {
        public string Name { get; set; }
    }

    public partial class MainWindow : Window
    {
        // List<Person> Personlist=new List<Person>(); EI käytä listaa ei toimi

        //Person PersonB = new Person(); Kovakoodattuja dummyja
        //Person PersonC = new Person();
        //Person PersonD = new Person();

        // Osoitekirjaa varten listat, henkilölista ja hakutuloslista:
        ObservableCollection<Person> Personlist = new ObservableCollection<Person>();
        ObservableCollection<Person> resultList = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
            LueYhteysTiedot();

            //Kovakoodatut yhteystiedot kehitysvaihetta varten
            //Person PersonB = new Person();
            //PersonB.Name = "Mauri Pekkarinen";
            //PersonB.Address = "Pekkakuja 3, 24260 Salo";
            //PersonB.Email = "Mp@netti.fi";
            //PersonB.Phonenumber = "+358503701234";
            //Personlist.Add(PersonB);

            //////Person PersonC = new Person();
            //PersonC.Name = "Anita Jääskeläinen";
            //PersonC.Address = "Kissakuja 3, 24260 Helsinki";
            //PersonC.Email = "Mp@netti.fi";
            //PersonC.Phonenumber = "+358503701234";
            //Personlist.Add(PersonC);

            //////Person PersonD = new Person();
            //PersonD.Name = "Ville Valo";
            //PersonD.Address = "Valokuja 3, 24260 Espoo";
            //PersonD.Email = "Mp@netti.fi";
            //PersonD.Phonenumber = "+358503701234";
            //Personlist.Add(PersonD);

            // Binding
            this.myListView.ItemsSource = Personlist;
            this.myListView2.ItemsSource = Personlist;
        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { // Valitaan yhteystieto haku tabissa

            try 
            { //Lisää Textblokkiin valitun yhteystiedon
                this.myTextBlockA.Text = ((Person)e.AddedItems[0]).Name + "\n" +
                 ((Person)e.AddedItems[0]).Phonenumber + "\n" +
                 ((Person)e.AddedItems[0]).Address + "\n" +
                 ((Person)e.AddedItems[0]).Email;

                this.myTextBlockA.FontSize = 20;
            }
            catch (System.IndexOutOfRangeException exception)
            { // Jos yhteystieto poistettu estää kaatumisen

                this.myTextBlockA.Text = "Yhteystieto";
            }
            catch (Exception ex)// Jos yhteystieto poistettu estää kaatumisen
            {
                if (ex is IndexOutOfRangeException)
                {
                    this.myTextBlockA.Text = "Yhteystieto";
                }
                else // jos antaa anna nimi kenttään tyhjän ja listview ilmoittaa ei hakutuloksia ja sitä painettaessa 
                    //textblokkiin kirjoitetaan anna nimi
                {
                    this.myListView.ItemsSource = null;
                    this.myListView.Items.Clear();
                    this.myListView.ItemsSource = Personlist;

                    this.Hakuikkuna.Text = "Anna nimi";
                }
            }
        }

        //Lisätyn yhteystiedon tallennus
        private void Tallennusnappi_Click(object sender, RoutedEventArgs e) 
        {
            if (this.Nimi.Text == "Kirjoita nimi" || this.Osoite.Text == "Kirjoita osoite" || this.Email.Text == "Kirjoita sähköposti" || this.Puhelinnumero.Text == "Kirjoita puhelinnumero")
                MessageBox.Show("Tarkista tiedot", "Tietojen tarkistus", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (this.Nimi.Text == "" || this.Osoite.Text == "" || this.Email.Text == "" || this.Puhelinnumero.Text == "")
                MessageBox.Show("Tarkista tiedot", "Tietojen tarkistus", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                string olionnimi = this.Nimi.Text;
                olionnimi = olionnimi.Replace(" ", "");

                Person henkilo = new Person();

                henkilo.Name = this.Nimi.Text;
                henkilo.Address = this.Osoite.Text;
                henkilo.Phonenumber = this.Puhelinnumero.Text;
                henkilo.Email = this.Email.Text;
                Personlist.Add(henkilo);      
            }
        }

        // Poistaa valitun yhteystiedon
        private void Poista_Click(object sender, RoutedEventArgs e) 
        {
            int index;
            this.myListView2.SelectedItem.GetType();
            //   MessageBox.Show(myListView2.SelectedItem.GetType().Name);
            if (myListView2.SelectedItem != null)
            {

                index = Personlist.IndexOf((Person)myListView2.SelectedItem);
                Personlist.RemoveAt(index);
                foreach (ListViewItem eachItem in myListView.SelectedItems)
                {
                    myListView.Items.Remove(eachItem);
                }
                foreach (ListViewItem eachItem in myListView2.SelectedItems)
                {
                    myListView2.Items.Remove(eachItem);
                }
            }
        }

        // Valitaan yhteystieto muokkaamista varten Muokkaa tabissa
        private void myListView2_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            try
            {
                this.Nimiboksi.Text = ((Person)e.AddedItems[0]).Name;
                this.Emailboksi.Text = ((Person)e.AddedItems[0]).Email;
                this.Puhelinnumeroboksi.Text = ((Person)e.AddedItems[0]).Phonenumber;
                this.Osoiteboksi.Text = ((Person)e.AddedItems[0]).Address;
                //((Person)e.AddedItems[0]).Phonenumber + "\n" +
                //((Person)e.AddedItems[0]).Address + "\n" +
                //((Person)e.AddedItems[0]).Email;

                //   this.Nimiboksi.FontSize = 15;
            }
            catch (System.IndexOutOfRangeException exception)
            {
                this.Nimiboksi.Text = "Nimi";
                this.Osoiteboksi.Text = "Osoite";
                this.Puhelinnumeroboksi.Text = "Puhelin";
                this.Emailboksi.Text = "Sähköposti";
            }
        }

        //Muokatun yhteystiedon talleus
        private void Tallenna_Click(object sender, RoutedEventArgs e)
        {
            int index;
            if (!this.Nimiboksi.Text.Equals("Nimi"))
            {
                try
                {
                    string valitunNimi = ((Person)this.myListView2.SelectedItem).Name;
                    index = Personlist.IndexOf((Person)myListView2.SelectedItem);
                    Personlist[index].Name = this.Nimiboksi.Text;

                    ((Person)this.myListView2.SelectedItem).Name = Personlist[index].Name;
                    string valitunOsoite = ((Person)this.myListView2.SelectedItem).Address;
                    index = Personlist.IndexOf((Person)myListView2.SelectedItem);
                    Personlist[index].Address = this.Osoiteboksi.Text;

                    ((Person)this.myListView2.SelectedItem).Address = Personlist[index].Address;

                    string valitunPuhelin = ((Person)this.myListView2.SelectedItem).Phonenumber;
                    index = Personlist.IndexOf((Person)myListView2.SelectedItem);
                    Personlist[index].Phonenumber = this.Puhelinnumeroboksi.Text;

                    ((Person)this.myListView2.SelectedItem).Phonenumber = Personlist[index].Phonenumber;

                    string valitunEmail = ((Person)this.myListView2.SelectedItem).Email;
                    index = Personlist.IndexOf((Person)myListView2.SelectedItem);
                    Personlist[index].Email = this.Emailboksi.Text;

                    ((Person)this.myListView2.SelectedItem).Email = Personlist[index].Email;
                
                    // Must be removed and added again to show the changes in listView

                    Person dummy = Personlist[index];

                    Personlist.RemoveAt(index);

                    Personlist.Insert(index, dummy);
                }
                catch (NullReferenceException exception)
                {
                    // Kaatuu kun mitään ei ole valittuna
                    // Korjattu
                }
            }
        }

        //Haku nappula
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (resultList.Count != 0)
            {
                for (int i = 0; i < resultList.Count; i++)
                    resultList.RemoveAt(0);
            }
            string hakuSana = Hakuikkuna.Text;
            foreach (Person person in Personlist)
            {
                if (person.Name.Contains(hakuSana) && hakuSana != "")
                    resultList.Add(person);
            }
            //  BindingOperations.ClearBinding(Personlist,ListView.ItemsSourceProperty)
            //   this.myListView.ItemsSource = resultList;
            this.myListView.ItemsSource = null;
            this.myListView.Items.Clear();
            if (resultList.Count != 0)
                this.myListView.ItemsSource = resultList;
            else
            {
                FooPerson notFoundPerson = new FooPerson(); // Ei hakutuloksia tilanteessa
                notFoundPerson.Name = "Ei hakutuloksia";
                this.myListView.Items.Add(notFoundPerson);
            }
        }
 
        //MAhdollisesti kokeilu?
        private void myTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nimix = sender.GetType().Name;
        }

        //Tyhjentää valitun textboxin tekstin kirjoittamista varten
        private void Nimi_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox boksi = (TextBox)sender;
            boksi.Text = string.Empty;
            //       boksi.GotFocus -= Nimi_GotFocus;
        }

        private void Osoite_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox boksi2 = (TextBox)sender;
            boksi2.Text = string.Empty;
            //         boksi2.GotFocus -= Osoite_GotFocus;
        }

        private void Puhelinnumero_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox boksi3 = (TextBox)sender;
            boksi3.Text = string.Empty;
            //      boksi3.GotFocus -= Puhelinnumero_GotFocus;
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox boksi4 = (TextBox)sender;
            boksi4.Text = string.Empty;
            //    boksi4.GotFocus -= Email_GotFocus;
        }

        // PAlauttaa tekstiboksin tekstin kun boxiin ei ole kirjoitettu mitään
        private void Nimi_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Nimi.Text == "")
            {
                TextBox boksi = (TextBox)sender;
                boksi.Text = "Kirjoita Nimi";
                //  boksi.GotFocus -= Nimi_LostFocus;
            }
        }

        private void Puhelinnumero_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Puhelinnumero.Text == "")
            {
                TextBox boksi5 = (TextBox)sender;
                boksi5.Text = "Kirjoita Puhelinnumero";
                //        boksi5.GotFocus -= Puhelinnumero_GotFocus;
            }
        }

        private void Osoite_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Osoite.Text == "")
            {
                TextBox boksi6 = (TextBox)sender;
                boksi6.Text = "Kirjoita Osoite";
                //      boksi6.GotFocus -= Osoite_GotFocus;
            }
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "")
            {
                TextBox boksi7 = (TextBox)sender;
                boksi7.Text = "Kirjota sähköposti";
                //    boksi7.GotFocus -= Osoite_GotFocus;
            }
        }

        //Tyhjää kentät tallennetun yhteystiedon jälkeen
        private void Nappi_Click(object sender, RoutedEventArgs e)
        {
            this.Nimi.Text = "Kirjoita Nimi";
            this.Osoite.Text = "Kirjoita Osoite";
            this.Puhelinnumero.Text = "Kirjoita Puhelinnumero";
            this.Email.Text = "Kirjoita Sähköposti";
        }

        // Tallentaa yhteytiedot tiedostoon
        private void Kirjoita_Click(object sender, RoutedEventArgs e)
        {
            System.IO.StreamWriter file2 = new System.IO.StreamWriter(@"c:\file3.txt", false);

            foreach (Person person in Personlist)
            {
                file2.Write(person.Name + "|");
                file2.Write(person.Phonenumber + "|");
                file2.Write(person.Address + "|");
                file2.WriteLine(person.Email + "|");
            }
            file2.Close();
        }

        //Luetaan ajon aluksi tiedostossa olevat yhteystiedot
        private void LueYhteysTiedot()
        {
            string path = @"c:\file3.txt";
            System.IO.StreamReader file2;
            System.IO.StreamWriter file3;
            if (!System.IO.File.Exists(path)) // Jos tiedostoa ei ole luodaan tiedosto
            {
                file3 = System.IO.File.CreateText(path); //Palauttaa streamwriterin, kiertotie
                file3.Close();
                file2 = new System.IO.StreamReader(@"c:\file3.txt");
            }
            else // avaa olemassaolevan tiedoston
            {
                file2 = new System.IO.StreamReader(@"c:\file3.txt");
            }
            this.myListView2.ItemsSource = null;
            this.myListView2.Items.Clear();

            for (int i = 0; i < Personlist.Count; i++)
            {
                Personlist.RemoveAt(i);
            }
            int j = 0;
            while (file2.EndOfStream == false)
            {
                j = 0;
                Person person = new Person();
                string line = file2.ReadLine();
                string[] lines = line.Split('|');
                person.Name = lines[j];
                person.Phonenumber = lines[j + 1];
                person.Address = lines[j + 2];
                person.Email = lines[j + 3];
                Personlist.Add(person);
            }
            file2.Close();
        }

        //Tyhjää hakuboxin tekstin
        private void Hakuikkuna_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox boksi7 = (TextBox)sender;
            boksi7.Text = string.Empty;
        }
    }
}