using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestGiver.Shared.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuestGiver.Shared.JsonConverters
{
    public class QuestPriorityConverter : JsonConverter<QuestPriority>
    {
        public override QuestPriority Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                int value = reader.GetInt32();
                return (QuestPriority)value;
            }

            return QuestPriority.Low; // Default value if unable to parse
        }

        public override void Write(Utf8JsonWriter writer, QuestPriority value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)value);
        }
    }
}
