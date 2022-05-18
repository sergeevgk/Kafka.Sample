using Confluent.Kafka;
using Kafka.Consumer;
using Kafka.Common.Models;

var config = new ConsumerConfig
{
    GroupId = "sample-group-id",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var kafkaConsumer = new ConsumerBuilder<Ignore, Message>(config)
         .SetValueDeserializer(new CustomValueDeserializer<Message>())
         .Build();

var topicName = "sample-topic";

var consumer = new SampleConsumer();
await consumer.SubscribeAsync(topicName, kafkaConsumer);
