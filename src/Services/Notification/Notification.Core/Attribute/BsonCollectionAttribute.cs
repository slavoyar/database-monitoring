namespace DatabaseMonitoring.Services.Notification.Core.Attributes;

public class BsonCollectionAttribute : Attribute
{
    public string CollectionName { get; }
    public BsonCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}