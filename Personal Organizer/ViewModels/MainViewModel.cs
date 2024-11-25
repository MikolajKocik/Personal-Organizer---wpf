using PersonalOrganizer.Data;
using PersonalOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalOrganizer.ViewModels
{
    public class MainViewModel
    {
        // Pole do przechowywania polecenia
        public ICommand AddEventCommand { get; set; }

        // Kolekcja wydarzeń (ObservableCollection pozwala na dynamiczne uaktualnianie UI)
        public ObservableCollection<EventModel> Events { get; set; }

        // Model nowego wydarzenia
        public EventModel NewEvent { get; set; } = new EventModel
        {
            Date = DateTime.Today // Ustawiamy dzisiejszą datę jako domyślną
        };

        // Konstruktor, gdzie ustawiamy polecenie i dane
        public MainViewModel()
        {
            // Tworzymy nową komendę, która zawsze jest wykonalna
            AddEventCommand = new RelayCommand(o => AddEvent(), _ => true);

            // Inicjalizacja kolekcji
            Events = new ObservableCollection<EventModel>();
            NewEvent = new EventModel();

            // Ładowanie istniejących wydarzeń z bazy danych
            LoadEvents();
        }

        // Metoda do ładowania wydarzeń z bazy danych
        private void LoadEvents()
        {
            using (var db = new AppDbContext())
            {
                var events = db.Events.ToList();
                foreach (var evt in events)
                {
                    // Dodajemy każde wydarzenie do ObservableCollection, co powoduje uaktualnienie UI
                    Events.Add(evt);
                }
            }
        }

        // Metoda dodająca nowe wydarzenie
        public void AddEvent()
        {
            using (var db = new AppDbContext())
            {
                // Dodajemy nowe wydarzenie do bazy danych
                db.Events.Add(NewEvent);
                db.SaveChanges();

                // Dodajemy nowe wydarzenie do ObservableCollection, co powoduje uaktualnienie UI
                Events.Add(NewEvent);
            }

            // Resetujemy formularz po dodaniu wydarzenia
            NewEvent = new EventModel();
        }
    }
}
