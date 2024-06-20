using HorariosClasesActividades.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorariosClasesActividades.Repositorios
{
    public class ActiviadadClaseRepositorio
    {
        SQLiteConnection conexion;

        public ActiviadadClaseRepositorio()
        {
            var ruta = FileSystem.AppDataDirectory + "/registros.sqlite";
            conexion = new SQLiteConnection(ruta);
            conexion.CreateTable<RegsitroModel>();
        }

        public IEnumerable<RegsitroModel> GetAll()
        {
            var datos = conexion.Table<RegsitroModel>().OrderBy(x => x. Hora).ToList();
            return datos;
        }

        public void Insert(RegsitroModel actividadclase)
        {
            conexion.Insert(actividadclase);
        }

        public void Update(RegsitroModel actividadclase)
        {
            conexion.Update(actividadclase);
        }

        public void Delete(RegsitroModel actividadclase)
        {
            conexion.Delete(actividadclase);
        }
    }
}

