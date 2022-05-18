using System.Text.Json;

namespace Kafka.Common.Models
{
	public readonly record struct Message(string Id, string Data, DateTime Timestamp)
	{
		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}