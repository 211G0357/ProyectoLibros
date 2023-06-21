using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLibros.Models
{
    public class Libro
    {
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string TituloOriginal { get; set; } = "";
        public string Genero { get; set; } = "";
        public string? Reseña { get; set; }
    }
}
