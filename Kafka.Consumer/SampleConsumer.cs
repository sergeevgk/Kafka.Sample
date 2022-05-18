using Confluent.Kafka;
using Kafka.Common.Models;
using Kafka.Consumer.Interfaces;
using System.Text.Json;

namespace Kafka.Consumer
{
	internal class SampleConsumer : ISampleConsumer
	{
		public async Task SubscribeAsync(string topicName, ConsumerConfig config)
		{
            using var consumer = new ConsumerBuilder<Ignore, Message>(config)
            .SetValueDeserializer(new CustomValueDeserializer<Message>())
            .Build();
            consumer.Subscribe(topicName);

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating to see messages.
                cts.Cancel();
            };
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cts.Token);
                        Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at offset: '{consumeResult.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"[Ctrl + C] typed");
                        consumer.Close();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unhandled Exception: {ex.Message}");
                        consumer.Close();
                        break;
                    }
                }
            }, cts.Token);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
	}

    internal class CustomValueDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
