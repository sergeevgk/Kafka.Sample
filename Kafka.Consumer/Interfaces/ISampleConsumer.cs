using Confluent.Kafka;

namespace Kafka.Consumer.Interfaces
{
	internal interface ISampleConsumer
	{
		public Task SubscribeAsync(string topicName, ConsumerConfig config);
	}
}
