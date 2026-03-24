using System.ComponentModel;
using System.Reflection;

namespace Connectamente.API.Enums
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());

            if (field == null) return value.ToString();

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute != null ? attribute.Description : value.ToString();
        }

        public static string ValidarModulo(this TipoModuloEnum perfil)
        {
            return (int)perfil switch
            {
                 666 => "AdminSistema",
                > 200 and < 300 => "Psicologia",
                _ => "Desconhecido"                 
            };

        }
    }
}
