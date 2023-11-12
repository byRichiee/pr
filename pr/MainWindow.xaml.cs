using System.Collections.ObjectModel;
using System.Windows;

namespace PetManagementApp
{
    // Класс основного окна приложения
    public partial class MainWindow : Window
    {
        // Коллекция питомцев
        private ObservableCollection<Pet> pets;

        // Конструктор основного окна
        public MainWindow()
        {
            InitializeComponent();
            // Инициализация коллекции питомцев
            pets = new ObservableCollection<Pet>();
        }

        // Обработчик события для кнопки "Добавить питомца"
        private void AddPet_Click(object sender, RoutedEventArgs e)
        {
            // Получение данных о новом питомце из полей ввода
            string name = txtName.Text;
            string breed = cmbBreed.Text;
            int age = int.Parse(txtAge.Text);

            // Создание нового объекта питомца
            Pet newPet = new Pet { Name = name, Breed = breed, Age = age };

            // Добавление питомца в коллекцию
            pets.Add(newPet);

            // Вывод уведомления
            MessageBox.Show("Питомец добавлен!");

            // Очистка полей ввода
            ClearFields();
        }

        // Обработчик события для кнопки "Просмотреть питомцев"
        private void ViewPets_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна списка питомцев, передача коллекции
            PetListWindow petListWindow = new PetListWindow(pets);
            petListWindow.ShowDialog();
        }

        // Метод для очистки полей ввода
        private void ClearFields()
        {
            txtName.Text = "";
            cmbBreed.Text = "";
            txtAge.Text = "";
        }
    }
}