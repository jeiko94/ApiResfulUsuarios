﻿using System.Diagnostics.Contracts;

namespace ApiResfulUsuarios.Models.DTO
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
}
