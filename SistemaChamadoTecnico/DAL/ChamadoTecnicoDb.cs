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

        public static void CreateAtendente(Atendente atendente)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Atendente.Add(atendente);
                ent.SaveChanges();
            }
        }

        public static List<Atendente> ListAtendente()
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                return ent.Atendente.ToList<Atendente>();
            }
        }
    }
}