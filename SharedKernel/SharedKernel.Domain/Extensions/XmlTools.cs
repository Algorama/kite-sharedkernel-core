using System;
using System.Linq;
using System.Xml.Linq;

namespace SharedKernel.Domain.Extensions
{
    public static class XmlTools
    {
        public static DateTime ToDateTime(this XElement xElement)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xElement?.Value))
                    return DateTime.Now;

                var dataHoraString = xElement.Value.Split("T".ToCharArray());
                if (dataHoraString.Length != 2)
                    return DateTime.Now;

                var dataString = dataHoraString[0].Split("-".ToCharArray());
                if (dataString.Length != 3)
                    return DateTime.Now;

                var horaString = dataHoraString[1].Split(":".ToCharArray());
                if (horaString.Length != 3)
                    return DateTime.Now;

                return new DateTime(
                    Convert.ToInt32(dataString[0]),     // Ano
                    Convert.ToInt32(dataString[1]),     // Mes
                    Convert.ToInt32(dataString[2]),     // Dia
                    Convert.ToInt32(horaString[0]),     // Hora
                    Convert.ToInt32(horaString[1]),     // Minuto
                    Convert.ToInt32(horaString[2])      // Segundo
                    );
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static string GetStringDeFilho(this XElement xElement, string nomeFilho)
        {
            try
            {
                var filho = xElement.Elements().FirstOrDefault(e => e.Name.LocalName == nomeFilho);
                return filho?.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int GetIntDeFilho(this XElement xElement, string nomeFilho)
        {
            try
            {
                var filho = xElement.Elements().FirstOrDefault(e => e.Name.LocalName == nomeFilho);
                return filho == null ? 0 : Convert.ToInt32(filho.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal GetDecimalDeFilho(this XElement xElement, string nomeFilho)
        {
            try
            {
                var filho = xElement.Elements().FirstOrDefault(e => e.Name.LocalName == nomeFilho);
                return filho == null ? 0 : Convert.ToDecimal(filho.Value.Replace(".", ","));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool GetBoolDeFilho(this XElement xElement, string nomeFilho)
        {
            try
            {
                var filho = xElement.Elements().FirstOrDefault(e => e.Name.LocalName == nomeFilho);
                return filho != null && filho.Value == "1";
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DateTime GetDateTimeDeFilho(this XElement xElement, string nomeFilho)
        {
            try
            {
                var filho = xElement.Elements().FirstOrDefault(e => e.Name.LocalName == nomeFilho);
                return filho?.ToDateTime() ?? DateTime.Now;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static DateTime GetDateTimeDeFilho(this XElement xElement, string nomeDataFilho, string nomeHoraFilho)
        {
            try
            {
                var filhoData = xElement.Elements().FirstOrDefault(e => e.Name.LocalName == nomeDataFilho);

                var dataString = filhoData?.Value.Split("-".ToCharArray());
                if (dataString?.Length != 3) return DateTime.Now;

                var filhoHora = nomeHoraFilho == null ? null : xElement.Elements().FirstOrDefault(e => e.Name.LocalName == nomeHoraFilho);
                var hora = filhoHora?.Value ?? "00:00:00";

                var horaString = hora.Split(":".ToCharArray());
                if (horaString.Length != 3)
                    return DateTime.Now;

                return new DateTime(
                    Convert.ToInt32(dataString[0]),     // Ano
                    Convert.ToInt32(dataString[1]),     // Mes
                    Convert.ToInt32(dataString[2]),     // Dia
                    Convert.ToInt32(horaString[0]),     // Hora
                    Convert.ToInt32(horaString[1]),     // Minuto
                    Convert.ToInt32(horaString[2])      // Segundo
                    );
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static string GetAtributo(this XElement xElement, string nomeAtributo)
        {
            try
            {
                var filho = xElement.Attributes().FirstOrDefault(e => e.Name.LocalName == nomeAtributo);
                return filho?.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int GetAtributoInt(this XElement xElement, string nomeAtributo)
        {
            try
            {
                var filho = xElement.Attributes().FirstOrDefault(e => e.Name.LocalName == nomeAtributo);
                return filho == null ? 0 : Convert.ToInt32(filho.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
