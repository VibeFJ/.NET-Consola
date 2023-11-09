using System;
using System.Collections.Generic;
using Consola.ControladorNegocio;
using Consola.Entidades;
namespace Consola
{
    public class Program
    {
        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.WriteLine("**********  Bienvenido al CRUD **********");
                Console.WriteLine("**********  Por favor seleccione una opcion **********");

                Console.WriteLine("1._ Insertar");
                Console.WriteLine("2._ Mostrar");
                Console.WriteLine("3._ Actualizar");
                Console.WriteLine("4._ Eliminar");
                Console.WriteLine("Opcion: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Registrar();
                        break;
                    case 2:
                        Mostrar();
                        break;
                    case 3:
                        Actualizar();
                        break;
                    case 4:
                        Eliminar();
                        break;
                }
            }while (opcion != 0);
        }

        static void Registrar()
        {
            Console.Clear();

            var usuario = new TAUsuario();

            Console.WriteLine("Nombre: ");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Apellido Paterno: ");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Apellido Materno: ");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Nombre Usuario: ");
            usuario.NombreUsuario = Console.ReadLine();

            Console.WriteLine("Contraseña: ");
            usuario.Contraseña = Console.ReadLine();

            Console.WriteLine("Dirección: ");
            usuario.Direccion = Console.ReadLine();

            Console.WriteLine("Telefono: ");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Selecccione un Genero");

            var controlador = new ctrTCGenero();
            var generos = controlador.Obtener();

            foreach (var genero in generos)
            {
                Console.WriteLine(genero.GeneroId + "._ " + genero.Descripcion);
            }

            Console.WriteLine("Genero: ");
            usuario.GeneroId = int.Parse(Console.ReadLine());

            var ctrUsuario = new ctrTAUsuario();
            var respuesta = ctrUsuario.Insertar(usuario);

            if (respuesta)
            {
                var ctrUsuarioDetalle = new ctrTAUsuarioDetalle();
                respuesta = ctrUsuarioDetalle.Insertar(usuario);

                if (respuesta)
                {
                    Console.Clear();
                    Console.WriteLine("Registro exitoso");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Registro erroneo");
                    Console.ReadLine();
                }
            }
        }

        static void Mostrar()
        {
            Console.Clear();
            var ctrUsuario = new ctrTAUsuario();
            var usuarios = ctrUsuario.Obtener();

            var ctrUsuarioDetalle = new ctrTAUsuarioDetalle();
            var usuariosDetalle = ctrUsuarioDetalle.Obtener();

            var ctrgenero = new ctrTCGenero();
            var generos = ctrgenero.Obtener();

            foreach (var usuario in usuarios)
            {
                foreach (var usuarioDetalle in usuariosDetalle)
                {
                    if(usuario.UsuarioId == usuarioDetalle.UsuarioId)
                    {
                        usuario.Direccion = usuarioDetalle.Direccion;
                        usuario.Telefono = usuarioDetalle.Telefono; 
                        usuario.GeneroId = usuarioDetalle.GeneroId;
                    }

                }

                foreach (var genero in generos)
                {
                    if (genero.GeneroId == usuario.GeneroId)
                    {
                        usuario.Genero = genero.Descripcion;
                        break;
                    }
                }

                Console.WriteLine("Nombre: " + usuario.Nombre);
                Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                Console.WriteLine("Usuario: " + usuario.NombreUsuario);
                Console.WriteLine("Genero: " + usuario.Genero);
                Console.WriteLine("Direccion: " + usuario.Direccion);
                Console.WriteLine("Telefono: " + usuario.Telefono);
                Console.WriteLine();
            }
            Console.ReadLine();
            Console.Clear();
        }

        static void Actualizar()
        {
            Console.WriteLine("Ingresa tu usuario: ");
            var NombreUsuario = Console.ReadLine().ToString();

            Console.WriteLine("Ingresa la contraseña: ");
            var Contraseña = Console.ReadLine().ToString();

            var ctrUsuario = new ctrTAUsuario();
            var usuarios = ctrUsuario.Obtener();

            bool respuesta = true;
            foreach (var usuario in usuarios )
            {
                if(NombreUsuario == usuario.NombreUsuario && Contraseña == usuario.Contraseña)
                {
                    Console.WriteLine("Ingresa tus datos");
                    Console.ReadLine();
                    Console.WriteLine("Nombre: ");
                    usuario.Nombre = Console.ReadLine();

                    Console.WriteLine("Apellido Paterno: ");
                    usuario.ApellidoPaterno = Console.ReadLine();

                    Console.WriteLine("Apellido Materno: ");
                    usuario.ApellidoMaterno = Console.ReadLine();

                    Console.WriteLine("Nombre Usuario: ");
                    usuario.NombreUsuario = Console.ReadLine();

                    Console.WriteLine("Contraseña: ");
                    usuario.Contraseña = Console.ReadLine();

                    Console.WriteLine("Dirección: ");
                    usuario.Direccion = Console.ReadLine();

                    Console.WriteLine("Telefono: ");
                    usuario.Telefono = Console.ReadLine();

                    Console.WriteLine("Selecccione un Genero");

                    var controlador = new ctrTCGenero();
                    var generos = controlador.Obtener();

                    foreach (var genero in generos)
                    {
                        Console.WriteLine(genero.GeneroId + "._ " + genero.Descripcion);
                    }

                    Console.WriteLine("Genero: ");
                    usuario.GeneroId = int.Parse(Console.ReadLine());

                    ctrUsuario = new ctrTAUsuario();
                    respuesta = ctrUsuario.Actualizar(usuario);

                    if (respuesta)
                    {
                        var ctrUsuarioDetalle = new ctrTAUsuarioDetalle();
                        respuesta = ctrUsuarioDetalle.Actualizar(usuario);

                        if (respuesta)
                        {
                            Console.Clear();
                            Console.WriteLine("Actualizacion exitosa");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Actualización erronea");
                            Console.ReadLine();
                        }
                    }
                    break;
                }
                else
                {
                    respuesta = false;
                }
            }

            if (!respuesta)
            {
                Console.Clear();
                Console.WriteLine("Usuario y/o contraseña incorrecta");
                Console.ReadLine();
            }
        }

        static void Eliminar()
        {
            Console.WriteLine("Ingresa tu usuario: ");
            var NombreUsuario = Console.ReadLine().ToString();

            Console.WriteLine("Ingresa la contraseña: ");
            var Contraseña = Console.ReadLine().ToString();

            var ctrUsuario = new ctrTAUsuario();
            var usuarios = ctrUsuario.Obtener();

            bool respuesta = true;
            foreach (var usuario in usuarios)
            {
                if (NombreUsuario == usuario.NombreUsuario && Contraseña == usuario.Contraseña)
                {
                    Console.WriteLine("¿Seguro que deseas eliminar? Y/N");
                    var confirmar = Console.ReadLine();

                    if(confirmar == "Y")
                    {
                        ctrUsuario = new ctrTAUsuario();
                        respuesta = ctrUsuario.Eliminar(usuario);

                        if (respuesta)
                        {
                            var ctrUsuarioDetalle = new ctrTAUsuarioDetalle();
                            respuesta = ctrUsuarioDetalle.Eliminar(usuario);
                            if (respuesta)
                            {
                                Console.Clear();
                                Console.WriteLine("Se elimino correctamente");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("No se ha podido eliminar");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("No se ha podido eliminar");
                            Console.ReadLine();
                        }
                    }
                    break;
                }
                else
                {
                    respuesta = false;
                }
            }

            if (!respuesta)
            {
                Console.Clear();
                Console.WriteLine("Usuario y/o contraseña incorrecta");
                Console.ReadLine();
            }
        }
    }
}
