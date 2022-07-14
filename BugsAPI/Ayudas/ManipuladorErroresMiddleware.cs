using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BugsAPI.Ayudas
{
	public class ManipuladorErroresMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ManipuladorErroresMiddleware> _logger;

		public ManipuladorErroresMiddleware(RequestDelegate next, ILogger<ManipuladorErroresMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
        {
			try
            {
				await _next(context);
            }
			catch (Exception error)
            {
				var respuesta = context.Response;
				respuesta.ContentType = "application/json";

				switch (error)
                {
					case AppException e:
						respuesta.StatusCode = (int)HttpStatusCode.BadRequest;
						break;
					case KeyNotFoundException e:
						// Error de Llave no encontrada
						respuesta.StatusCode = (int)HttpStatusCode.NotFound;
						break;
					default:
						// Error de Excepción no manipulada
						respuesta.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}

				_logger.LogError(error, error.Message);

				var respuestaError = new
				{
					estado = respuesta.StatusCode,
					mensaje = error?.Message,
					error = error?.ToString()
				};
				await respuesta.WriteAsync(JsonConvert.SerializeObject(respuestaError));
            }
        }
	}
}

