using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaChamadoTecnico.Models;

namespace SistemaChamadoTecnico.DAL
{
    public class ChamadoTecnicoDb
    {
        public static void CreateCliente(Cliente cliente)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Cliente.Add(cliente);
                ent.SaveChanges();
            }
        }

        public static List<Cliente> ListCliente()
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                return ent.Cliente.ToList<Cliente>();
            }
        }
    }
}