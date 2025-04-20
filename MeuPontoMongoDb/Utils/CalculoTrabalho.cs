using MeuPontoMongoDb.Models;
using TimeZoneConverter;

namespace MeuPontoMongoDb.Utils
{
    public class CalculoTrabalho
    {


      
        public static DateTime ObterHorarioBrasilia()
        {
            TimeZoneInfo zonaBrasilia = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Utc, zonaBrasilia);
        }

        public static DateTime ConverterParaHorarioBrasilia(DateTime dataHora)
        {
            TimeZoneInfo zonaBrasilia = TZConvert.GetTimeZoneInfo("E. South America Standard Time");

            // Verifique se a data/hora já está no horário de Brasília para evitar conversões desnecessárias
            if (dataHora.Kind == DateTimeKind.Utc)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(dataHora, zonaBrasilia);
            }
            else if (dataHora.Kind == DateTimeKind.Unspecified)
            {
                return TimeZoneInfo.ConvertTime(dataHora, zonaBrasilia);
            }

            return dataHora; // Já está no fuso correto
        }
    }

}

