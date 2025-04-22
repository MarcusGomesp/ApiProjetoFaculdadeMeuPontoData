using MeuPontoMongoDb.Models;
using TimeZoneConverter;

namespace MeuPontoMongoDb.Utils
{
    public class CalculoTrabalho
    {


        // obter data/hora UTC para o horário de Brasília
        public static DateTime ObterHorarioBrasilia()
        {
            TimeZoneInfo zonaBrasilia = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Utc, zonaBrasilia);
        }

        // Converte a data/hora UTC para o horário de Brasília
        public static DateTime ConverterParaHorarioBrasilia(DateTime dataHora)
        {
            TimeZoneInfo zonaBrasilia = TZConvert.GetTimeZoneInfo("E. South America Standard Time");

            // Verifica data/hora UTC ou local
            if (dataHora.Kind == DateTimeKind.Utc)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(dataHora, zonaBrasilia);
            }
            else if (dataHora.Kind == DateTimeKind.Unspecified)
            {
                return TimeZoneInfo.ConvertTime(dataHora, zonaBrasilia);
            }

            return dataHora; 
        }
    }

}

