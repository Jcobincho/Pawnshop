namespace Pawnshop.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapSourceAttribute : Attribute
    {
        public string SourceName { get; }

        public MapSourceAttribute(string sourceName)
        {
            SourceName = sourceName;
        }
    }
}
