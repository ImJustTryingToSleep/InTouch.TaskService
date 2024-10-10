namespace InTouch.TaskService.Common.Entities;

public class TaskServiceSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Kafka Kafka { get; set; }
}

public class ConnectionStrings
{
    public string PostgreSQL { get; set; }
    public string EmailFrom { get; set; }
}

public class Kafka
{
    public string Topic { get; set; }
}