using Confluent.Kafka;
using Kafka.Common.Models;
using Kafka.Producer.Interfaces;
using System.Text;
using System.Text.Json;

namespace Kafka.Producer
{
	internal class SampleProducer : ISampleProducer
	{
		public async Task ProduceAsync(IProducer<Null, Message> producer, Message message)
		{
			await producer.ProduceAsync(
				"sample-topic",
				new Message<Null, Message> { Value = message },
				CancellationToken.None);
			Console.WriteLine($"{message.Id} : '{message.Data}' - published.");
		}
	}

	internal class CustomValueSerializer<T> : ISerializer<T>
	{
		public byte[] Serialize(T data, SerializationContext context)
		{
			return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, typeof(T)));
		}
	}
}
