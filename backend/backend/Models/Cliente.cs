using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TelefonoMovil { get; set; }
        public string Direccion { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string TelefonoSec { get; set; }
        public string Documento { get; set; }
        public string Observacion { get; set; }
        public string Imagen { get; set; }
        public bool Credito { get; set; }
        public decimal? MontoCredito { get; set; }


    }
}
