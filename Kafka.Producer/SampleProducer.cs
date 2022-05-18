using Confluent.Kafka;
using Kafka.Common.Models;
using Kafka.Producer.Interfaces;
using System.Text;
using System.Text.Json;

namespace Kafka.Producer
{
	internal class SampleProducer : ISampleProducer
	{
		public async Task ProduceAsync(ProducerConfig config, Message message)
		{
			using var producer = new ProducerBuilder<Null, Message>(config)
				.SetErrorHandler((_, e) =>
				{
					Console.WriteLine($"Kafka Error {e.Code}: {e.Reason}");
				})
				.SetValueSerializer(new CustomValueSerializer<Message>())
				.Build();

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
