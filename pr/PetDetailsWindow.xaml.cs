using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace PetManagementApp
{
    public partial class PetDetailsWindow : Window
    {
        private ObservableCollection<Pet> pets;

        // Конструктор окна деталей питомца
        public PetDetailsWindow(Pet pet, ObservableCollection<Pet> petList)
        {
            InitializeComponent();
            DataContext = pet;
            pets = petList;

            // Заполняем ComboBox для времени утра, дня и вечера
            FillTimeComboBoxes();

            // Если время кормления уже выбрано, устанавливаем его в ComboBox
            SetFeedingTimeComboBoxes(pet);

            // Если время похода к ветеринару уже выбрано, устанавливаем его в DatePicker и ComboBox
            if (!string.IsNullOrEmpty(pet.VetAppointment))
            {
                DateTime vetAppointmentDateTime = DateTime.Parse(pet.VetAppointment);
                dpVetAppointmentDate.SelectedDate = vetAppointmentDateTime.Date;

                // Установка времени в ComboBox
                cmbVetAppointmentTime.SelectedItem = vetAppointmentDateTime.ToString("hh:mm tt");
            }
        }

        // Метод для заполнения ComboBox времени
        private void FillTimeComboBoxes()
        {
            // Добавляем вариант прочерка, если кормление не требуется
            cmbFeedingMorningTime.Items.Add("-");
            cmbFeedingDayTime.Items.Add("-");
            cmbFeedingEveningTime.Items.Add("-");

            // Добавляем варианты времени для утра, дня и вечера
            for (int i = 1; i <= 12; i++)
            {
                cmbFeedingMorningTime.Items.Add($"{i}:00 AM");
                cmbFeedingDayTime.Items.Add($"{i}:00 PM");
                cmbFeedingEveningTime.Items.Add($"{i}:00 PM");
            }

            // Очищаем cmbVetAppointmentTime и добавляем только утренние часы
            cmbVetAppointmentTime.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cmbVetAppointmentTime.Items.Add($"{i}:00 AM");
            }
        }

        // Метод для установки выбранных значений в ComboBox времени кормления
        private void SetFeedingTimeComboBoxes(Pet pet)
        {
            if (!string.IsNullOrEmpty(pet.FeedingTimeMorning))
            {
                cmbFeedingMorningTime.SelectedItem = pet.FeedingTimeMorning;
            }
            else
            {
                cmbFeedingMorningTime.SelectedItem = "-"; // Прочерк, если кормление не требуется утром
            }

            if (!string.IsNullOrEmpty(pet.FeedingTimeDay))
            {
                cmbFeedingDayTime.SelectedItem = pet.FeedingTimeDay;
            }
            else
            {
                cmbFeedingDayTime.SelectedItem = "-"; // Прочерк, если кормление не требуется днем
            }

            if (!string.IsNullOrEmpty(pet.FeedingTimeEvening))
            {
                cmbFeedingEveningTime.SelectedItem = pet.FeedingTimeEvening;
            }
            else
            {
                cmbFeedingEveningTime.SelectedItem = "-"; // Прочерк, если кормление не требуется вечером
            }

            if (!string.IsNullOrEmpty(pet.VetAppointment))
            {
                cmbVetAppointmentTime.SelectedItem = pet.VetAppointment;
            }
        }

        // Обработчик события для кнопки "Сохранить и вернуться"
        private void SaveAndReturn_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем изменения, например, в базе данных
            SaveChanges();

            // Возвращаемся к предыдущему окну
            BackToPreviousWindow();
        }

        // Обработчик события для кнопки "Назад"
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            // Просто возвращаемся к предыдущему окну
            BackToPreviousWindow();
        }

        // Метод для сохранения изменений
        private void SaveChanges()
        {
            (DataContext as Pet).FeedingSchedule = "Утро";
            (DataContext as Pet).FeedingTimeMorning = cmbFeedingMorningTime.SelectedItem?.ToString();

            (DataContext as Pet).FeedingSchedule = "День";
            (DataContext as Pet).FeedingTimeDay = cmbFeedingDayTime.SelectedItem?.ToString();

            (DataContext as Pet).FeedingSchedule = "Вечер";
            (DataContext as Pet).FeedingTimeEvening = cmbFeedingEveningTime.SelectedItem?.ToString();

            (DataContext as Pet).FeedingSchedule = "Ветеринар";
            (DataContext as Pet).VetAppointment = cmbVetAppointmentTime.SelectedItem?.ToString();
        }

        // Метод для возврата к предыдущему окну
        private void BackToPreviousWindow()
        {
            // Вместо закрытия окна просто скрываем его
            Hide();

            PetListWindow petListWindow = new PetListWindow(pets);
            petListWindow.ShowDialog();

            // Закрываем окно деталей после завершения работы с окном выбора питомца
            Close();
        }

        // Обработчик события для кнопки "Сохранить"
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем изменения, например, в базе данных
            SaveChanges();

            // Возвращаемся к предыдущему окну
            BackToPreviousWindow();
        }

        // Обработчик события для кнопки "Удалить питомца"
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Выбранный питомец
            Pet currentPet = DataContext as Pet;

            if (currentPet != null)
            {
                // Удаляем питомца из коллекции
                pets.Remove(currentPet);
                // Сохраняем изменения (в базе данных, если используется)
                Close();
            }
        }
    }
}