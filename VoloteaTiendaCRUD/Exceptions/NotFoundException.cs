using System;

namespace VoloteaTiendaAPI
{
    //Clase personal para manejar mejos las posibles excepciones de la API
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
