using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Executors
{
    public class Result<T>
    {
        public bool Exitoso { get; set; }
        public string? Mensaje { get; set; }
        public string? CodigoError { get; set; }
        public T? Data { get; set; }

        public static Result<T> Ok(T data, string? mensaje = null)
        {
            return new Result<T> { Exitoso = true, Data = data, Mensaje = mensaje };
        }

        public static Result<T> Error(string mensaje, string? codigoError = null)
        {
            return new Result<T> { Exitoso = false, Mensaje = mensaje, CodigoError = codigoError };
        }
    }
}