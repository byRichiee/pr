using System.Collections.ObjectModel;
using System.Windows;

namespace PetManagementApp
{
    // Класс окна списка питомцев
    public partial class PetListWindow : Window
    {
        // Коллекция питомцев
        private ObservableCollection<Pet> pets;

        // Конструктор окна списка питомцев
        public PetListWindow(ObservableCollection<Pet> petList)
        {
            InitializeComponent();
            // Инициализация коллекции питомцев
            pets = petList;

            // Заполнение списка питомцев в окне
            lstPets.ItemsSource = pets;
        }

        // Обработчик события для кнопки "Просмотреть детали"
        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбранного питомца
            if (lstPets.SelectedItem != null)
            {
                // Открытие окна деталей питомца, передача выбранного питомца и коллекции
                PetDetailsWindow detailsWindow = new PetDetailsWindow(lstPets.SelectedItem as Pet, pets);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите питомца для просмотра деталей.");
            }
        }

        // Обработчик события для кнопки "Назад"
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие текущего окна списка питомцев
            Close();
        }

        // Обработчик события для кнопки "Удалить питомца"
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный питомец
            Pet selectedPet = lstPets.SelectedItem as Pet;

            if (selectedPet != null)
            {
                // Удаляем питомца из коллекции
                pets.Remove(selectedPet);
                // Обновляем список в ListBox
                RefreshPetList();
            }
        }

        // Метод для обновления списка питомцев в ListBox
        private void RefreshPetList()
        {
            lstPets.ItemsSource = null;
            lstPets.ItemsSource = pets;
        }
    }
}