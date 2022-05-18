using Confluent.Kafka;
using Kafka.Common.Models;

namespace Kafka.Producer.Interfaces
{
	internal interface ISampleProducer
	{
		public Task ProduceAsync(IProducer<Null, Message> producer, Message message);
	}
}
