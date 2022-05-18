using Confluent.Kafka;
using Kafka.Consumer;

var config = new ConsumerConfig
{
    GroupId = "sample-group-id",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var topicName = "sample-topic";

var sampleConsumer = new SampleConsumer();
await sampleConsumer.SubscribeAsync(topicName, config);
