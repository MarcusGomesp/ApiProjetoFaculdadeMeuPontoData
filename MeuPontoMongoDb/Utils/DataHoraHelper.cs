using TimeZoneConverter;

namespace MeuPontoMongoDb.Utils
{
    public class DataHoraHelper
    {
        // obter data/hora UTC para o horário de Brasília
        public static DateTime ObterHorarioBrasilia()
        {
            TimeZoneInfo zonaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Utc, zonaBrasilia);
        }



        // Converte a data/hora UTC para o horário de Brasília
        public static DateTime ObterDataHoraBrasilia(DateTime dataHora)
        {
            TimeZoneInfo zonaBrasilia = TZConvert.GetTimeZoneInfo("E. South America Standard Time");

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
