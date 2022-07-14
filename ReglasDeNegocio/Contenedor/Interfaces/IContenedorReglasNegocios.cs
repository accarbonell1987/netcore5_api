using ReglasDeNegocio;
using System;

namespace ReglasNegocio.Contenedor.Interfaces
{
	public interface IContenedorReglasNegocios
	{
		BugRN BugRN { get; }
		ProyectoRN ProyectoRN { get; }
		UsuarioRN UsuarioRN { get; }
	}
}

