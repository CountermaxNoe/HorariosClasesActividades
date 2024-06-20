using HorariosClasesActividades.Models;
using HorariosClasesActividades.Repositorios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorariosClasesActividades.ViewModels
{
    public class AgendarClaseActividadViewModel : INotifyPropertyChanged
    {
        public ActiviadadClaseRepositorio repositorio = new();

        public ObservableCollection<RegsitroModel> Registros { get; set; } = new();
        List<RegsitroModel> Lista = new();

        public RegsitroModel? Registro { get; set; }
        public IEnumerable<Dias> DiasSemana => Enum.GetValues(typeof(Dias)).Cast<Dias>();
        Dias diaseleccionado;
        public Dias Dia { get => diaseleccionado; set { diaseleccionado = value; Actualizar(); } }

        public ICommand VerAgregarClaseCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand VerAgregarActividadCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }
        public ICommand EditarCommand {  get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public string Error { get; set; } = "";


        public AgendarClaseActividadViewModel()
        {
            VerAgregarClaseCommand = new Command(VerAgregarClase);
            AgregarCommand = new Command(Agregar);
            VerAgregarActividadCommand = new Command(VerAgregarActividad);
            VerEditarCommand = new Command(VerEditar);
            EditarCommand = new Command(Editar);
            EliminarCommand = new Command(Eliminar);
            CancelarCommand = new Command(Cancelar);

            LlenarLista();
            Actualizar();
        }

        private void Editar()
        {
            if (Registro != null)
            {
                Error = "";

                if (Registro.Categoria == Categorias.Actividad)
                {
                    if (string.IsNullOrWhiteSpace(Registro.Nombre))
                    {
                        Error = "Escriba el nombre de la actividad\n";
                    }
                    if (string.IsNullOrWhiteSpace(Registro.Descripcion) || Registro.Descripcion.Count() > 80)
                    {
                        Error += "Escriba una breve descripcion de la actividad\n";
                    }
                    if (Registro.Hora < 0 || Registro.Hora > 24)
                    {
                        Error += "Ingrese una hora valida dentro del formato de 24 horas";
                    }

                    Actualizar(nameof(Error));
                    if (Error == "")
                    {
                        repositorio.Update(Registro);
                        LlenarLista();
                        Actualizar(nameof(Lista));
                        Actualizar();
                        Actualizar(nameof(Registros));
                        Shell.Current.GoToAsync("//Lista");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Registro.Nombre))
                    {
                        Error = "Escriba el nombre de la Clase\n";
                    }
                    if (string.IsNullOrWhiteSpace(Registro.Docente))
                    {
                        Error += "Escriba el nombre del docente\n";
                    }
                    if (Registro.Hora < 0 || Registro.Hora > 24)
                    {
                        Error += "Ingrese una hora valida dentro del formato de 24 horas";
                    }
                    Actualizar(nameof(Error));

                    if (Error == "")
                    {
                        repositorio.Update(Registro);
                        LlenarLista();
                        Actualizar(nameof(Lista));
                        Actualizar();
                        Actualizar(nameof(Registros));
                        Shell.Current.GoToAsync("//Lista");
                    }
                }
            }
        }

        private void Agregar()
        {
            if (Registro != null)
            {
                Error = "";

                if (Registro.Categoria == Categorias.Actividad)
                {
                    if (string.IsNullOrWhiteSpace(Registro.Nombre))
                    {
                        Error = "Escriba el nombre de la actividad\n";
                    }
                    if (string.IsNullOrWhiteSpace(Registro.Descripcion)||Registro.Descripcion.Count()>80)
                    {
                        Error += "Escriba una breve descripcion de la actividad\n";
                    }
                    if (Registro.Hora < 0 || Registro.Hora > 24)
                    {
                        Error += "Ingrese una hora valida dentro del formato de 24 horas";
                    }

                    Actualizar(nameof(Error));
                    if (Error == "")
                    {
                        repositorio.Insert(Registro);
                        LlenarLista();
                        Actualizar(nameof(Lista));
                        Actualizar();
                        Actualizar(nameof(Registros));
                        Shell.Current.GoToAsync("//Lista");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Registro.Nombre))
                    {
                        Error = "Escriba el nombre de la Clase\n";
                    }
                    if (string.IsNullOrWhiteSpace(Registro.Docente))
                    {
                        Error += "Escriba el nombre del docente\n";
                    }
                    if (Registro.Hora < 0 || Registro.Hora > 24)
                    {
                        Error += "Ingrese una hora valida dentro del formato de 24 horas";
                    }
                    Actualizar(nameof(Error));

                    if (Error == "")
                    {
                        repositorio.Insert(Registro);
                        LlenarLista();
                        Actualizar(nameof(Lista));
                        Actualizar();
                        Actualizar(nameof(Registros));
                        Shell.Current.GoToAsync("//Lista");
                    }
                }
            }
        }

        private void VerEditar()
        {
            if (Registro != null)
            {
                var clon = new RegsitroModel()
                {
                    Nombre = Registro.Nombre,
                    Hora = Registro.Hora,
                    Docente = Registro.Docente,
                    Dia = Registro.Dia,
                    Descripcion = Registro.Descripcion,
                    Categoria = Registro.Categoria,
                    Id = Registro.Id
                };
                Registro = clon;
                Actualizar(nameof(Registro));

                if (string.IsNullOrWhiteSpace(Registro.Docente)) 
                {
                    Shell.Current.GoToAsync("//EditarActividad");
                }
                else
                {
                    Shell.Current.GoToAsync("//EditarClase");
                }
            }
        }

        private void VerAgregarActividad()
        {
            Registro = new()
            {
                Categoria = Categorias.Actividad
            };
            Actualizar(nameof(Registro));
            Shell.Current.GoToAsync("//AgregarActividad");
        }

        private void VerAgregarClase()
        {
            Registro = new()
            {
                Categoria = Categorias.Clase
            };
            Actualizar(nameof(Registro));
            Shell.Current.GoToAsync("//AgregarClase");
        }

        private void Eliminar()
        {
            if (Registro != null)
            {
                repositorio.Delete(Registro);
                LlenarLista();
                Actualizar(nameof(Lista));
                Actualizar();
                Actualizar(nameof(Registros));
            }
        }

        private void Cancelar()
        {
            Registro=null;
            Actualizar(nameof(Registro));
            Shell.Current.GoToAsync("//Lista");
        }

        private void Actualizar()
        {
            Registros.Clear();
            var info = Lista.Where(x => x.Dia == diaseleccionado).OrderBy(x => x.Hora);
            foreach (var item in info)
            {
                Registros.Add(item);
            }
        }
        private void LlenarLista()
        {
            Lista.Clear();
            foreach (var item in repositorio.GetAll().ToList())
            {
                Lista.Add(item);
            }
        }

        void Actualizar(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
