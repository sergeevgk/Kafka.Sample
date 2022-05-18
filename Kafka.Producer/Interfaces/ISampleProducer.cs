using Confluent.Kafka;
using Kafka.Common.Models;

namespace Kafka.Producer.Interfaces
{
	internal interface ISampleProducer
	{
		public Task ProduceAsync(ProducerConfig config, Message message);
	}
}
