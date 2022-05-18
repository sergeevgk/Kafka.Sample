using Confluent.Kafka;
using Kafka.Common.Models;

namespace Kafka.Consumer.Interfaces
{
	internal interface ISampleConsumer
	{
		public Task SubscribeAsync(string topicName, IConsumer<Ignore, Message> consumer);
	}
}
