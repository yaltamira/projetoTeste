using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using TesteLib.BE;

namespace TesteLib.DB
{
    public class Contexto: DbContext
    {
        public Contexto() : base("Contexto")
        {
            
        }

        public DbSet<UsuarioBE> UsuarioDB { get; set; }
        public DbSet<ContatoBE> ContatoDB { get; set; }
    }
}
