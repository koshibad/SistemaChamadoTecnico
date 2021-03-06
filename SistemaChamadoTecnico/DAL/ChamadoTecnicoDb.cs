﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaChamadoTecnico.Models;
using System.Data.Entity;

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

        public static void CreateChamado(Chamado chamado)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Chamado.Add(chamado);
                ent.SaveChanges();
            }
        }

        public static List<Chamado> ListChamado(string estadoChamado = "")
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                return ent.Chamado.Include(x => x.Atendente).Include(x => x.Cliente)
                    .Where(x => string.IsNullOrEmpty(estadoChamado) ||
                    x.EstadoChamado.Equals(estadoChamado)).ToList<Chamado>();
            }
        }

        public static void CreatePersonUser(string idUser, int idPerson)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                var personUser = new PersonUser();
                personUser.IdPerson = idPerson;
                personUser.IdUser = idUser;
                ent.PersonUser.Add(personUser);
                ent.SaveChanges();
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

        public static Atendente SearchAtendente(int id)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                return ent.Atendente.FirstOrDefault(x => x.IdAtendente == id);
            }
        }

        public static void AlterAtendente(Atendente atendente)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Entry<Atendente>(atendente).State = System.Data.Entity.EntityState.Modified;
                ent.SaveChanges();
            }
        }

        public static void DeleteChamado(Chamado chamado)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Entry<Chamado>(chamado).State = System.Data.Entity.EntityState.Deleted;
                ent.SaveChanges();
            }
        }

        public static void AlterChamado(Chamado chamado)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Entry<Chamado>(chamado).State = System.Data.Entity.EntityState.Modified;
                ent.SaveChanges();
            }
        }

        public static Chamado SearchChamado(int id)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                return ent.Chamado.Include(x => x.Cliente).Include(x => x.Atendente)
                    .FirstOrDefault(x => x.IdChamado == id);
            }
        }

        public static Cliente SearchCliente(int id)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                return ent.Cliente.FirstOrDefault(x => x.IdCliente == id);
            }
        }

        public static void DeleteAtendente(Atendente atendente)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Entry<Atendente>(atendente).State = System.Data.Entity.EntityState.Deleted;
                ent.SaveChanges();
            }
        }

        public static void AlterCliente(Cliente cliente)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Entry<Cliente>(cliente).State = System.Data.Entity.EntityState.Modified;
                ent.SaveChanges();
            }
        }

        public static void DeletePersonUser(int idCliente, ref string idUser)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                var personUser = SearchPersonUser(idCliente);
                idUser = personUser.IdUser;
                ent.Entry<PersonUser>(personUser).State = System.Data.Entity.EntityState.Deleted;
                ent.SaveChanges();
            }
        }

        public static PersonUser SearchPersonUser(int idCliente, string idUser = "")
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                if (string.IsNullOrEmpty(idUser))
                    return ent.PersonUser.FirstOrDefault(x => x.IdPerson == idCliente);

                return ent.PersonUser.FirstOrDefault(x => x.IdUser == idUser);
            }
        }

        public static void DeleteCliente(Cliente cliente)
        {
            using (var ent = new ChamadoTecnicoEntities())
            {
                ent.Entry<Cliente>(cliente).State = System.Data.Entity.EntityState.Deleted;
                ent.SaveChanges();
            }
        }
    }
}