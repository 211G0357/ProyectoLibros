using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProyectoLibros.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProyectoLibros.ViewModels
{
    public class LibroViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        private Libro book;
        public Libro Libro
        {
            get { return book; }
            set {
                book = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        public ObservableCollection<Libro> libros { get; set; } = new ObservableCollection<Libro>();
        public string? Error { get; set; }
        public string Vista { get; set; } = "ver";
        public int Indice { get; set; }

        public ICommand AgregarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand CancelarCommand { get; set; }

        public void Agregar()
        {
            Error = "";
            if (string.IsNullOrEmpty(Libro.TituloOriginal))
            {
                Error += "Ingrese el titulo autentico";
                MessageBox.Show("Ingrese el titulo autentico");
                return;
            }
            if (string.IsNullOrEmpty(Libro.Titulo))
            {
                Error += "Ingrese el titulo";
                MessageBox.Show("Ingrese el titulo");
                return;
            }
            if (string.IsNullOrEmpty(Libro.Genero))
            {
                Error += "Ingrese el genero correspondiente";
                MessageBox.Show("Ingrese el genero");
                return;
            }
            if (string.IsNullOrEmpty(Libro.Reseña))
            {
                Error += "Ingrese la reseña";
                MessageBox.Show("Ingrese la reseña");
                return;
            }
            if (string.IsNullOrEmpty(Libro.Autor))
            {
                Error += "Ingrese el nombre del director";
                MessageBox.Show("Ingrese el nombre del director");
                return;
            }
            if (libros != null)
            {
                libros.Add(Libro);
                Guardar();
                CambiarVista("ver");
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public void Eliminar()
        {
            if (Libro != null)
            {
                libros.Remove(Libro);
                Guardar();
                CambiarVista("ver");
            }
        }

        public void Editar()
        {
            if (Libro != null && libros != null)
            {
                libros[Indice] = Libro;
                Guardar();
                CambiarVista("ver");
            }
        }

        public void CambiarVista(string vistaACambiar)
        {
            Vista = vistaACambiar;
            if (vistaACambiar == "agregar")
            {
                Libro = new Libro();

            }
            if (vistaACambiar == "editar")
            {
                if (Libro != null)
                {
                    Indice = libros.IndexOf(Libro);

                    Libro copiaLibro = new Libro()
                    {
                        TituloOriginal = Libro.TituloOriginal,
                        Titulo = Libro.Titulo,
                        Autor = Libro.Autor,
                        Genero = Libro.Genero,
                        Reseña = Libro.Reseña,
                    };
                    Libro = copiaLibro;
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Vista"));
        }

        public void Cancelar()
        {
            Vista = "ver";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vista)));
        }

        public void Guardar()
        {
            var json = JsonConvert.SerializeObject(libros);
            File.WriteAllText("ProLibros.json", json);
        }

        public void Cargar()
        {
            if (File.Exists("ProLibros.Json"))
            {
                var json = File.ReadAllText("ProLibros.json");
                var datos = JsonConvert.DeserializeObject<ObservableCollection<Libro>>(json);
                if (datos != null)
                {
                    libros = (ObservableCollection<Libro>)datos;

                }
                else
                {
                    libros = new ObservableCollection<Libro>();
                }
            }
        }

        public LibroViewModels()
        {
            Cargar();
            AgregarCommand = new RelayCommand(Agregar);
            EditarCommand = new RelayCommand(Editar);
            EliminarCommand = new RelayCommand(Eliminar);
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
            CancelarCommand = new RelayCommand(Cancelar);
        }












    }
}
