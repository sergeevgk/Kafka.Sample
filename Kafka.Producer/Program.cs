using Confluent.Kafka;
using Kafka.Common.Models;
using Kafka.Producer;
using Kafka.Producer.Interfaces;

var producerConfig = new ProducerConfig
{
    BootstrapServers = "localhost:9092",
    BatchSize = 1
};

ISampleProducer producer = new SampleProducer();
string userInput;
int messageIdCounter = 0;
do
{
    userInput = Console.ReadLine();
    switch (userInput)
    {
        case "q":
            break;
        default:
            var message = new Message { 
                Id = messageIdCounter.ToString(),
                Data = userInput,
                Timestamp = DateTime.Now 
            };
            await producer.ProduceAsync(producerConfig, message);
            break;
    }

} while (userInput != "q");

